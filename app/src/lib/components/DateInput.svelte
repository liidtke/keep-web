<script lang="ts">
import dateConverter from "$lib/date-converter";

  import IMask from "imask";
  import { onMount, onDestroy } from "svelte";

  export let value;
  export let inputclass = "input";
  export let today:boolean = false;
  export let id = "date_input";
  let mask;
  let element;

  $: {
      if(mask && mask.value != value){
          if(value){
              mask.typedValue = value.toString(); //convert here
          }
          else mask.typedValue = "";
      }
  }

  onMount(async () => {
      if(today){
          let date = Date.now();
          value = dateConverter.convert(date);
      }

      var maskOptions = {
          mask: "00/00/0000", //TODO format
      };

      mask = IMask(element, maskOptions);
      if(value !== null && value !== undefined){
          mask.typedValue = value.toString();
      }
      mask.on("accept", changed);
  });

  onDestroy(async () => {
      if(mask){
          mask.destroy();

      }
  });

  function changed(){
      value = mask.value; //convert here
  }

</script>

<input id={id} bind:this={element} class={inputclass} type="text"/>
