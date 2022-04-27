
<script lang="ts">
	import "../../app.css";
	import { onMount, afterUpdate, beforeUpdate } from 'svelte';
	import Snackbar from "$lib/components/Snackbar.svelte";
	import { snackMessage, showMessage, isAuthenticated } from "$lib/store";


	
	beforeUpdate(async () => {
		
	});

</script>

<main>
	<slot />
</main>


<Snackbar bind:message={$snackMessage} bind:show={$showMessage} />

<style>
	main{
		width: 100vw;
		height: 100vh;
		overflow: auto;
	}
</style>
