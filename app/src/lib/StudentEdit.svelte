<script lang="ts">
  import DateInput from "$lib/components/DateInput.svelte";

  export let student;
</script>

<!-- <h2>Cadastro</h2> -->
<div class="p-form is-dense student">
  <div class="row">
    <div class="col-9">
      <label for="name">Nome</label>
      <input type="text" name="name" id="name" bind:value={student.name} />
    </div>
    <div class="col-3">
      <label for="number">Número</label>
      <input type="text" name="number" id="number" />
    </div>
  </div>

  <div class="row">
    <div class="col-3">
      <label for="matr">Matrícula</label>
      <input type="text" name="matr" id="matr" />
    </div>
    <div class="col-3" >
      <label for="adm">Admissão</label>
      <DateInput id="adm"></DateInput>
    </div>
    <div class="col-6" >
      <label for="loc">Localidade</label>
      <input type="text" name="loc" id="loc" />
    </div>
  </div>

  <div class="row">
    <div class="col-3">
      <label for="raio">Raio</label>
      <input type="text" name="raio" id="raio" />
    </div>
    <div class="col-3">
      <label for="cela">Cela</label>
      <input type="text" name="cela" id="cela" />
    </div>
    <div class="col-3">
      <label for="pav">Pavilhão</label>
      <input type="text" name="pav" id="pav" />
    </div>
    <div class="col-3">
      <label for="xad">Xadrez</label>
      <input type="text" name="xad" id="xad" />
    </div>
  </div>

  <div class="row">
    <div class="col-6">
      <label for="who">Quem Indicou?</label>
      <input type="text" name="who" id="who" />
    </div>
    <div class="col-6">
      <label for="obs">Observações</label>
      <input type="text" name="obs" id="obs" />
    </div>
  </div>

  <div class="row">
    <div class="col-12">
      <button class="p-button" >Cancelar</button>
      <button class="p-button--positive u-float-right">Salvar</button>
    </div>
  </div>
</div>


<style>
  .student{
    line-height: 1rem;
  }
</style>