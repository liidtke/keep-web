<script lang="ts">
  import DateInput from "$lib/components/DateInput.svelte";
  import CourseSelect from "$lib/components/CourseSelect.svelte";
  import dateConverter from "$lib/date-converter";
  import Numbers from "$lib/components/Numbers.svelte";

  import type { IRegistration, IStudentCourses, IProgress } from "./types";

  export let id;

  let isAddingRegistration: boolean = false;
  let newRegistration: IRegistration;

  let stc: IStudentCourses;

  $: {
    load(id);
  }

  $: canSaveCourses = stc && stc.Registrations && stc.Registrations.length;

  function load(id) {
    //todo load
    stc = {
      Registrations: [],
      StudentId: id,
    } as any;
  }

  function cancelAddRegistration() {
    newRegistration = null;
  }

  function addRegistration() {
    isAddingRegistration = true;
    newRegistration = {
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
      stc.Registrations.unshift(newRegistration);
      newRegistration = {} as any;
      isAddingRegistration = false;
      stc = stc;
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
    stc = stc;
  }

  function cancelCourses() {}

  function saveCourses() {
    console.log(stc);
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

{#each stc.Registrations as registration}
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
              <th>Observações</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            {#each registration.Progress as progress}
              {#if !progress._edit}
                <tr>
                  <td>
                    {#each progress.Lessons as number}
                      <span>{number}</span>
                    {/each}
                  </td>
                  <td>
                    {dateConverter.toString(progress.Sent)}
                  </td>
                  <td>
                    {dateConverter.toString(progress.Returned)}
                  </td>
                  <td>
                    <!-- todo actions -->
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
                    <DateInput bind:value={progress.Sent} />
                  </td>
                  <td>
                    <input type="text" bind:value={progress.Comments} />
                  </td>
                  <td>
                    
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
{/each}

{#if canSaveCourses}
  <div class="row">
    <div class="col-6">
      <button class="p-button" on:click={cancelCourses}>Cancelar </button>
      <button class="p-button--positive" on:click={saveCourses}>Salvar </button>
    </div>
  </div>
{/if}

<!-- <div class="row">
  <h3>Finalizados</h3>
  <p>Nenhum</p>
</div> -->
<style>
  .lessons {
    max-height: 26vh;
    display: block;
    overflow: auto;
    border: 1px solid #b1b0b0;
    margin-bottom: 1rem;
  }

</style>
