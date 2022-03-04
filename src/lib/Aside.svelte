<script lang="ts">
	import { isAddOpen } from '$lib/store';
</script>

<!-- {#if $isAddOpen} -->
<aside class="l-aside" id="aside-panel" class:is-collapsed={!$isAddOpen}>
	<div class="p-panel">
		<div class="p-panel__header">
			<h4 class="p-panel__title">Novo Aluno</h4>
			<div class="p-panel__controls">
				<button
					on:click={() => isAddOpen.set(false)}
					class="p-button--base js-aside-close u-no-margin--bottom has-icon"
					><i class="p-icon--close" /></button
				>
			</div>
		</div>
		<div class="p-panel__content">
			<div class="p-form p-form--stacked">
				<div class="p-form__group row">
					<div class="col-4">
						<label for="full-name-stacked" class="p-form__label">Nome</label>
					</div>

					<div class="col-8">
						<div class="p-form__control">
							<input type="text" id="full-name-stacked" name="fullName" autocomplete="name" />
						</div>
					</div>
				</div>

				<div class="p-form__group row">
					<div class="col-4">
						<label for="matr" class="p-form__label">Matrícula</label>
					</div>

					<div class="col-8">
						<div class="p-form__control">
							<input
								type="text"
								id="matr"
								name="username-stacked"
								autocomplete="username"
								aria-describedby="exampleHelpTextMessage"
							/>
							<!-- <p class="p-form-help-text" id="exampleHelpTextMessage">30 characters or fewer.</p> -->
						</div>
					</div>
				</div>

				<div class="p-form__group row">
					<div class="col-4">
						<label for="pres" class="p-form__label"> Localidade </label>
					</div>

					<div class="col-8">
						<div class="p-form__control">
							<input
								type="text"
								id="pres"
								class="p-form-validation__input"
								name="pres"
								autocomplete="username"
							/>
              <p class="p-form-help-text" id="exampleHelpTextMessage">Presídio - Caso não exista adicione um novo</p>
						</div>
					</div>
				</div>

				<div class="p-form__group row">
					<div class="col-4">
						<label for="raio" class="p-form__label"> Raio </label>
					</div>

					<div class="col-8">
						<div class="p-form__control">
							<input
								type="text"
								id="raio"
								class="p-form-validation__input"
								name="raio"
								autocomplete="raio"
							/>
						</div>
					</div>
				</div>

				<div class="p-form__group row">
					<div class="col-4">
						<label for="cela" class="p-form__label"> Cela </label>
					</div>

					<div class="col-8">
						<div class="p-form__control">
							<input
								type="text"
								id="cela"
								class="p-form-validation__input"
								aria-invalid="true"
								name="cela"
								autocomplete="cela"
							/>
						</div>
					</div>
				</div>

				<div class="p-form__group row">
					<div class="col-4">
						<label for="pav" class="p-form__label"> Pavilhão </label>
					</div>

					<div class="col-8">
						<div class="p-form__control">
							<input
								type="text"
								id="pav"
								class="p-form-validation__input"
								name="pav"
								autocomplete="pav"
								aria-describedby="username-error-message-stacked"
							/>
						</div>
					</div>
				</div>

				<div class="p-form__group row">
					<div class="col-4">
						<label for="xad" class="p-form__label"> Xadrez </label>
					</div>

					<div class="col-8">
						<div class="p-form__control">
							<input
								type="text"
								id="xad"
								class="p-form-validation__input"
								aria-invalid="true"
								name="username-stackederror"
								autocomplete="username"
								aria-describedby="username-error-message-stacked"
							/>
						</div>
					</div>
				</div>

				<div class="p-form__group row">
					<div class="col-4">
						<label for="address-optional-stacked0" class="p-form__label">Quem indicou?</label>
					</div>

					<div class="col-8">
						<div class="p-form__control">
							<input type="text" name="ind" autocomplete="ind" />
						</div>
					</div>
				</div>

				<div class="p-form__group row">
					<div class="col-4">
						<label for="obs" class="p-form__label">Observações</label>
					</div>

					<div class="col-8">
						<div class="p-form__control">
							<input
								type="text"
								id="obs"
								name="address-optional-stacked"
								autocomplete="address-line3"
							/>
						</div>
					</div>
				</div>

				<!-- <fieldset>
					<label for="list-input-1">Endereço</label>
					<input
						placeholder="Rua"
						id="list-input-1"
						name="list-input-1"
						type="text"
						autocomplete="addr"
					/>
					<label for="list-input-2">Número</label>
					<input
						placeholder="0"
						id="list-input-2"
						name="list-input-2"
						type="text"
						autocomplete="addr--n"
					/>
					<label for="list-input-3">CEP</label>
					<input
						placeholder="13184-030"
						id="list-input-3"
						type="text"
						autocomplete="cep"
					/>
				</fieldset> -->

				<div class="row">
					<div class="col-6">
						<button class="p-button u-float-left" name="add-details">Adicionar Localidade</button>
					</div>
					<div class="col-6">
						<button class="p-button--positive u-float-right" name="add-details">Salvar</button>
					</div>
				</div>
			</div>
		</div>
	</div>
</aside>
<!-- {/if} -->
