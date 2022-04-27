<script lang="ts">
  import { isOpen, service, students } from '$lib/store';
  import DateInput from './components/DateInput.svelte';
  import LocalitySelect from '$lib/components/LocalitySelect.svelte';
  import type { IStudent } from './types';

  let el;
  let message = null;
  let student : IStudent = {
   
  } as any

  $:{
    if(message){
      el.scrollIntoView();
    }
  }

  async function save(){
    message = null;

    let result = await $service.saveStudent(student);
    if(result.isError){
      message = result.message;
    }
    else{
      isOpen.set(false);
      getAll();
    }
  }

  function cancel(){
    message = null;
    student = { } as any;
    isOpen.set(false);
  }

  async function getAll(){
    let sts = await $service.getStudents();
    students.set(sts);
  }

</script>

<div class="p-panel" bind:this={el}>
  <div class="p-panel__header">
    <h4 class="p-panel__title">Novo Aluno</h4>
    <div class="p-panel__controls">
      <button
        on:click={() => isOpen.set(false)}
        class="p-button--base js-aside-close u-no-margin--bottom has-icon"
        ><i class="p-icon--close" /></button
      >
    </div>
  </div>
  <div class="p-panel__content">

    {#if message}
      <div class="p-notification--caution">
        <div class="p-notification__content">
          <h5 class="p-notification__title">Erro ao Salvar</h5>
            <p class="p-notification__message">{message}</p>
        </div>
      </div>
    {/if}

    <div class="p-form p-form--stacked">
      <div class="p-form__group row">
        <div class="col-4">
          <label for="full-name-stacked" class="p-form__label">Nome</label>
        </div>

        <div class="col-8">
          <div class="p-form__control">
            <input type="text" id="full-name-stacked" name="fullName" autocomplete="name" bind:value={student.Name} />
          </div>
        </div>
      </div>

      <div class="p-form__group row">
        <div class="col-4">
          <label for="number" class="p-form__label">Número</label>
        </div>

        <div class="col-8">
          <div class="p-form__control">
            <input type="text" id="number" name="Number" autocomplete="name" bind:value={student.Number} />
          </div>
        </div>
      </div>

      <div class="p-form__group row">
        <div class="col-4">
          <label for="matr" class="p-form__label">Matrícula</label>
        </div>

        <div class="col-8">
          <div class="p-form__control">
            <input
              type="text"
              id="matr"
              name="username-stacked"
              autocomplete="username"
              aria-describedby="exampleHelpTextMessage"
              bind:value={student.Registration}
            />
            <!-- <p class="p-form-help-text" id="exampleHelpTextMessage">30 characters or fewer.</p> -->
          </div>
        </div>
      </div>

      <div class="p-form__group row">
        <div class="col-4">
          <label for="localitySelect" class="p-form__label"> Localidade </label>
        </div>

        <div class="col-8">
          <div class="p-form__control">
            <LocalitySelect bind:student={student}></LocalitySelect>
            <p class="p-form-help-text" id="exampleHelpTextMessage">Presídio - Caso não exista adicione um novo</p>
          </div>
        </div>
      </div>

      <div class="p-form__group row">
        <div class="col-4">
          <label for="raio" class="p-form__label"> Raio </label>
        </div>

        <div class="col-8">
          <div class="p-form__control">
            <input
              type="text"
              id="raio"
              class="p-form-validation__input"
              name="raio"
              autocomplete="raio"
              bind:value={student.Radius}
            />
          </div>
        </div>
      </div>

      <div class="p-form__group row">
        <div class="col-4">
          <label for="cela" class="p-form__label"> Cela </label>
        </div>

        <div class="col-8">
          <div class="p-form__control">
            <input
              type="text"
              id="cela"
              class="p-form-validation__input"
              aria-invalid="true"
              name="cela"
              autocomplete="cela"
              bind:value={student.Cell}
            />
          </div>
        </div>
      </div>

      <div class="p-form__group row">
        <div class="col-4">
          <label for="pav" class="p-form__label"> Pavilhão </label>
        </div>

        <div class="col-8">
          <div class="p-form__control">
            <input
              type="text"
              id="pav"
              class="p-form-validation__input"
              name="pav"
              autocomplete="pav"
              aria-describedby="username-error-message-stacked"
              bind:value={student.Pav}
            />
          </div>
        </div>
      </div>

      <div class="p-form__group row">
        <div class="col-4">
          <label for="xad" class="p-form__label"> Xadrez </label>
        </div>

        <div class="col-8">
          <div class="p-form__control">
            <input
              type="text"
              id="xad"
              class="p-form-validation__input"
              aria-invalid="true"
              name="username-stackederror"
              autocomplete="username"
              aria-describedby="username-error-message-stacked"
              bind:value={student.Xad}
            />
          </div>
        </div>
      </div>

      <div class="p-form__group row">
        <div class="col-4">
          <label for="address-optional-stacked0" class="p-form__label">Quem indicou?</label>
        </div>

        <div class="col-8">
          <div class="p-form__control">
            <input type="text" name="ind" autocomplete="ind" bind:value={student.Referer} />
          </div>
        </div>
      </div>

      <div class="p-form__group row">
        <div class="col-4">
          <label for="obs" class="p-form__label">Observações</label>
        </div>

        <div class="col-8">
          <div class="p-form__control">
            <input
              type="text"
              id="obs"
              name="address-optional-stacked"
              autocomplete="address-line3"
              bind:value={student.Observation}
            />
          </div>
        </div>
      </div>

      <div class="p-form__group row">
        <div class="col-4">
          <label for="adm" class="p-form__label">Admissão</label>
        </div>

        <div class="col-8">
          <div class="p-form__control">
            <DateInput bind:value={student.AdmissionDate} today={true}/>
          </div>
        </div>
      </div>

      <div class="row">
        <div class="col-small-1 col-medium-3 col-6 ">
          <button class="p-button u-float-left" name="add-details" on:click={cancel}>Cancelar</button>
        </div>
        <div class="col-small-3 col-medium-3 col-6 ">
          <button class="p-button--positive u-float-right" name="add-details" on:click={save}>Salvar</button>
        </div>
      </div>
    </div>
  </div>
</div>