using DataStructures.Listas;
using System;
using Xunit;

namespace DataStructures.Tests
{
    public class QueueTests
    {
        [Fact(DisplayName = "Ao espiar, deve retornar o primeiro elemento da fila")]
        [Trait("Queue", "Espiar")]
        public void Espiar_FilaComItens_DeveRetornarPrimeiroElementoDaFila()
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

        [Fact(DisplayName = "Ao enfileirar, o tamanho da fila deve ser incrementado")]
        [Trait("Queue", "Enfileirar")]
        public void Enfileirar_NovaFila_DeveIncrementarTamanho()
        {
            //arrange
            var fila = new Queue<int>();

            //act
            fila.Enqueue(1);
            fila.Enqueue(2);
            fila.Enqueue(3);

            //assert
            Assert.Equal(3, fila.Size);
        }

        [Fact(DisplayName = "Ao enfileirar, a fila não pode ser vazia")]
        [Trait("Queue", "Enfileirar")]
        public void Enfileirar_NovaFila_NaoPodeSerVazia()
        {
            //arrange
            var fila = new Queue<int>();

            //act
            fila.Enqueue(1);

            //assert
            Assert.False(fila.IsEmpty);
        }

        [Fact(DisplayName = "Ao desenfileirar, deve retornar o primeiro elemento adicionado")]
        [Trait("Queue", "Desenfileirar")]
        public void Desenfileirar_FilaComItens_DeveRetornarPrimeiroElementoDaFila()
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
        [Trait("Queue", "Desenfileirar")]
        public void Desenfileirar_FilaComItens_DeveEstarVazia()
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

        [Fact(DisplayName = "Ao criar uma fila com a capacidade negativa, deve estourar exception")]
        [Trait("Queue", "Instânciar")]
        public void Instanciar_FilaComCapacidadeNegativa_DeveEstourarException()
        {
            //arrange
            var capacidade = -1;

            //act & assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Queue<int>(capacidade));
        }

        [Fact(DisplayName = "Ao criar uma fila, o tamanho deve ser zero")]
        [Trait("Queue", "Instânciar")]
        public void Instanciar_FilaSemItens_TamanhoDeveSerZero()
        {
            //arrange & act
            var filaSemCapacidade = new Queue<int>();
            var filaComCapacidade = new Queue<int>(5);

            //assert
            Assert.Equal(0, filaSemCapacidade.Size);
            Assert.Equal(0, filaComCapacidade.Size);
        }

        [Fact(DisplayName = "Ao criar um array a partir da fila, deve respeitar a mesma ordem interna")]
        [Trait("Queue", "ToArray")]
        public void ConverterArray_FilaComItens_DeveTerAMesmaSequenciaDeItens()
        {
            //arrange
            var fila = new Queue<int>();

            fila.Enqueue(1);
            fila.Enqueue(2);
            fila.Enqueue(3);
            fila.Enqueue(4);
            fila.Enqueue(5);
            fila.Enqueue(6);

            //act
            var arr = fila.ToArray();

            //assert
            for (int index = 0; index < fila.Size; index++)
            {
                var value = fila.Dequeue();
                Assert.Equal(value, arr[index]);
            }
        }
    }
}
