<script lang="ts">
  import { AuthService } from "$lib/auth-service";
  import { goto } from "$app/navigation";
  import { onMount, onDestroy } from "svelte";
  import { isAuthenticated } from "$lib/store";

  let pwd: string;
  let email: string;
  let service = new AuthService();
  let message: string;
  let loading = false;

  onMount(async () => {
    if ($isAuthenticated) {
      goto("/");
    }
  });

  async function doit() {
    console.log('login clicked')
    message = null;
    if (email && pwd) {
      loading = true;
      let res = await service.login(email, pwd);
      if (res.isSuccess) {
        loading = false;
        goto("/");
      } else {
        //console.log(res);
        message = res.message;
        loading = false;
      }
    }
  }

  function keypress(e) {
    if (e && e.keyCode == 13) {
      doit();
    }
  }

  function addUser() {
    goto("/login/cadastrar");
  }
</script>

<svelte:head>
  <title>Login</title>
</svelte:head>

<h1 class="login-header">Login - Cadastro de Alunos</h1>
<div class="grp">
  <div class="login--container ">
    <div class="login" autocomplete="on">
      <label for="full-name-stacked" class="p-form__label">Email</label>
      <input
        type="email"
        id="full-name-stacked"
        name="fullName"
        autocomplete="username"
        bind:value={email}
      />

      <label for="pss" class="p-form__label">Senha</label>
      <input
        type="password"
        id="pss"
        name="pss"
        autocomplete="current-password"
        bind:value={pwd}
        on:keypress={keypress}
      />
      {#if message}
        <div class="p-notification--caution">
          <div class="p-notification__content">
            <p class="p-notification__message">{message}</p>
          </div>
        </div>
      {/if}
      <button class="p-button--brand" on:click={doit}>
        {#if loading}
          <i class="p-icon--spinner u-animation--spin is-light" />
        {/if}
        Login
      </button>
      <button class="" on:click={addUser}> Cadastrar </button>
    </div>
  </div>
</div>

<style>
  .login-header {
    text-align: center;
    margin-top: 1rem;
  }

  .grp {
    margin-top: 2rem;
    display: flex;
    justify-content: center !important;
  }

  .login--container {
    max-width: 24rem;
    outline: dashed 1px black;
  }
  .login--container > * + * {
    margin-top: 8px;
    display: block;
  }

  .login {
    padding: 24px;
  }

  .login > button {
    width: 100%;
  }
</style>
