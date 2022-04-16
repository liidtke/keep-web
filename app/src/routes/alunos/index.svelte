<script lang="ts">
  import { isOpen, currentAside, students, service } from "$lib/store";
  import { goto } from "$app/navigation";
  import { AsideType } from "$lib/types";
  import { onMount } from "svelte";

  onMount(async () => {
    currentAside.set(AsideType.Student);
    get();
  });

  function navigate(id) {
    goto("/alunos/" + id);
  }

  async function get(){
    let sts = await $service.getStudents();
    students.set(sts);
  }

</script>

<svelte:head>
	<title>Alunos</title>
</svelte:head>

<div class="p-strip">
  <h1>Alunos</h1>
  <hr />

  <button class="p-button--brand" on:click={() => isOpen.set(true)}>
  
    <span>Novo Aluno</span>
  </button>

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
      {#each $students as std }

      <tr>
        <td>{std.Name ?? ''}</td>
        <td>{std.Number}</td>
        <td>{std.Registration ?? ''}</td>
        <!-- <td>{student.Admission ?? ''}</td> -->
        <td>{std.Observation ?? ''}</td>
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
