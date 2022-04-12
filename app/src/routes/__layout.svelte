<script lang="ts">
	import Header from "$lib/header/Header.svelte";
	import { snackMessage, showMessage } from "$lib/store";
	import Aside from "$lib/Aside.svelte";
	import Snackbar from '$lib/components/Snackbar.svelte';
	import "../app.css";

</script>

<!-- <div class="row"> -->
<main class="l-main">
	<Header />
	<div class="row">
		<slot />
	</div>
</main>

<Aside />

<Snackbar bind:message={$snackMessage} bind:show={$showMessage}></Snackbar>