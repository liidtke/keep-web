<script lang="ts">
  import DateInput from "$lib/components/DateInput.svelte";
  import LocalitySelect from "$lib/components/LocalitySelect.svelte";
  import { onMount } from "svelte";
  import type { IStudent } from "./types";
  import { service } from '$lib/store';

  export let student: IStudent;

  onMount(async () => {
    if(student){
     
    }
  })

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

<style>
  .student {
    line-height: 1rem;
  }
</style>
