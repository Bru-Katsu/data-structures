using DataStructures.Listas;
using System;
using Xunit;

namespace DataStructures.Tests
{
    public class StackTests
    {
        [Fact(DisplayName = "Ao empilhar, o elemento deve estar no topo")]
        [Trait(nameof(Stack<int>), nameof(Stack<int>.Push))]
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

        [Fact(DisplayName = "Ao tentar desempilhar uma pilha vazia, deve estourar exception")]
        [Trait(nameof(Stack<int>), nameof(Stack<int>.Pop))]
        public void Desempilhar_PilhaVazia_DeveEstourarException()
        {
            //arrange
            var pilha = new Stack<int>();

            //act & assert
            Assert.Throws<InvalidOperationException>(() => pilha.Pop());
        }

        [Fact(DisplayName = "Ao desempilhar o último elemento, a pilha deve estar vazia")]
        [Trait(nameof(Stack<int>), nameof(Stack<int>.Pop))]
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
        [Trait(nameof(Stack<int>), nameof(Stack<int>.Pop))]
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

        [Fact(DisplayName = "Ao criar uma pilha sem itens, o tamanho deve ser igual a zero")]
        [Trait(nameof(Stack<int>), "Instânciar")]
        public void Instanciar_PilhaSemItens_TamanhoDeveSerZero()
        {
            //arrange
            var pilha = new Stack<int>();

            //act & assert
            Assert.Equal(0, pilha.Count);
        }
    }
}
