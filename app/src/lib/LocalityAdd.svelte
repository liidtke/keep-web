<script lang="ts">
  import { isOpen, service, locality, localities } from "$lib/store";
  import { onMount } from "svelte";
  import type { ILocality } from "./types";

  let currentLocality: ILocality = {} as any;

  onMount(async () => {
    locality.subscribe((l) => {
      currentLocality = l as any;
      //console.log('changing locality', l)
    });
  });


  let result;

  isOpen.subscribe((value) => openChanged(value));

  function openChanged(value) {
    result = null;
    if(value === false){
      currentLocality = {} as any;
    }
    
  }

  async function save() {
    result = null;
    if (!currentLocality || !currentLocality.Name) {
      result = { message: "Dados necessários" };
      return;
    }

    let res = await $service.saveLocality(currentLocality);
    if (res.isSuccess) {
      isOpen.set(false);
      load();
    } else {
      result = res;
    }
  }

  async function load(){
    let all = await $service.getLocalities();
    localities.set(all);
  }

</script>

<div class="p-panel">
  <div class="p-panel__header">
    {#if currentLocality.Id}
    <h4 class="p-panel__title">Editar Localidade</h4>
    {:else}
    <h4 class="p-panel__title">Nova Localidade</h4>
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
    {#if result}
      <div class="p-notification--caution">
        <div class="p-notification__content">
          <h5 class="p-notification__title">Erro ao Salvar</h5>
          {#if result.message}
            <p class="p-notification__message">{result.message}</p>
          {/if}
        </div>
      </div>
    {/if}

    <div class="p-form p-form--stacked">
      <div class="p-form__group row">
        <div class="col-4">
          <label for="name" class="p-form__label">Nome</label>
        </div>
        <div class="col-8">
          <div class="p-form__control">
            <input
              type="text"
              id="name"
              name="name"
              autocomplete="name"
              bind:value={currentLocality.Name}
            />
          </div>
        </div>
      </div>

      <div class="p-form__group row">
        <div class="col-4">
          <label for="addr" class="p-form__label">Endereço</label>
        </div>
        <div class="col-8">
          <div class="p-form__control">
            <input
              type="text"
              id="addr"
              name="Number"
              autocomplete="name"
              bind:value={currentLocality.Address}
            />
          </div>
        </div>
      </div>

      <div class="p-form__group row">
        <div class="col-4">
          <label for="number" class="p-form__label">Número</label>
        </div>
        <div class="col-8">
          <div class="p-form__control">
            <input
              type="text"
              id="number"
              name="Number"
              autocomplete="name"
              bind:value={currentLocality.Number}
            />
          </div>
        </div>
      </div>

      <div class="p-form__group row">
        <div class="col-4">
          <label for="ad2" class="p-form__label">Complemento</label>
        </div>
        <div class="col-8">
          <div class="p-form__control">
            <input
              type="text"
              id="ad2"
              name="Number"
              autocomplete="name"
              bind:value={currentLocality.Address2}
            />
          </div>
        </div>
      </div>

      <div class="p-form__group row">
        <div class="col-4">
          <label for="zip" class="p-form__label">CEP</label>
        </div>
        <div class="col-8">
          <div class="p-form__control">
            <input
              type="text"
              id="zip"
              name="Number"
              autocomplete="name"
              bind:value={currentLocality.ZipCode}
            />
          </div>
        </div>
      </div>

      <div class="p-form__group row">
        <div class="col-4">
          <label for="city" class="p-form__label">Cidade</label>
        </div>
        <div class="col-8">
          <div class="p-form__control">
            <input
              type="text"
              id="city"
              name="Number"
              autocomplete="name"
              bind:value={currentLocality.City}
            />
          </div>
        </div>
      </div>

      <div class="p-form__group row">
        <div class="col-4">
          <label for="state" class="p-form__label">Estado</label>
        </div>
        <div class="col-8">
          <div class="p-form__control">
            <input
              type="text"
              id="state"
              name="Number"
              autocomplete="name"
              bind:value={currentLocality.State}
            />
          </div>
        </div>
      </div>

      <div class="p-form__group row">
        <div class="col-4">
          <label for="box" class="p-form__label">Caixa Postal</label>
        </div>
        <div class="col-8">
          <div class="p-form__control">
            <input
              type="text"
              id="box"
              name="POBox"
              autocomplete="name"
              bind:value={currentLocality.Box}
            />
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
