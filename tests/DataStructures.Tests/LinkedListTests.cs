
using DataStructures.Listas;
using System;
using System.Linq;
using Xunit;

namespace DataStructures.Tests
{
    public class LinkedListTests
    {
        [Fact(DisplayName = "Ao adicionar o item, deve estar ao final da lista"), Trait(nameof(LinkedList<int>), nameof(LinkedList<int>.Append))]
        public void AdicionarAoFinal_ItemDeveEstarNoUltimoNode()
        {
            //arrange
            var list = new LinkedList<int>()
            {
                1, 15
            };

            //act
            list.Append(16);
            var retorno = list.GetValueAt(list.Count - 1);

            //assert            
            Assert.Equal(16, retorno);
        }

        [Fact(DisplayName = "Ao adicionar o item, deve estar ao inicio da lista"), Trait(nameof(LinkedList<int>), nameof(LinkedList<int>.Prepend))]
        public void AdicionarAoInicio_ItemDeveEstarNoPrimeiroNode()
        {
            //arrange
            var list = new LinkedList<int>() { 1, 15 };

            //act
            list.Prepend(16);

            var retorno = list.GetValueAt(0);

            //assert            
            Assert.Equal(16, retorno);
        }

        [Theory(DisplayName = "Ao inserir o item, deve estar na posição definida"), Trait(nameof(LinkedList<int>), nameof(LinkedList<int>.InsertAt))]
        [InlineData(1, 20)]
        [InlineData(2, 140)]
        [InlineData(0, 100)]
        [InlineData(3, 99)]
        public void Inserir_ItemDeveEstarNoPosicaoDefinida(int posicao, int valor)
        {
            //arrange
            var list = new LinkedList<int>() { 16, 1, 15 };

            //act
            list.InsertAt(posicao, valor);
            var retorno = list.GetValueAt(posicao);

            //assert            
            Assert.Equal(valor, retorno);
        }

        [Fact(DisplayName = "Ao inserir o item em uma posição maior do que a última possível, deve retornar exception"), Trait(nameof(LinkedList<int>), nameof(LinkedList<int>.InsertAt))]
        public void Inserir_PosicaoMaiorQueAdicionarEmUltimaPosicao_DeveRetornarException()
        {
            //arrange
            var list = new LinkedList<int>() { 16, 1, 15 };

            //act & assert                      
            Assert.Throws<ArgumentOutOfRangeException>(() => list.InsertAt(4, 99));
        }

        [Fact(DisplayName = "Ao inserir o item em uma posição negativa, deve retornar exception"), Trait(nameof(LinkedList<int>), nameof(LinkedList<int>.InsertAt))]
        public void Inserir_PosicaoNegativa_DeveRetornarException()
        {
            //arrange
            var list = new LinkedList<int>() { 16, 1, 15 };

            //act & assert                      
            Assert.Throws<ArgumentOutOfRangeException>(() => list.InsertAt(-2, 99));
        }

        [Fact(DisplayName = "Ao percorrer o IEnumerable, deve retornar o valor se existir na lista"), Trait(nameof(LinkedList<int>), "IEnumerable")]
        public void Pesquisar_IEnumerableDeveRetornarItemCorreto()
        {
            //arrange
            var list = new LinkedList<int>() { 0, 1, 2, 3 };
            list.Append(1);
            list.Append(15);
            list.Prepend(16);

            //act & assert            
            Assert.Equal(16, list.FirstOrDefault(x => x.Equals(16)));
            Assert.Equal(15, list.FirstOrDefault(x => x.Equals(15)));
            Assert.Equal(1, list.FirstOrDefault(x => x.Equals(1)));
        }
    }
}
