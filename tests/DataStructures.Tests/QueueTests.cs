using DataStructures.Collections;
using Xunit;

namespace DataStructures.Tests
{
    public class QueueTests
    {
        #region Peek
        [Fact(DisplayName = "Ao espiar, deve retornar o primeiro elemento da fila")]
        [Trait(nameof(Queue<int>), nameof(Queue<int>.Peek))]
        public void Peek_FilaComItens_DeveRetornarPrimeiroElementoDaFila()
        {
            //arrange
            var fila = new Queue<int>();
            var valor = 2;

            fila.Enqueue(valor);
            fila.Enqueue(1);

            //act
            var observado = fila.Peek();

            //assert
            Assert.Equal(valor, observado);
        }
        #endregion

        #region Enqueue
        [Fact(DisplayName = "Ao enfileirar, o tamanho da fila deve ser incrementado")]
        [Trait(nameof(Queue<int>), nameof(Queue<int>.Enqueue))]
        public void Enqueue_NovaFila_DeveIncrementarTamanho()
        {
            //arrange
            var fila = new Queue<int>();

            //act
            fila.Enqueue(1);
            fila.Enqueue(2);
            fila.Enqueue(3);

            //assert
            Assert.Equal(3, fila.Count);
        }

        [Fact(DisplayName = "Ao enfileirar, a fila não pode ser vazia")]
        [Trait(nameof(Queue<int>), nameof(Queue<int>.Enqueue))]
        public void Enqueue_NovaFila_NaoPodeSerVazia()
        {
            //arrange
            var fila = new Queue<int>();

            //act
            fila.Enqueue(1);

            //assert
            Assert.False(fila.IsEmpty);
        }
        #endregion

        #region Dequeue
        [Fact(DisplayName = "Ao desenfileirar, deve retornar o primeiro elemento adicionado")]
        [Trait(nameof(Queue<int>), nameof(Queue<int>.Dequeue))]
        public void Dequeue_FilaComItens_DeveRetornarPrimeiroElementoDaFila()
        {
            //arrange
            var fila = new Queue<int>();

            var valor1 = 1;
            var valor2 = 2;

            fila.Enqueue(valor2);
            fila.Enqueue(valor1);

            //act
            var primeiroDesenfileirar = fila.Dequeue();
            var segundoDesenfileirar = fila.Dequeue();

            //assert
            Assert.Equal(valor2, primeiroDesenfileirar);
            Assert.Equal(valor1, segundoDesenfileirar);
        }

        [Fact(DisplayName = "Ao desenfileirar todos os itens, a fila deve estar vazia")]
        [Trait(nameof(Queue<int>), nameof(Queue<int>.Dequeue))]
        public void Dequeue_FilaComItens_DeveEstarVazia()
        {
            //arrange
            var fila = new Queue<int>();

            var valor1 = 1;
            var valor2 = 2;

            fila.Enqueue(valor2);
            fila.Enqueue(valor1);

            //act
            fila.Dequeue();
            fila.Dequeue();

            //assert
            Assert.True(fila.IsEmpty);            
        }
        #endregion

        #region Clear
        [Fact(DisplayName = "Ao criar uma fila, o tamanho deve ser zero")]
        [Trait(nameof(Queue<int>), nameof(Queue<int>.Clear))]
        public void Clear_FilaComItens_AoLimparQuantidadeDeRegistrosDeveSerZerado()
        {
            //arrange
            var fila = new Queue<int>();
            fila.Enqueue(1);
            fila.Enqueue(3);
            fila.Enqueue(5);
            fila.Enqueue(9);

            //act
            fila.Clear();

            //assert
            Assert.Equal(0, fila.Count);
        }
        #endregion

        #region New Instance
        [Fact(DisplayName = "Ao criar uma fila, o tamanho deve ser zero")]
        [Trait(nameof(Queue<int>), "Instânciar")]
        public void Instanciar_FilaSemItens_TamanhoDeveSerZero()
        {
            //arrange & act
            var filaSemCapacidade = new Queue<int>();

            //assert
            Assert.Equal(0, filaSemCapacidade.Count);
        }
        #endregion
    }
}
