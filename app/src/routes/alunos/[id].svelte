<script lang="ts">
  import { students, service } from "$lib/store";
  import { page } from "$app/stores";
  import { onMount } from "svelte";
  import StudentView from "$lib/StudentView.svelte";
  import StudentProgress from "$lib/StudentProgress.svelte";
  import type { IStudent } from "$lib/types";

  let id: string = $page.params.id;
  let student: IStudent;
  let notFound:boolean = false;

  onMount(async () => {
    await getStudent();
  });

  async function getStudent() {
    if (!id) {
      console.log('return');
      return;
    }

    console.log('check');

    if ($students && $students.length) {
      let index = $students.indexOf((x) => x.Id == id);
      if (index > -1) {
        console.log('found');
        student = $students[index];
      }
    }

    if(!student){
      loadFromServer();
    }
  }

  async function loadFromServer() {
    student = await $service.getStudent(id);
    if(!student){
      notFound = true;
    }
  }
</script>

<div class="p-strip">
  <h1>Detalhes do Aluno</h1>

  {#if student}
    <p class="p-heading--3">{student.Name}</p>
    <hr />

    <div class="row">
      <div class="col-12">
        <StudentView bind:student />
        cancelar salvar aqui
      </div>
      <div class="col-12">
        <StudentProgress bind:id={student.Id} />
      </div>
    </div>
  {:else}
    <div class="loader" />
    <div class="p-notification--caution">
      <div class="p-notification__content">
        <h5 class="p-notification__title">Não Encontrado</h5>
          <p class="p-notification__message">Não foi possível encontrar o aluno</p>
      </div>
    </div>
  {/if}
</div>
