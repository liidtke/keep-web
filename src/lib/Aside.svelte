<script lang="ts">
	import { AsideType } from "./types";
	import { isOpen, currentAside } from '$lib/store';
	import StudentAdd from '$lib/StudentAdd.svelte';
	import AddCourse from '$lib/AddCourse.svelte';
	import LocalityAdd from "./LocalityAdd.svelte";
  
</script>

{#if $currentAside == AsideType.Student}
<aside class="l-aside" id="aside-panel" class:is-collapsed={!$isOpen}>
	<StudentAdd />
</aside>
{/if}

{#if $currentAside == AsideType.Course}
<aside class="l-aside" id="aside-panel" class:is-collapsed={!$isOpen}>
	<AddCourse></AddCourse>
</aside>
{/if}

{#if $currentAside == AsideType.Locality}
<aside class="l-aside" id="aside-panel" class:is-collapsed={!$isOpen}>
	<LocalityAdd></LocalityAdd>
</aside>
{/if}
