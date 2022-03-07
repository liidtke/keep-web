<script lang="ts">
  import { isOpen, currentAside, service, localities } from "$lib/store";
  import { goto } from "$app/navigation";
  import { AsideType } from "$lib/types";
  import { onMount } from "svelte";

  onMount(async () => {
    currentAside.set(AsideType.Locality);
    load();
  });

  function load() {
    if ($localities == null || $localities.length == 0) {
      localities.set($service.getAllLocalities());
    }
  }

  function edit(locality){

  }
</script>

<div class="p-strip">
  <h1>Localidades</h1>
  <hr />
  <button class="p-button--brand" on:click={() => isOpen.set(true)}>Nova Localidade</button>
  <table>
    <thead>
      <tr>
        <th>Nome</th>
        <th>Endereço</th>
        <th>CEP</th>
        <th></th>
      </tr>
    </thead>

    <tbody>
      {#each $localities as locality}
        <tr>
          <td>{locality.name}</td>
          <td>{locality.address}</td>
          <td>{locality.zipCode}</td>
          <td>
            <button
            class="u-toggle is-dense"
            aria-controls="expanded-row"
            aria-expanded="false"
            data-shown-text="Hide"
            data-hidden-text="Show"
            on:click={() => edit(locality)}>Detalhes</button
          >
          </td>
        </tr>
      {/each}
    </tbody>
  </table>
</div>
