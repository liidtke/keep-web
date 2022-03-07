<script lang="ts">
  import { service, localities } from '$lib/store';
  import { onMount } from "svelte";

  export let selected;

	onMount(async () => {
    if($localities == null || $localities.length == 0){
      localities.set($service.getAllLocalities());
    }
	});

</script>

<select name="localitySelect" id="localitySelect" bind:value={selected}>
  <option value="">Selecione uma Localidade</option>
  {#each $localities as loc}
			<option value={loc}>
				{loc.name}
			</option>
		{/each}
</select>

