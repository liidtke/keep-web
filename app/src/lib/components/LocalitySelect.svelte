<script lang="ts">
  import { service, localities } from "$lib/store";
  import type { IStudent } from "$lib/types";
  import { onMount } from "svelte";
  import LocalityView from "$lib/components/LocalityView.svelte";

  export let student: IStudent;
  
  let selected; //internal state
  

  onMount(async () => {
    await $service.loadLocalities();
    if ($localities && student && student.LocalityId) {
      selected = $localities.find((x) => x.Id == student.LocalityId);
    }
  });

  function changed(){
    if(selected){
      student.Locality = selected;
      student.LocalityId = selected.Id;
    }
  }

  let showDetails:boolean = false;

</script>

<span class="details" on:click={() => showDetails = !showDetails}>Ver detalhes</span>
{#if student}
  <select name="localitySelect" id="localitySelect" bind:value={selected} on:change={changed}>
    <option value="">Selecione uma Localidade</option>
    {#each $localities as loc}
      <option value={loc}>
        {loc.Name}
      </option>
    {/each}
  </select>
{/if}

<LocalityView bind:locality={selected} bind:show={showDetails} ></LocalityView>


<style>
  .details{
    float:right;
    cursor: pointer;
    margin-bottom: .6rem;
    width: fit-content;
    padding-top: .4rem;
  }
</style>
