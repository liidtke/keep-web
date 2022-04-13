
<script lang="ts">
	import { onMount, afterUpdate, beforeUpdate } from 'svelte';

	
	beforeUpdate(async () => {
		
	});

</script>

<main>
	<slot />
</main>



<style>
	main{
		width: 100vw;
		height: 100vh;
		overflow: auto;
	}
</style>