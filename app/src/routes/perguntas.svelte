<script lang="ts">
  import { isOpen, currentAside, service, questions } from "$lib/store";
  import { goto } from "$app/navigation";
  import { AsideType } from "$lib/types";
  import { onMount } from "svelte";

  onMount(async () => {
    currentAside.set(AsideType.Questions);
    await load();
  });

  let message = null;

  async function load() {

      await $service.loadQuestions(true);
    
  }

  function addOne() {
    questions.set([{_isEditing:true}, ...$questions]);
  }

  function edit(question) {
    question._isEditing = true;
    $questions = $questions
  }

  async function save(question) {
    message = null;
    let result = await $service.saveQuestion(question);
    if (result.isSuccess) {
      load();
    } else {
      message = result.message;
    }
  }

  function cancel() {
    load();
  }
</script>

<svelte:head>
  <title>Perguntas</title>
</svelte:head>

<div class="p-strip">
  <h1>Perguntas</h1>
  <hr />
  <button class="p-button--brand" on:click={addOne}>Nova Pergunta</button>
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

  <table>
    <thead>
      <tr>
        <th>Texto</th>
        <th />
      </tr>
    </thead>

    <tbody>
      {#each $questions as q}
        <tr>
          {#if q._isEditing}
            <td>
              <input type="text" name="editing" bind:value={q.Text} />
            </td>
            <td>
              <button class="button p-button--positive u-float-right" on:click={()=> save(q)}>Salvar</button>
              <button class="button p-button u-float-right" on:click={cancel}>Cancelar</button>
            </td>
          {:else}
            <td>{q.Text ?? ""}</td>
            <td>
              <button
                class="u-toggle is-dense"
                aria-controls="expanded-row"
                aria-expanded="false"
                data-shown-text="Hide"
                data-hidden-text="Show"
                on:click={() => edit(q)}>Editar</button
              >
            </td>
          {/if}
        </tr>
      {/each}
    </tbody>
  </table>
</div>

<style>
.button{
  margin-left: 1rem;
}
</style>
