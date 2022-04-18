<script lang="ts">
  import DateInput from "$lib/components/DateInput.svelte";
  import LocalitySelect from "$lib/components/LocalitySelect.svelte";
  import QuestionSelect from "$lib/components/QuestionSelect.svelte";
  import { onMount } from "svelte";
  import type { IStudent } from "./types";

  export let student: IStudent;

  $:{
    if(!student.Answers){
     student.Answers = [];
    }
  }

  onMount(async () => {
    
  })

  function addAnswer(){
    student.Answers.unshift({} as any);
    student.Answers = student.Answers;
  }

</script>

<!-- <h2>Cadastro</h2> -->
<div class="p-form is-dense student">
  <div class="row">
    <div class="col-9">
      <label for="name">Nome</label>
      <input type="text" name="name" id="name" bind:value={student.Name} />
    </div>
    <div class="col-3">
      <label for="number">Número</label>
      <input
        type="text"
        name="number"
        id="number"
        bind:value={student.Number}
      />
    </div>
  </div>

  <div class="row">
    <div class="col-3">
      <label for="matr">Matrícula</label>
      <input
        type="text"
        name="matr"
        id="matr"
        bind:value={student.Registration}
      />
    </div>
    <div class="col-3">
      <label for="adm">Admissão</label>
      <DateInput bind:value={student.AdmissionDate}/>
    </div>
    <div class="col-6">
      <label for="loc">Localidade</label>
      <LocalitySelect bind:student={student} />
    </div>
  </div>

  <div class="row">
    <div class="col-3">
      <label for="raio">Raio</label>
      <input type="text" name="raio" id="raio" bind:value={student.Radius} />
    </div>
    <div class="col-3">
      <label for="cela">Cela</label>
      <input type="text" name="cela" id="cela" bind:value={student.Cell} />
    </div>
    <div class="col-3">
      <label for="pav">Pavilhão</label>
      <input type="text" name="pav" id="pav" bind:value={student.Pav} />
    </div>
    <div class="col-3">
      <label for="xad">Xadrez</label>
      <input type="text" name="xad" id="xad" bind:value={student.Xad} />
    </div>
  </div>

  <div class="row">
    <div class="col-6">
      <label for="who">Quem Indicou?</label>
      <input type="text" name="who" id="who" bind:value={student.Referer} />
    </div>
    <div class="col-6">
      <label for="obs">Observações</label>
      <input type="text" name="obs" id="obs" bind:value={student.Observation} />
    </div>
  </div>
</div>

{#if student.Answers}
<div class="row">
  <h3>Perguntas e Respostas</h3>
  <div class="col-6">
    <button class="p--button" on:click={addAnswer}>Adicionar</button>
  </div>
  <div class="col-12">
    {#each student.Answers as answer}
    <QuestionSelect bind:selected={answer.Question}></QuestionSelect>
    <label for="ans">Resposta</label>
    <input type="text" name="" id="ans" bind:value={answer.Text} placeholder="Resposta">
    {/each}
  </div>
</div>
{/if}

<style>
  .student {
    line-height: 1rem;
  }
</style>
