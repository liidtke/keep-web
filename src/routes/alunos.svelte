<script lang="ts">
  import { isAddOpen } from '$lib/store';
</script>

<div class="p-strip">
  <h1>Alunos</h1>
  <hr>

  <button class="p-button--brand has-icon" on:click={() => isAddOpen.set(true)}>
    <i class="p-icon--plus"></i>
    <span>Novo Aluno</span>
  </button>

  <table aria-label="alunos">
    <thead>
      <tr>
        <th>Nome</th>
        <th>Matrícula</th>
        <th>Número</th>
        <th>Admissão</th>
        <th></th>
      </tr>
    </thead>
    <tbody>
      <tr>
        <td>José Carlos Silva</td>
        <td>VX-12PP</td>
        <th>218</th>
        <th>02/12/2021</th>
        <td>
          <button class="u-toggle is-dense" aria-controls="expanded-row" aria-expanded="false" data-shown-text="Hide" data-hidden-text="Show">Detalhes</button>
        </td>
      </tr>
      <tr>
        <td>Mário Gonçalves Lima</td>
        <td>1U-12444</td>
        <th>219</th>
        <th>03/12/2021</th>
        <td>
          <button class="u-toggle is-dense" aria-controls="expanded-row" aria-expanded="false" data-shown-text="Hide" data-hidden-text="Show">Detalhes</button>
        </td>
      </tr>
      <tr>
        <td>Flávio Carvalho</td>
        <td>1X-1212</td>
        <th>220</th>
        <th>01/01/2022</th>
        <td>
          <button class="u-toggle is-dense" aria-controls="expanded-row" aria-expanded="false" data-shown-text="Hide" data-hidden-text="Show">Detalhes</button>
        </td>
      </tr>
      <tr>
        <td>Grabiel Silva</td>
        <td>2X-1212</td>
        <th>221</th>
        <th>01/01/2022</th>
        <td>
          <button class="u-toggle is-dense" aria-controls="expanded-row" aria-expanded="false" data-shown-text="Hide" data-hidden-text="Show">Detalhes</button>
        </td>
      </tr>
    </tbody>
    <!-- <tfoot>
      <tr>
        <th>One-time price</th>
        <td>$75,000</td>
        <td>$150,000</td>
      </tr>
    </tfoot> -->
  </table>
  
</div>

