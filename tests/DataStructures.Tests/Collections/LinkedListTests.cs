using DataStructures.Collections;
using DataStructures.Extensions;
using System;
using System.Linq;
using System.Text.Json;
using Xunit;

namespace DataStructures.Tests.Collections
{
    public class LinkedListTests
    {
        #region AddLast
        [Fact(DisplayName = "Ao adicionar o item, deve estar ao final da lista"), Trait("LinkedList", "AddLast")]
        public void AdicionarAoFinal_ItemDeveEstarNoUltimoNode()
        {
            //arrange
            var list = new LinkedList<int>()
            {
                1, 15
            };

            //act
            list.AddLast(16);
            var retorno = list.GetItemAt(list.Count - 1);

            //assert            
            Assert.Equal(16, retorno);
        }

        [Fact(DisplayName = "Ao adicionar o item, deve incrementar o tamanho"), Trait("LinkedList", "AddLast")]
        public void AdicionarAoFinal_TamanhoDeveSerIncrementado()
        {
            //arrange
            var list = new LinkedList<int>()
            {
                1, 15
            };

            //act
            list.AddLast(16);
            list.AddLast(17);

            //assert            
            Assert.Equal(4, list.Count);
        }
        #endregion

        #region AddFirst
        [Fact(DisplayName = "Ao adicionar o item, deve estar ao inicio da lista"), Trait("LinkedList", "AddFirst")]
        public void AdicionarAoInicio_ItemDeveEstarNoPrimeiroNode()
        {
            //arrange
            var list = new LinkedList<int>() { 1, 15 };

            //act
            list.AddFirst(16);

            var retorno = list.GetItemAt(0);

            //assert            
            Assert.Equal(16, retorno);
        }

        [Fact(DisplayName = "Ao adicionar o item, deve incrementar o tamanho"), Trait("LinkedList", "AddFirst")]
        public void AdicionarAoInicio_TamanhoDeveSerIncrementado()
        {
            //arrange
            var list = new LinkedList<int>()
            {
                1, 15
            };

            //act
            list.AddFirst(16);
            list.AddFirst(17);

            //assert            
            Assert.Equal(4, list.Count);
        }
        #endregion

        #region Insert
        [Theory(DisplayName = "Ao inserir o item, deve estar na posição definida"), Trait("LinkedList", "InsertAt")]
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
            var retorno = list.GetItemAt(posicao);

            //assert            
            Assert.Equal(valor, retorno);
        }

        [Fact(DisplayName = "Ao inserir o item em uma posição maior do que a última possível, deve retornar exception"), Trait("LinkedList", "InsertAt")]
        public void Inserir_PosicaoMaiorQueAdicionarEmUltimaPosicao_DeveRetornarException()
        {
            //arrange
            var list = new LinkedList<int>() { 16, 1, 15 };

            //act & assert                      
            Assert.Throws<ArgumentOutOfRangeException>(() => list.InsertAt(4, 99));
        }

        [Fact(DisplayName = "Ao inserir o item em uma posição negativa, deve retornar exception"), Trait("LinkedList", "InsertAt")]
        public void Inserir_PosicaoNegativa_DeveRetornarException()
        {
            //arrange
            var list = new LinkedList<int>() { 16, 1, 15 };

            //act & assert                      
            Assert.Throws<ArgumentOutOfRangeException>(() => list.InsertAt(-2, 99));
        }

        [Fact(DisplayName = "Ao inserir o item em uma posição válida, o tamanho deve ser incrementado"), Trait("LinkedList", "InsertAt")]
        public void Inserir_PosicaoValida_DeveIncrementarTamanho()
        {
            //arrange
            var list = new LinkedList<int>() { 16, 1, 15 };

            //act 
            list.InsertAt(1, 50);
            list.InsertAt(0, 99);

            //assert
            Assert.Equal(5, list.Count);
        }
        #endregion

        #region Remove
        [Fact(DisplayName = "Ao remover o item em uma posição maior do que a última, deve retornar exception"), Trait("LinkedList", "RemoveAt")]
        public void Remover_PosicaoMaiorQueAdicionarEmUltimaPosicao_DeveRetornarException()
        {
            //arrange
            var list = new LinkedList<int>() { 16, 1, 15 };

            //act & assert                      
            Assert.Throws<ArgumentOutOfRangeException>(() => list.InsertAt(4, 99));
        }

