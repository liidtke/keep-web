<script lang="ts">
  import { service, courses } from '$lib/store';
  import { onMount } from "svelte";

  export let selected;

	onMount(async () => {
    if($courses == null || $courses.length == 0){
      let all = await $service.getCourses();
      courses.set(all);
    }
	});

</script>

<select name="select" id="select" bind:value={selected}>
  <option value="">Selecione uma Localidade</option>
  {#each $courses as course}
			<option value={course}>
				{course.Name}
			</option>
		{/each}
</select>

