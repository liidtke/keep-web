<script lang="ts">
  import DateInput from "$lib/components/DateInput.svelte";
  import CourseSelect from "$lib/components/CourseSelect.svelte";
  import dateConverter from "$lib/date-converter";
  import Numbers from "$lib/components/Numbers.svelte";

  import type { IRegistration, IProgress } from "./types";
  import { service, snackMessage, showMessage } from "./store";

  export let studentId;

  let isAddingRegistration: boolean = false;

  let registrations: IRegistration[] = [];
  let newRegistration: IRegistration;
  let message: string;

  $: {
    load(studentId);
  }

  async function load(id) {
    registrations = await $service.getRegistrations(id);
  }

  function cancelAddRegistration() {
    isAddingRegistration = false;
    newRegistration = null;
  }

  function addRegistration() {
    isAddingRegistration = true;
    newRegistration = {
      StudentId: studentId,
      Course: null,
      Progress: [],
      IsCompleted: false,
      StartDate: null,
    };
  }

  function confirmRegistration() {
    if (
      newRegistration &&
      newRegistration.Course &&
      newRegistration.StartDate
    ) {
      registrations.unshift(newRegistration);
      newRegistration = {} as any;
      isAddingRegistration = false;
      registrations = registrations;
    }
  }

  let isAddingProgress: boolean = false;
  function addProgress(reg) {
    isAddingProgress = true;
    let newProgress = {
      Lessons: [],
      Sent: null,
      Comments: null,
      Returned: null,
      _edit: true,
    };

    reg.Progress.push(newProgress);
    registrations = registrations;
  }

  function cancelRegistration(registration) {
    load(studentId);
  }

  async function removeRegistration(registration) {
    let response = await $service.deleteRegistration(registration);

    if (response.isSuccess) {
      load(studentId);
    } else {
      message = response.message ?? "Erro ao realizar ação";
    }
  }

  async function saveRegistration(registration) {
    registration = prepareSaveRegistration(registration);
    message = null;
    console.log(registration);

    let response = await $service.saveRegistration(registration);

    if (response.isSuccess) {
      snackMessage.set("Curso Salvo");
      showMessage.set(true);
      load(studentId);
    } else {
      message = response.message;
    }
  }

  function prepareSaveRegistration(registration: IRegistration) {
    registration.Progress.forEach((p) => {
      p.Lessons = p.Lessons.filter((x) => Number.isInteger(x));

      if (p.Lessons.some((l) => l == registration.Course.Lessons)) {
        registration.IsCompleted = true;
      }
    });
    
    return registration;
  }

  function removeLesson(registration, index){
    registration.Progress.splice(index, 1);
    registrations = registrations;
  }

</script>

<h2>Cursos</h2>
{#if !isAddingRegistration}
  <button class="p-button" on:click={addRegistration}>Inscrever</button>
{/if}

{#if isAddingRegistration}
  <div class="row">
    <div class="col-6 col-small-12">
      <h3>Inscrever</h3>
      <div class="form">
        <label for="type">Selecione um Curso</label>
        <CourseSelect bind:selected={newRegistration.Course} />

        <label for="date">Data de Inscrição</label>
        <DateInput bind:value={newRegistration.StartDate} today={true} />
      </div>
    </div>
  </div>
  <div class="row">
    <div class="col-6">
      <button class="p-button" on:click={cancelAddRegistration}
        >Cancelar
      </button>
      <button class="p-button--positive" on:click={confirmRegistration}
        >Adicionar
      </button>
    </div>
  </div>
{/if}

{#if message}
  <div class="p-notification--caution">
    <div class="p-notification__content">
      <h5 class="p-notification__title">Erro ao salvar</h5>
      <p class="p-notification__message">
        {message}
      </p>
    </div>
  </div>
{/if}

{#each registrations as registration}
  <div class="row">
    <div class="col-3">
      <h4>{registration.Course.Name}</h4>
    </div>
    <div class="col-3">
      {dateConverter.toString(registration.StartDate)}
    </div>
    {#if registration.IsCompleted}
      <div class="col-3">
        <strong>Completo</strong>
      </div>
    {/if}
  </div>

  <div class="row">
    <div class="col-12">
      <div class="lessons">
        <table>
          <thead>
            <tr>
              <th>Lições</th>
              <th>Enviado</th>
              <th>Recebido</th>
              <th class="only-pc">Observações</th>
              <th />
            </tr>
          </thead>
          <tbody>
            {#each registration.Progress as progress, index}
              {#if !progress._edit}
                <tr>
                  <td>
                    {#each progress.Lessons as number}
                      <span>{number} </span>
                    {/each}
                  </td>
                  <td>
                    {dateConverter.toString(progress.Sent)}
                  </td>
                  <td>
                    {dateConverter.toString(progress.Returned)}
                  </td>
                  <td class="only-pc">
                    {progress.Comments ?? ""}
                  </td>
                  <td>
                    <button
                      class="p-button has-icon right-floated"
                      aria-controls="expanded-row"
                      aria-expanded="false"
                      data-shown-text="Hide"
                      data-hidden-text="Show"
                      on:click={() => (progress._edit = true)}>
<i class="p-icon--show"></i>
                      </button
                    >
                  </td>
                </tr>
              {:else}
                <tr>
                  <td>
                    <Numbers bind:numbers={progress.Lessons} />
                  </td>
                  <td>
                    <DateInput bind:value={progress.Sent} today={true} />
                  </td>
                  <td>
                    <DateInput bind:value={progress.Returned} />
                  </td>
                  <td>
                    <input type="text" bind:value={progress.Comments} />
                  </td>
                  <td>
                    <button
                      class="p-button--negative is-dense"
                      aria-controls="expanded-row"
                      aria-expanded="false"
                      on:click={() => removeLesson(registration, index)}>Remover</button
                    >
                  </td>
                </tr>
              {/if}
            {/each}
          </tbody>
        </table>
      </div>
    </div>
  </div>

  <div class="row">
    <div class="col-12">
      <button class="p-button" on:click={() => addProgress(registration)}
        >Adicionar Progresso</button
      >
    </div>
  </div>

  <div class="row">
    <div class="col-6">
      {#if registration.Id}
        <button class="p-button--negative" on:click={() => removeRegistration(registration)}
          >Excluir
        </button>
      {/if}
      <button class="p-button" on:click={cancelRegistration}>Cancelar </button>
      <button
        class="p-button--positive"
        on:click={() => saveRegistration(registration)}
        >Salvar
      </button>
    </div>
  </div>
{/each}

<style>
  .lessons {
    max-height: 26vh;
    display: block;
    overflow: auto;
    border: 1px solid #b1b0b0;
    margin-bottom: 1rem;
  }
</style>
