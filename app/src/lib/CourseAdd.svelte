<script lang="ts">
  import { isOpen, service, course, courses } from "$lib/store";
  import type { ICourse } from '$lib/types';
  import { onMount } from "svelte";

  let message = null;

  let currentCourse:ICourse = {
    Id:null,
    Name:null,
    Lessons:0,
    IsActive:true,
  }

  course.subscribe((value) => { currentCourse = value as any });
  isOpen.subscribe((value) => openChanged(value));

  async function save(){
    let result = await $service.saveCourse(currentCourse);
    if(result.isSuccess){
      isOpen.set(false);
      let all = await $service.getCourses();
      courses.set(all);
    }
    else { 
      message = result.message ?? "Erro";
    }
  }

  function openChanged(value) {
    message = null;
    if(value === false){
      currentCourse = {} as any;
    }
    
  }

</script>

<div class="p-panel">
  <div class="p-panel__header">
    {#if currentCourse.Id}
    <h4 class="p-panel__title">Editar Curso</h4>
    {:else}
    <h4 class="p-panel__title">Novo Curso</h4>
    {/if}
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
            <input type="text" id="full-name-stacked" name="fullName" autocomplete="name" bind:value={currentCourse.Name} />
          </div>
        </div>
      </div>

      <div class="p-form__group row">
        <div class="col-4">
          <label for="number" class="p-form__label" >Número de Lições</label>
        </div>

        <div class="col-8">
          <div class="p-form__control">
            <input type="text" id="number" name="Number" autocomplete="name" bind:value={currentCourse.Lessons} />
            <!-- todo number input -->
          </div>
        </div>
      </div>


      <div class="row">
        <div class="col-small-1 col-medium-3 col-6 ">
          <button
            class="p-button u-float-left"
            name="add-details"
            on:click={() => isOpen.set(false)}>Cancelar</button
          >
        </div>
        <div class="col-small-3 col-medium-3 col-6 ">
          <button
            class="p-button--positive u-float-right"
            name="add-details"
            on:click={save}>Salvar</button
          >
        </div>
      </div>

    </div>
  </div>
</div>