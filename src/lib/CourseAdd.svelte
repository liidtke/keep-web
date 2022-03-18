<script lang="ts">
  import { isOpen } from '$lib/store';

</script>

<div class="p-panel">
  <div class="p-panel__header">
    <h4 class="p-panel__title">Novo Curso</h4>
    <div class="p-panel__controls">
      <button
        on:click={() => isOpen.set(false)}
        class="p-button--base js-aside-close u-no-margin--bottom has-icon"
        ><i class="p-icon--close" /></button
      >
    </div>
  </div>
  <div class="p-panel__content">
    <div class="p-form p-form--stacked">
      <div class="p-form__group row">
        <div class="col-4">
          <label for="full-name-stacked" class="p-form__label">Nome</label>
        </div>

        <div class="col-8">
          <div class="p-form__control">
            <input type="text" id="full-name-stacked" name="fullName" autocomplete="name" />
          </div>
        </div>
      </div>

      <div class="p-form__group row">
        <div class="col-4">
          <label for="number" class="p-form__label">Número de Lições</label>
        </div>

        <div class="col-8">
          <div class="p-form__control">
            <input type="text" id="number" name="Number" autocomplete="name" />
          </div>
        </div>
      </div>

      <div class="row">
        
        <div class="col-12">
          <button class="p-button--positive u-float-right" name="add-details">Salvar</button>
        </div>
      </div>
    </div>
  </div>
</div>