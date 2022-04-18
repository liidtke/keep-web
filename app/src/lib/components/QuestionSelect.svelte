<script lang="ts">
  import { service, questions } from "$lib/store";
  import { onMount } from "svelte";


  export let selected;

  onMount(async () => {
    if($questions == null || $questions.length == 0){
      await $service.loadQuestions();
    }
	});

  $: {
    if(selected && $questions){
      let found = $questions.find((o) => o.Id === selected.Id)
      if(found){
        selected = found
      }
    }
  }

</script>

<label for="selectQuestion">Pergunta</label>
<select name="select" id="selectQuestion" bind:value={selected}>
  <option value="">Selecione uma Pergunta</option>
  {#each $questions as question}
			<option value={question}>
				{question.Text}
			</option>
		{/each}
</select>