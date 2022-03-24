<script lang="ts">
  import { service, localities } from '$lib/store';
  import { onMount } from "svelte";

  export let selected;

	onMount(async () => {
    if($localities == null || $localities.length == 0){
      let all = await $service.getLocalities();
      localities.set(all);
    }
	});

</script>

<select name="localitySelect" id="localitySelect" bind:value={selected}>
  <option value="">Selecione uma Localidade</option>
  {#each $localities as loc}
			<option value={loc}>
				{loc.Name}
			</option>
		{/each}
</select>

