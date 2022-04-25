<script lang="ts">
  import { service, snackMessage, showMessage } from "$lib/store";
  import EmailInput from "$lib/components/EmailInput.svelte"

  let user: any = {};
  let message: string = null;

  async function save() {
    message = null;
    let result = await $service.saveUser(user);
    if(result.isSuccess){
      snackMessage.set("Usuário adicionado com sucesso");
      showMessage.set(true);
      clean();
    }
    else message = result.message;
  }

  function cancel(){
   clean();
   
  }

  function clean(){
    message = null;
    user = {};
  }

</script>

<div class="p-strip">
  <h1>Novo Usuário</h1>
  <hr />

  <div class="p-form is-dense">
    {#if message}
      <div class="p-notification--caution">
        <div class="p-notification__content">
          <h5 class="p-notification__title">Erro ao Salvar</h5>
          <p class="p-notification__message">{message}</p>
        </div>
      </div>
    {/if}

    <label for="name">Nome</label>
    <input type="text" id="name" bind:value={user.Name} />

    <EmailInput bind:value={user.Email}></EmailInput>
    <!-- <label for="email">Email</label> -->
    <!-- <input type="email" id="email" bind:value={user.Email} /> -->

    <label for="pass">Senha</label>
    <input type="password" id="pass" bind:value={user.Password} />
  </div>

  <div class="row">
    <div class="col-12">
      <button class="p-button" on:click={cancel}>Cancelar</button>
      <button class="p-button--positive" on:click={save}>Salvar</button>
    </div>
  </div>

</div>
