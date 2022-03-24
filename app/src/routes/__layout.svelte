<script lang="ts">
	import Header from "$lib/header/Header.svelte";
	import Aside from "$lib/Aside.svelte";
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
