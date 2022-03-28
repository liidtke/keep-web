<script lang="ts">
	import { page } from '$app/stores';
	import logo from './keep-logo.svg';
</script>

<header id="navigation" class="p-navigation">
  <div class="p-navigation__row">
    <div class="p-navigation__banner">
      <div class="p-navigation__logo">
        <a class="p-navigation__item" sveltekit:prefetch href="/">
          <img class="p-navigation__image" src="{logo}" alt="logo">
        </a>
      </div>
      <a href="#navigation" class="p-navigation__toggle--open" title="menu">Menu</a>
      <a href="#navigation-closed" class="p-navigation__toggle--close" title="close menu">Close menu</a>
    </div>

    <nav class="p-navigation__nav" aria-label="Example main navigation">
      <ul class="p-navigation__items">
        <li class="p-navigation__item" class:is-selected={$page.url.pathname === "/"} >
          <a class="p-navigation__link" sveltekit:prefetch href="/">Início</a>
        </li>
        <li class="p-navigation__item" class:is-selected={$page.url.pathname === "/alunos"} >
          <a class="p-navigation__link" sveltekit:prefetch href="/alunos">Alunos</a>
        </li>
        <li class="p-navigation__item" class:is-selected={$page.url.pathname === "/cursos"} >
          <a class="p-navigation__link" sveltekit:prefetch href="/cursos">Cursos</a>
        </li>
				<li class="p-navigation__item" class:is-selected={$page.url.pathname === "/localidades"} >
          <a class="p-navigation__link" sveltekit:prefetch href="/localidades">Localidades</a>
        </li>
        <li class="p-navigation__item" class:is-selected={$page.url.pathname === "/sobre"} >
          <a class="p-navigation__link" sveltekit:prefetch href="/sobre">Sobre</a>
        </li>
        
      </ul>
    </nav>
  </div>
</header>

