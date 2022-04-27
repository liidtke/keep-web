<script lang="ts">
  import Nip from "$lib/components/Nip.svelte";
  export let numbers:any[] = [];

  $: {
    if(!numbers || numbers.length == 0){
      numbers = [''];
    }
    
    else if (numbers.length && numbers[numbers.length-1]){
      numbers.push('');
    }
    else if(numbers.length > 1 && !numbers[numbers.length-1] && !numbers[numbers.length-2]){
      numbers.pop();
    }
  }
</script>

{#each numbers as number}
 <!-- <input class="mini" type="text" bind:value={number}> -->
 <Nip bind:value={number} inputclass="mini"></Nip>
{/each}



