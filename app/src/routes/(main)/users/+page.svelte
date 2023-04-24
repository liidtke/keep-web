<script lang="ts">
  import { service, snackMessage, showMessage } from "$lib/store";
  import type { IUser } from "$lib/types";
  import { onMount } from "svelte";

  let users: IUser[] = [];
  let message:string = null;

  onMount(async () => {
    await getAll();
  });

  async function getAll() {
    users = await $service.getUsers();
  }

  function edit(user){
    user._edit = true;
    users = users;
  }

  function cancel(user){
    user._edit = false;
    users = users;
   getAll();
  }

  async function save(user){
    message = null;
    let res = await $service.saveUser(user);
    if(res.isSuccess){
      snackMessage.set("Salvo com sucesso");
      showMessage.set(true);
      getAll();
    }
    else {
      message = res.message;
    }

  }

</script>

<div class="p-strip">
  <h1>Usuários</h1>
  <hr />

  {#if users && users.length}
    <table>
      <thead>
        <tr>
          <th>Nome</th>
          <th>Email</th>
          <th>Ativo</th>
          <th />
        </tr>
      </thead>
      <tbody>
        {#each users as user}
        {#if user._edit}
        
        <tr>
          <td>
            <input type="text" name="" id="" bind:value={user.Name}>
          </td>
          <td>
            <input type="text" bind:value={user.Email}>
          </td>
          <td>
            <input type="checkbox" id="chk" name="chk" bind:checked={user.IsVerified} />
          </td>
          <td>
            <button class="button p-button--positive u-float-right" on:click={()=> save(user)}>Salvar</button>
            <button class="button p-button u-float-right" on:click={() => cancel(user)}>Cancelar</button>
          </td>
        </tr>

        {:else}
        <tr>
          <td>{user.Name ?? ''}</td>
          <td>{user.Email ?? ''}</td>
          <td>{user.IsVerified ? 'Ativo': 'Inativo'}</td>
          <td>
            <button
            class="u-toggle is-dense"
            aria-controls="expanded-row"
            aria-expanded="false"
            data-shown-text="Hide"
            data-hidden-text="Show"
            on:click={() => edit(user)}
            >Editar</button
            >
          </td>
        </tr>
          {/if}
        {/each}
      </tbody>
    </table>
  {/if}
</div>


<style>
  .button{
  margin-left: .1rem;
}

.email{
  min-width: 250px;
}
</style>