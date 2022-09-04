<script lang="ts">
  import { students, service, snackMessage, showMessage } from "$lib/store";
  import { page } from "$app/stores";
  import { goto } from "$app/navigation";
  import { onMount } from "svelte";
  import StudentDetails from "$lib/StudentDetails.svelte";
  import StudentProgress from "$lib/StudentProgress.svelte";
  import type { IStudent } from "$lib/types";

  let id: string = $page.params.id;
  let student: IStudent;
  let notFound: boolean = false;
  let isLoading: boolean = true;
  let message = null;

  onMount(async () => {
    await getStudent();
  });

  async function getStudent() {
    if (!id) {
      console.log("return");
      return;
    }

    console.log("check");

    if ($students && $students.length) {
      let index = $students.indexOf((x) => x.Id == id);
      if (index > -1) {
        console.log("found");
        student = $students[index];
      }
    }

    if (!student) {
      await loadFromServer();
    }

    isLoading = false;
  }

  async function loadFromServer() {
    isLoading = true;
    student = await $service.getStudent(id);
    if (!student) {
      notFound = true;
    }
    isLoading = false;
  }

  async function save() {
    message = null;

    let result = await $service.saveStudent(student);
    if (result.isError) {
      message = result.message;
    } else {
      snackMessage.set("Salvo com Sucesso");
      showMessage.set(true);
    }
  }

  async function deleteStudent() {
    let result = await $service.deleteStudent(student);
    if (result && result.isSuccess) {
      snackMessage.set("Excluido com sucesso");
      showMessage.set(true);
      back();
    }
  }

  function cancel() {
    message = null;
    loadFromServer();
  }

  function back() {
    goto("/alunos/");
  }
</script>

<svelte:head>
  <title>Detalhes do Aluno</title>
</svelte:head>

<div class="p-strip">
  <h1>Detalhes do Aluno</h1>

  {#if student}
    <p class="p-heading--3">{student.Name}</p>
    <hr />

    <div class="row">
      {#if message}
        <div class="col-12">
          <div class="p-notification--caution">
            <div class="p-notification__content">
              <h5 class="p-notification__title">Erro ao Salvar</h5>
              <p class="p-notification__message">{message}</p>
            </div>
          </div>
        </div>
      {/if}

      <div class="col-12">
        <StudentDetails bind:student />
        <div class="row">
          <div class="col-6 ">
            <button class="p-button--base" on:click={back}>Voltar</button>
            <button class="p-button" on:click={cancel}>Cancelar</button>
            <button class="p-button--positive" on:click={save}>Salvar</button>
          </div>
        </div>
      </div>
      <hr />
      <div class="col-12">
        <StudentProgress bind:studentId={student.Id} />
      </div>
    </div>
  {:else if isLoading}
    <div class="loader" />
  {:else}
    <div class="p-notification--caution">
      <div class="p-notification__content">
        <h5 class="p-notification__title">Não Encontrado</h5>
        <p class="p-notification__message">
          Não foi possível encontrar o aluno
        </p>
      </div>
    </div>
  {/if}
  <hr />
  {#if student}
    <button class="p-button--negative fr" on:click={deleteStudent}
      >Excluir Aluno</button
    >
  {/if}
</div>

