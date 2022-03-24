<script lang="ts">
	import { AsideType } from "./types";
	import { isOpen, currentAside } from '$lib/store';
	import StudentAdd from '$lib/StudentAdd.svelte';
	import CourseAdd from '$lib/CourseAdd.svelte';
	import LocalityAdd from "./LocalityAdd.svelte";
  
</script>

{#if $currentAside == AsideType.Student}
<aside class="l-aside" id="aside-panel" class:is-collapsed={!$isOpen}>
	<StudentAdd />
</aside>
{/if}

{#if $currentAside == AsideType.Course}
<aside class="l-aside" id="aside-panel" class:is-collapsed={!$isOpen}>
	<CourseAdd></CourseAdd>
</aside>
{/if}

{#if $currentAside == AsideType.Locality}
<aside class="l-aside" id="aside-panel" class:is-collapsed={!$isOpen}>
	<LocalityAdd></LocalityAdd>
</aside>
{/if}
