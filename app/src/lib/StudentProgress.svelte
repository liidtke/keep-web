<script lang="ts">
  import DateInput from "$lib/components/DateInput.svelte";

  export let id;
  let isAddingCourse: boolean = false;
  let course = {};

  $:{
    load(id);
  }

  function load(id){
    //todo load
  }
  function addCourse() {
    console.log("test");
    isAddingCourse = true;
  }

  function saveCourse() {}

  function cancelCourse() {
    course = {};
    isAddingCourse = false;
  }
  
</script>

<h2>Cursos</h2>
{#if !isAddingCourse}
  <button class="p-button" on:click={addCourse}>Inscrever</button>
{/if}

{#if isAddingCourse}
  <div class="row">
    <div class="col-6 col-small-12">
      <h4>Inscrever Aluno</h4>
      <div class="form">
        <label for="type">Selecione um Curso</label>
        <input type="text" id="type" />

        <label for="date">Data de Inscrição</label>
        <input type="text" id="date" />
      </div>
    </div>
  </div>
  <div class="row">
    <div class="col-6">
      <button class="p-button" on:click={cancelCourse}>Cancelar </button>
      <button class="p-button--positive" on:click={saveCourse}>Salvar </button>
    </div>
  </div>
{/if}

<h3>Em andamento</h3>
<div class="row">
  <div class="col-3"><strong>Encontro com a Vida</strong></div>
  <div class="col-3">Data de Inscrição: 12/01/2022</div>
</div>

<div class="row">
  <div class="col-12">
    <div class="lessons">
      <table>
        <thead>
          <tr>
            <th>Lição</th>
            <th>Enviado</th>
            <th>Recebido</th>
          </tr>
        </thead>
        <!-- <caption>
      <div class="p-strip">
        <div class="row">
          <div class="u-align-text--center col-12 col-medium-12 col-small-12">
           <p>Nenhum registro</p>
          </div>
        </div>
      </div>
    </caption> -->
        <tbody>
          <tr>
            <td>01</td>
            <td>01/02/2022</td>
            <td>15/02/2022</td>
          </tr>
          <tr>
            <td>01</td>
            <td>01/02/2022</td>
            <td>15/02/2022</td>
          </tr>
          <tr>
            <td>07</td>
            <td>01/02/2022</td>
            <td>15/02/2022</td>
          </tr>
          <tr>
            <td>08</td>
            <td>01/02/2022</td>
            <td>15/02/2022</td>
          </tr>
          <tr>
            <td>09</td>
            <td>01/02/2022</td>
            <td>15/02/2022</td>
          </tr>
          <tr>
            <td>10</td>
            <td>01/02/2022</td>
            <td>15/02/2022</td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</div>

<div class="row">
  <div class="col-6">
    <button class="p-button">Adicionar Registro</button>
    <button class="p-button--positive">Completar</button>
  </div>
  <div class="col-6" />

  <hr />
</div>
<div class="row">
  <h3>Finalizados</h3>
  <p>Nenhum</p>
</div>

<style>
  .lessons {
    max-height: 26vh;
    display: block;
    overflow: auto;
    border: 1px solid #b1b0b0;
    margin-bottom: 1rem;
  }
</style>
