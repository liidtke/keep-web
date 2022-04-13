<script lang="ts">
	import Header from "$lib/header/Header.svelte";
	import { snackMessage, showMessage, isAuthenticated } from "$lib/store";
	import Aside from "$lib/Aside.svelte";
	import { goto } from "$app/navigation";
	import Snackbar from "$lib/components/Snackbar.svelte";
	import { onMount } from "svelte";
	import "../app.css";
	import { AuthService } from "$lib/auth-service";

	onMount(async () => {
		if (!$isAuthenticated) {
			let auth = new AuthService();
			auth.reload();

			if(!$isAuthenticated){
				goto("/login");
			}
		}
	});
</script>

{#if $isAuthenticated}
	<main class="l-main">
		<Header />
		<div class="row">
			<slot />
		</div>
	</main>
{/if}

<Aside />

<Snackbar bind:message={$snackMessage} bind:show={$showMessage} />
