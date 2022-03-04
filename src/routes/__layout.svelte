<script lang="ts">
	import Header from '$lib/header/Header.svelte';
	import Aside from '$lib/Aside.svelte';
	import '../app.css';
</script>


<!-- <div class="row"> -->
	<main class="l-main">
			 <Header />
			 <div class="row">
				 <slot />
				</div>
			</main>

			<Aside />

		<!-- </div> -->

		<!-- <footer class="p-strip--light">
			<div class="row p-content__row">
				<div class="col-12">
					<p class="u-no-margin--bottom">© 2022 Canonical Ltd. Ubuntu and Canonical are registered trademarks of Canonical Ltd.</p>
					<nav aria-label="Footer navigation">
						<ul class="p-inline-list--middot u-no-margin--bottom">
							<li class="p-inline-list__item">
								<a href="https://github.com/canonical-web-and-design/vanilla-framework/releases/latest">Vanilla framework v3.1.0</a>
							</li>
							<li class="p-inline-list__item">
								<a href="https://www.ubuntu.com/legal">Legal info</a>
							</li>
							<li class="p-inline-list__item">
								<a href="https://github.com/canonical-web-and-design/vanilla-framework/issues/new/choose">Report a bug</a>
							</li>
							<li class="p-inline-list__item">
								<a href="#tracker-settings" class="js-revoke-cookie-manager">Manage your tracker settings</a>
							</li>
						</ul>
						<span class="u-off-screen">
							<a href="#">Go to the top of the page</a>
						</span>
					</nav>
				</div>
			</div>
		</footer> -->

