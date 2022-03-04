<script lang="ts">
	function addCourse() {}
</script>

<div class="p-strip">
	<h1>Cursos</h1>
	<hr />
  
  <button class="p-button--brand has-icon">
    <i class="p-icon--plus" />
    <span>Novo Curso</span>
  </button>

  <table aria-label="alunos">
    <thead>
      <tr>
        <th>Nome</th>
        <th>Número de Lições</th>
        <th>Status</th>
        <th></th>
      </tr>
    </thead>
    <tbody>
      <tr>
        <td>Encontro com a Vida</td>
        <th>20</th>
        <th>Ativo</th>
        <td>
          <button class="u-toggle is-dense" aria-controls="expanded-row" aria-expanded="false" data-shown-text="Hide" data-hidden-text="Show">Detalhes</button>
        </td>
      </tr>
      <tr>
        <td>A Bíblia Fala</td>
        <th>24</th>
        <th>Ativo</th>
        <td>
          <button class="u-toggle is-dense" aria-controls="expanded-row" aria-expanded="false" data-shown-text="Hide" data-hidden-text="Show">Detalhes</button>
        </td>
      </tr>
      <tr>
        <td>As Revelações do Apocalipse</td>
        <th>25</th>
        <th>Ativo</th>
        <td>
          <button class="u-toggle is-dense" aria-controls="expanded-row" aria-expanded="false" data-shown-text="Hide" data-hidden-text="Show">Detalhes</button>
        </td>
      </tr>
    </tbody>
  </table>

</div>
