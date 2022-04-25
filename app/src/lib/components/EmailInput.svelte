<script lang="ts">
    import IMask from "imask";
    import { onMount, onDestroy } from "svelte";

    export let value;
    let element;
    let mask;

    $: {
        if (mask && mask.value != value) {
            if (value) {
                mask.typedValue = value;
            } else mask.typedValue = "";
        }
    }

    onMount(async () => {
        var maskOptions = {
            mask: /^\S*@?\S*$/,
        };
        mask = IMask(element, maskOptions);
        if (value !== null && value !== undefined) {
            mask.typedValue = value;
        }

        mask.on("accept", changed);
    });

    onDestroy(async () => {
        if (mask) {
            mask.destroy();
        }
    });

    function changed() {
        value = mask.value;
    }

</script>

<label for="email">Email</label>
<input id="email" bind:this={element} class="input" type="email" />

<style>
</style>
