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
      {#each $students as student }
      <tr>
        <td>{student.Name ?? ''}</td>
        <td>{student.Number}</td>
        <td>{student.Registration ?? ''}</td>
        <!-- <td>{student.Admission ?? ''}</td> -->
        <td>{student.Observations ?? ''}</td>
        <td>
          <button
          class="u-toggle is-dense"
          aria-controls="expanded-row"
          aria-expanded="false"
          data-shown-text="Hide"
          data-hidden-text="Show"
          on:click={() => navigate(student.Id)}>Detalhes</button
          >
        </td>
      </tr>
      {/each}
     
    </tbody>
    <!-- <tfoot>
      <tr>
        <th>One-time price</th>
        <td>$75,000</td>
        <td>$150,000</td>
      </tr>
    </tfoot> -->
  </table>
</div>
