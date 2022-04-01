<script lang="ts">
    import dateConverter from "$lib/date-converter";

    import IMask from "imask";
    import { onMount, onDestroy } from "svelte";

    export let value; //external value (dateTime)
    export let inputclass = "input";
    export let today: boolean = false;
    export let id = "date_input";

    let internalChange = false;
    let mask;
    let element;

    $: {
        valueChanged(value);
    }

    onMount(async () => {
        if (today) {
            let date = Date.now();
            value = dateConverter.toString(date);
        }

        var maskOptions = {
            mask: "00/00/0000", //TODO format
        };

        mask = IMask(element, maskOptions);
        if (value !== null && value !== undefined) {
            valueChanged(value);
        }
        mask.on("accept", changed);
    });

    onDestroy(async () => {
        if (mask) {
            mask.destroy();
        }
    });

    function valueChanged(value) {
        if (internalChange) {
            internalChange = false;
            return;
        }

        //console.log('value changed');

        if (mask && mask.value != value) {
            if (value) {
                mask.typedValue = dateConverter.toString(value);
            } else mask.typedValue = "";
        }
    }

    function changed(event) {
        //console.log("changed", event.target.value);
        if(!event || !event.target.value){
           return;
        }
        internalChange = true;
        let result = dateConverter.parse(event.target.value);
        if (result) {
          //  console.log('setting result')
            value = result;
        }
        else{
            //console.log('invalid value')
        }
    }
</script>

<input
    {id}
    bind:this={element}
    class={inputclass}
    type="text"
/>
