<script lang="ts">
  import { currentAside, isOpen, service, course, courses } from "$lib/store";
  import { AsideType, type ICourse } from "$lib/types";
  import { onMount } from "svelte";

  //let currentCourses:ICourse[] = [];

  onMount(async () => {
    currentAside.set(AsideType.Course);
    let all = await $service.getCourses();
    courses.set(all);
  });

  function edit(item){
    course.set(item);
    isOpen.set(true);
  }

  function boolToText(b){
    if(b) return "Ativo"
    else return "Inativo"
  }

</script>

<svelte:head>
	<title>Cursos</title>
</svelte:head>

<div class="p-strip">
  <h1>Cursos</h1>
  <hr />

  <button class="p-button--brand" on:click={() => isOpen.set(true)}>
    <span>Novo Curso</span>
  </button>

  <table aria-label="alunos">
    <thead>
      <tr>
        <th>Nome</th>
        <th>Número de Lições</th>
        <th>Status</th>
        <th />
      </tr>
    </thead>
    <tbody>
      {#each $courses as item}
        <tr>
          <td>{item.Name}</td>
          <th>{item.Lessons}</th>
          <th>{boolToText(item.IsActive)}</th>
          <td>
            <button
              class="u-toggle is-dense"
              aria-controls="expanded-row"
              aria-expanded="false"
              data-shown-text="Hide"
              data-hidden-text="Show"
              on:click={() => edit(item)}
              >Editar</button
            >
          </td>
        </tr>
      {/each}
    </tbody>
  </table>
</div>
