<script lang="ts">
  import { isOpen, currentAside, students, service, localities } from "$lib/store";
  import { goto } from "$app/navigation";
  import { AsideType } from "$lib/types";
  import { onMount } from "svelte";
  import type { Service } from "$lib/service";
  import dateConverter from "$lib/date-converter";
  import LocalitySelect from '$lib/components/LocalitySelect.svelte';

  let api: Service;
  let search: string;
  let order: string = "name";
  let loc;

  service.subscribe((s) => {
    api = s;
    //console.log("updating service instance")
  });

  onMount(async () => {
    currentAside.set(AsideType.Student);
    get();
    $service.loadLocalities(true);
  });

  function navigate(id) {
    goto("/alunos/" + id);
  }

  async function get() {
    console.log("getting students");
    let sts = await api.getStudents(search, order);
    students.set(sts);
  }

  $: {
    performSearch(search);
  }

  async function performSearch(search) {
    if (search && search.length > 2 || loc) {
      let sts = await api.getStudents(search, order, loc.Id);
      students.set(sts);
    } else if (!search) {
      await get();
    }
  }

  async function orderByName() {
    order = "name";
    get();
    
  }

  async function orderByLatest() {
    order = "latest";
    get();
  }

  function locChanged(){
    console.log('changed', loc)
    performSearch(search);
  }


</script>

<svelte:head>
  <title>Alunos</title>
</svelte:head>

<div class="p-strip">
  <h1>Alunos</h1>
  <hr />

  <div class="p-form">
    <div class="row">
      <div class="col-2 col-medium-2 col-small-1">
        <button class="p-button--brand" on:click={() => isOpen.set(true)}>
          <span>Novo Aluno</span>
        </button>
      </div>

      <div class="col-6 col-medium-2 col-small-1">
        
        <button class="p-button fr inline" on:click={orderByLatest}>
          <i class="p-icon--chevron-up"></i>
          <span>Recentes</span>
        </button>
        
        <button class="p-button fr inline" on:click={orderByName}>
          <i class="p-icon--chevron-down"></i>
          <span>Nome</span>
        </button>


      </div>

      <div class="col-4 col-medium-2 col-small-2">
        <input
          type="search"
          id="search"
          class="p-search-box__input"
          name="search"
          placeholder="Buscar"
          autocomplete="on"
          bind:value={search}
        />
      </div>

 <select name="localitySelect" id="localitySelect" bind:value={loc} on:change={locChanged}>
    <option value="">Selecione uma Localidade</option>
    {#each $localities as loc}
      <option value={loc}>
        {loc.Name}
      </option>
    {/each}
  </select>

    </div>
  </div>

  <table aria-label="alunos">
    <thead>
      <tr>
        <th>Nome</th>
        <th>Matrícula</th>
        <th>Número</th>
        <th>Admissão</th>
        <!-- <th>Observações</th> -->
        <th />
      </tr>
    </thead>
    <tbody>
      {#if $students.length}
        {#each $students as std}
          <tr>
            <td>{std.Name ?? ""}</td>
            <td>{std.Registration ?? ""}</td>
            <td>{std.Number ?? ""}</td>
            <td>{dateConverter.toString(std.AdmissionDate) ?? ""}</td>
            <!-- <td>{std.Observation ?? ""}</td> -->
            <td>
              <button
                class="u-toggle is-dense"
                aria-controls="expanded-row"
                aria-expanded="false"
                data-shown-text="Hide"
                data-hidden-text="Show"
                on:click={() => navigate(std.Id)}>Detalhes</button
              >
            </td>
          </tr>
        {/each}
      {/if}
    </tbody>
  </table>
</div>
