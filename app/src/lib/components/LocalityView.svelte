<script lang="ts">
  import type { ILocality } from "$lib/types";
  export let locality: ILocality;
  export let show: boolean = false;
</script>

{#if locality}
  <div class="p-modal" id="modal" class:hide={!show}>
    <section
      class="p-modal__dialog"
      role="dialog"
      aria-modal="true"
      aria-labelledby="modal-title"
      aria-describedby="modal-description"
    >
      <header class="p-modal__header">
        <h2 class="p-modal__title" id="modal-title">Localidade</h2>
        <button
          class="p-modal__close"
          aria-label="Close active modal"
          aria-controls="modal"
          on:click={() => (show = false)}>Close</button
        >
      </header>
      <p id="modal-description">
        {locality.Name}
      </p>
      <div class="row">
        <div class="col-12">
          <strong>Endereço</strong>
          <p>{locality.Address ?? ''} {locality.Number ?? ''} {locality.Address2 ?? ''}</p>
        </div>
        <div class="col-12">
          <strong>CEP</strong>
          <p>{locality.ZipCode ?? ''} - {locality.City ?? ''} - {locality.State ?? ''}</p>
        </div>
        {#if locality.Box}
        <div class="col-12">
          <strong>Caixa Postal</strong>
          <p>{locality.Box}</p>
        </div>
        {/if}
      </div>
    </section>
  </div>
{/if}

<style>
  .hide {
    display: none;
  }
</style>