        [Fact(DisplayName = "Ao remover o item em uma posição negativa, deve retornar exception"), Trait("LinkedList", "RemoveAt")]
        public void Remover_PosicaoNegativa_DeveRetornarException()
        {
            //arrange
            var list = new LinkedList<int>() { 16, 1, 15 };

            //act & assert                      
            Assert.Throws<ArgumentOutOfRangeException>(() => list.RemoveAt(-1));
        }

        [Fact(DisplayName = "Ao remover um item em uma posição válida, o tamanho deve ser decrementado"), Trait("LinkedList", "RemoveAt")]
        public void Remover_PosicaoValida_DeveDecrementarTamanho()
        {
            //arrange
            var list = new LinkedList<int>() { 16, 1, 15 };

            //act 
            list.RemoveAt(1);

            //assert
            Assert.Equal(2, list.Count);
        }
        #endregion

        #region Get
        #endregion

        #region Reverse
        [Fact(DisplayName = "Ao inverter a lista"), Trait("LinkedList", "Reverse")]
        public void Reverter_PosicaoValida_DeveDecrementarTamanho()
        {
            //arrange
            var list = new LinkedList<int>() { 16, 1, 15 };

            //act 
            list.Reverse();

            //assert
            Assert.Equal(3, list.Count);
        }
        #endregion

        #region Enumerable
        [Fact(DisplayName = "Ao percorrer o IEnumerable, deve retornar o valor se existir na lista"), Trait("LinkedList", "IEnumerable")]
        public void Pesquisar_IEnumerableDeveRetornarItemCorreto()
        {
            //arrange
            var list = new LinkedList<int>() { 0, 1, 2, 3 };
            list.AddLast(1);
            list.AddLast(15);
            list.AddFirst(16);

            //act & assert            
            Assert.Equal(16, list.FirstOrDefault(x => x.Equals(16)));
            Assert.Equal(15, list.FirstOrDefault(x => x.Equals(15)));
            Assert.Equal(1, list.FirstOrDefault(x => x.Equals(1)));
        }
        #endregion

        #region Serialize
        [Fact(DisplayName = "Ao seriaizar, deve conter itens em ordem"), Trait("LinkedList", "Serialize")]
        public void Serialize_PilhaSemItens_TamanhoDeveSerZero()
        {
            //arrange
            var pilha = new LinkedList<int>();
            pilha.Add(0);
            pilha.Add(10);
            pilha.Add(20);
            //act
            var json = JsonSerializer.Serialize(pilha);

            //assert
            Assert.Equal("[0,10,20]", json);
        }

        [Fact(DisplayName = "Ao deserializar, deve conter itens em ordem"), Trait("LinkedList", "Deserialize")]
        public void Deserialize_LinkedList_TamanhoDeveSerZero()
        {
            //arrange
            var pilha = new LinkedList<int>();
            pilha.Add(0);
            pilha.Add(10);
            pilha.Add(20);

            var json = JsonSerializer.Serialize(pilha);

            //act
            var deserialized = JsonSerializer.Deserialize<LinkedList<int>>(json);

            //assert
            Assert.Equal(0, deserialized.RemoveFirst());
            Assert.Equal(10, deserialized.RemoveFirst());
            Assert.Equal(20, deserialized.RemoveFirst());
        }
        #endregion

        #region Extensions
        [Fact(DisplayName = "Ao chamar a extensão, deve converter um IEnumerable para uma Queue com os mesmos itens")]
        [Trait("LinkedList", "ToSingleLinkedList")]
        public void ToLinkedList_DeveRetornarLinkedListComItens()
        {
            //arrange
            var lista = new System.Collections.Generic.List<string> { "A", "B", "C", "D" };

            //act
            var linkedList = lista.ToSingleLinkedList();

            //assert
            Assert.Equal(lista.Count, linkedList.Count);

            foreach (var item in lista)
            {
                Assert.Equal(item, linkedList.RemoveFirst());
            }
        }
        #endregion
    }
}
