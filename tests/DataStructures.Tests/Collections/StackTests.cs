using DataStructures.Collections;
using DataStructures.Extensions;
using System;
using System.Text.Json;
using Xunit;

namespace DataStructures.Tests.Collections
{
    public class StackTests
    {
        #region Push
        [Fact(DisplayName = "Ao empilhar, o elemento deve estar no topo")]
        [Trait("Stack", "Push")]
        public void Empilhar_PilhaVazia_ElementoDeveEstarNoTopo()
        {
            //arrange
            var pilha = new Stack<int>();
            var valor = 2;

            //act
            pilha.Push(1);
            pilha.Push(valor);

            //assert
            Assert.Equal(valor, pilha.Peek());
        }
        #endregion

        #region Pop
        [Fact(DisplayName = "Ao tentar desempilhar uma pilha vazia, deve estourar exception")]
        [Trait("Stack", "Pop")]
        public void Desempilhar_PilhaVazia_DeveEstourarException()
        {
            //arrange
            var pilha = new Stack<int>();

            //act & assert
            Assert.Throws<InvalidOperationException>(() => pilha.Pop());
        }

        [Fact(DisplayName = "Ao desempilhar o último elemento, a pilha deve estar vazia")]
        [Trait("Stack", "Pop")]
        public void Desempilhar_PilhaComUmItem_DeveFicarVazia()
        {
            //arrange
            var pilha = new Stack<int>();
            pilha.Push(1);

            //act
            pilha.Pop();

            //assert
            Assert.True(pilha.IsEmpty);
        }

        [Fact(DisplayName = "Ao desempilhar, deve retornar o último elemento da pilha")]
        [Trait("Stack", "Pop")]
        public void Desempilhar_PilhaComItens_DeveRetornarUltimoItem()
        {
            //arrange
            var pilha = new Stack<int>();
            var randomizer = new Random();

            pilha.Push(randomizer.Next());
            pilha.Push(randomizer.Next());
            pilha.Push(randomizer.Next());
            pilha.Push(randomizer.Next());
            pilha.Push(randomizer.Next());

            var ultimoElemento = pilha.Peek();

            //act
            var elemento = pilha.Pop();

            //assert
            Assert.Equal(ultimoElemento, elemento);
        }
        #endregion

        #region New Instance
        [Fact(DisplayName = "Ao criar uma pilha sem itens, o tamanho deve ser igual a zero")]
        [Trait("Stack", "Instânciar")]
        public void Instanciar_PilhaSemItens_TamanhoDeveSerZero()
        {
            //arrange
            var pilha = new Stack<int>();

            //act & assert
            Assert.Equal(0, pilha.Count);
        }
        #endregion

        #region Serialize
        [Fact(DisplayName = "Ao seriaizar, deve conter itens em ordem do último para o primeiro")]
        [Trait("Stack", "Serialize")]
        public void Serialize_PilhaSemItens_TamanhoDeveSerZero()
        {
            //arrange
            var pilha = new Stack<int>();
            pilha.Push(0);
            pilha.Push(10);
            pilha.Push(20);
            //act
            var json = JsonSerializer.Serialize(pilha);

            //assert
            Assert.Equal("[20,10,0]", json);
        }

        [Fact(DisplayName = "Ao deseriaizar, deve conter itens em ordem do último para o primeiro")]
        [Trait("Stack", "Deserialize")]
        public void Deserialize_PilhaSemItens_TamanhoDeveSerZero()
        {
            //arrange
            var pilha = new Stack<int>();
            pilha.Push(0);
            pilha.Push(10);
            pilha.Push(20);
            string json = JsonSerializer.Serialize(pilha);

            //act
            var deserialized = JsonSerializer.Deserialize<Stack<int>>(json);

            //assert
            Assert.Equal(20, deserialized.Pop());
            Assert.Equal(10, deserialized.Pop());
            Assert.Equal(0, deserialized.Pop());
        }
        #endregion

        #region Extensions
        [Fact(DisplayName = "Ao chamar a extensão, deve converter um IEnumerable para uma Stack com os mesmos itens")]
        [Trait("Stack", "ToStack")]
        public void ToStack_DeveRetornarStackComItens()
        {
            //arrange
            var lista = new System.Collections.Generic.List<string> { "A", "B", "C", "D" };

            //act
            var stack = lista.ToStack();

            //assert
            Assert.Equal(lista.Count, stack.Count);

            for (int i = lista.Count - 1; i >= 0; i--)
            {
                Assert.Equal(lista[i], stack.Pop());
            }
        }
        #endregion
    }
}
