using DataStructures.Listas;
using System;
using Xunit;

namespace DataStructures.Tests
{
    public class PilhaTests
    {
        [Fact(DisplayName = "Ao empilhar, o elemento deve estar no topo")]
        [Trait("Pilhas", "Empilhar")]
        public void Empilhar_PilhaVazia_ElementoDeveEstarNoTopo()
        {
            //arrange
            var pilha = new Pilha<int>();
            var valor = 2;

            //act
            pilha.Empilhar(1);
            pilha.Empilhar(valor);

            //assert
            Assert.Equal(valor, pilha.Topo);
        }

        [Fact(DisplayName = "Tamanho da pilha deve ser igual a quantidade de empilhamentos")]
        [Trait("Pilhas", "Empilhar")]
        public void Empilhar_PilhaComItens_TamanhoDeveSerIgualAQuantidadeDeEmpilhamentos()
        {
            //arrange
            var tamanho = 5;
            var pilha = new Pilha<int>(tamanho);
            var randomizer = new Random();

            //act
            pilha.Empilhar(randomizer.Next());
            pilha.Empilhar(randomizer.Next());
            pilha.Empilhar(randomizer.Next());
            pilha.Empilhar(randomizer.Next());
            pilha.Empilhar(randomizer.Next());

            //assert            
            Assert.Equal(tamanho, pilha.Tamanho);
        }

        [Fact(DisplayName = "Ao Empilhar mais itens do que o tamanho, deve estourar exception")]
        [Trait("Pilhas", "Empilhar")]        
        public void Empilhar_PilhaComTamanho_DeveEstourarExceptionAoExceder()
        {
            //arrange
            var tamanho = 4;
            var pilha = new Pilha<int>(tamanho);
            var randomizer = new Random();

            //act
            pilha.Empilhar(randomizer.Next());
            pilha.Empilhar(randomizer.Next());
            pilha.Empilhar(randomizer.Next());
            pilha.Empilhar(randomizer.Next());

            //assert            
            Assert.Throws<InvalidOperationException>(() => pilha.Empilhar(randomizer.Next()));
        }

        [Fact(DisplayName = "Ao tentar desempilhar uma pilha vazia, deve estourar exception")]
        [Trait("Pilhas", "Desempilhar")]
        public void Desempilhar_PilhaVazia_DeveEstourarException()
        {
            //arrange
            var pilha = new Pilha<int>();

            //act & assert
            Assert.Throws<InvalidOperationException>(() => pilha.Desempilhar());
        }

        [Fact(DisplayName = "Ao desempilhar o último elemento, a pilha deve estar vazia")]
        [Trait("Pilhas", "Desempilhar")]
        public void Desempilhar_PilhaComUmItem_DeveFicarVazia()
        {
            //arrange
            var pilha = new Pilha<int>();
            pilha.Empilhar(1);

            //act
            pilha.Desempilhar();

            //assert
            Assert.True(pilha.Vazia);
        }
    }
}
