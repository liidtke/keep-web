<script lang="ts">
  import { isOpen, currentAside, service, localities, locality } from "$lib/store";
  import { goto } from "$app/navigation";
  import { AsideType } from "$lib/types";
  import { onMount } from "svelte";

  onMount(async () => {
    currentAside.set(AsideType.Locality);
    await load();
  });

  async function load() {
    if ($localities == null || $localities.length == 0) {
      await $service.loadLocalities();
      
    }

    //console.log($localities);
  }

  function edit(l){
    locality.set(l);
    isOpen.set(true);
  }
</script>

<div class="p-strip">
  <h1>Localidades</h1>
  <hr />
  <button class="p-button--brand" on:click={() => isOpen.set(true)}>Nova Localidade</button>
  <table>
    <thead>
      <tr>
        <th>Nome</th>
        <th>Endereço</th>
        <th>CEP</th>
        <th></th>
      </tr>
    </thead>

    <tbody>
      {#each $localities as locality}
        <tr>
          <td>{locality.Name ?? ''}</td>
          <td>{locality.Address ?? ''}</td>
          <td>{locality.ZipCode ?? ''}</td>
          <td>
            <button
            class="u-toggle is-dense"
            aria-controls="expanded-row"
            aria-expanded="false"
            data-shown-text="Hide"
            data-hidden-text="Show"
            on:click={() => edit(locality)}>Detalhes</button
          >
          </td>
        </tr>
      {/each}
    </tbody>
  </table>
</div>
