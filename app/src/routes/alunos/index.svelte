<script lang="ts">
  import { isOpen, currentAside, students, service } from "$lib/store";
  import { goto } from "$app/navigation";
  import { AsideType } from "$lib/types";
  import { onMount } from "svelte";
  import type { Service } from "$lib/service";

  let api: Service;
  let search:string;

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
    let sts = await api.getStudents();
    students.set(sts);
  }

  $:{
    performSearch(search)
  }

  async function performSearch(search) {
    if(search && search.length > 2){
      let sts = await api.getStudents(search);
      students.set(sts);
    }
    else if(!search){
      await get();
    }
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
      <div class="col-8 col-medium-4 col-small-2">
        <button class="p-button--brand" on:click={() => isOpen.set(true)}>
          <span>Novo Aluno</span>
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
  </div>
  </div>

  <table aria-label="alunos">
    <thead>
      <tr>
        <th>Nome</th>
        <th>Número</th>
        <th>Matrícula</th>
        <!-- <th>Admissão</th> -->
        <th>Observações</th>
        <th />
      </tr>
    </thead>
    <tbody>
      {#if $students.length}
        {#each $students as std}
          <tr>
            <td>{std.Name ?? ""}</td>
            <td>{std.Number}</td>
            <td>{std.Registration ?? ""}</td>
            <!-- <td>{student.Admission ?? ''}</td> -->
            <td>{std.Observation ?? ""}</td>
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
