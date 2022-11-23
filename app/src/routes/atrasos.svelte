<script lang="ts">
  import { service } from "$lib/store";
  import { goto } from "$app/navigation";
  import { onMount } from "svelte";
  import type { Service } from "$lib/service";

  let api: Service;
  // let search: string;
  let delays = [];

  service.subscribe((s) => {
    api = s;
    //console.log("updating service instance")
  });

  onMount(async () => {
    get();
  });

  async function get() {
    delays = await api.getDelays();
  }

  async function process(){
    delays = await api.checkDelays();
    
  }
  function navigate(id) {
    goto("/alunos/" + id);
  }
</script>

<svelte:head>
  <title>Atrasos</title>
</svelte:head>

<div class="p-strip">
  <h1>Atrasos</h1>
  <hr />
  <button class="p-button--brand" on:click={process}>
    <span>Buscar Atrasos</span>
  </button>

  <table aria-label="atrasos">
    <thead>
      <tr>
        <th>Nome</th>
        <th>Curso</th>
        <th />
      </tr>
    </thead>
    <tbody>
      {#if delays && delays.length}
        {#each delays as delay}
          <tr>
            <td>{delay.StudentName ?? ""}</td>
            <td>{delay.CourseName ?? ""}</td>
            <!-- <td>{std.Observation ?? ""}</td> -->
            <td>
              <button
                class="u-toggle is-dense"
                aria-controls="expanded-row"
                aria-expanded="false"
                data-shown-text="Hide"
                data-hidden-text="Show"
                on:click={() => navigate(delay.StudentId)}>Detalhes</button
              >
            </td>
          </tr>
        {/each}
      {/if}
    </tbody>
  </table>

</div>
