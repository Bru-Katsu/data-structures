﻿using DataStructures.Collections;
using DataStructures.Extensions;
using System;
using System.Linq;
using System.Text.Json;
using Xunit;

namespace DataStructures.Tests.Collections
{
    public class DoubleDoubleLinkedListTests
    {
        #region Add Last
        [Fact(DisplayName = "Ao adicionar o item, deve estar ao final da lista"), Trait("DoubleLinkedList", "AddLast")]
        public void AdicionarAoFinal_ItemDeveEstarNoUltimoNode()
        {
            //arrange
            var list = new DoubleLinkedList<int>()
            {
                1, 15
            };

            //act
            list.AddLast(16);
            var retorno = list.GetItemAt(list.Count - 1);

            //assert            
            Assert.Equal(16, retorno);
        }

        [Fact(DisplayName = "Ao adicionar o item, deve incrementar o tamanho"), Trait("DoubleLinkedList", "AddLast")]
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

        #region Add First
        [Fact(DisplayName = "Ao adicionar o item, deve estar ao inicio da lista"), Trait("DoubleLinkedList", "AddFirst")]
        public void AdicionarAoInicio_ItemDeveEstarNoPrimeiroNode()
        {
            //arrange
            var list = new DoubleLinkedList<int>() { 1, 15 };

            //act
            list.AddFirst(16);

            var retorno = list.GetItemAt(0);

            //assert            
            Assert.Equal(16, retorno);
        }

        [Fact(DisplayName = "Ao adicionar o item, deve incrementar o tamanho"), Trait("DoubleLinkedList", "AddFirst")]
        public void AdicionarAoInicio_TamanhoDeveSerIncrementado()
        {
            //arrange
            var list = new DoubleLinkedList<int>()
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
        [Theory(DisplayName = "Ao inserir o item, deve estar na posição definida"), Trait("DoubleLinkedList", "InsertAt")]
        [InlineData(1, 20)]
        [InlineData(2, 140)]
        [InlineData(0, 100)]
        [InlineData(3, 99)]
        public void Inserir_ItemDeveEstarNoPosicaoDefinida(int posicao, int valor)
        {
            //arrange
            var list = new DoubleLinkedList<int>() { 16, 1, 15 };

            //act
            list.InsertAt(posicao, valor);
            var retorno = list.GetItemAt(posicao);

            //assert            
            Assert.Equal(valor, retorno);
        }

        [Fact(DisplayName = "Ao inserir o item em uma posição maior do que a última possível, deve retornar exception"), Trait("DoubleLinkedList", "InsertAt")]
        public void Inserir_PosicaoMaiorQueAdicionarEmUltimaPosicao_DeveRetornarException()
        {
            //arrange
            var list = new DoubleLinkedList<int>() { 16, 1, 15 };

            //act & assert                      
            Assert.Throws<ArgumentOutOfRangeException>(() => list.InsertAt(4, 99));
        }

        [Fact(DisplayName = "Ao inserir o item em uma posição negativa, deve retornar exception"), Trait("DoubleLinkedList", "InsertAt")]
        public void Inserir_PosicaoNegativa_DeveRetornarException()
        {
            //arrange
            var list = new DoubleLinkedList<int>() { 16, 1, 15 };

            //act & assert                      
            Assert.Throws<ArgumentOutOfRangeException>(() => list.InsertAt(-2, 99));
        }

        [Fact(DisplayName = "Ao inserir o item em uma posição válida, o tamanho deve ser incrementado"), Trait("DoubleLinkedList", "InsertAt")]
        public void Inserir_PosicaoValida_DeveIncrementarTamanho()
        {
            //arrange
            var list = new DoubleLinkedList<int>() { 16, 1, 15 };

            //act 
            list.InsertAt(1, 50);
            list.InsertAt(0, 99);

            //assert
            Assert.Equal(5, list.Count);
        }
        #endregion

        #region Remove
        [Fact(DisplayName = "Ao remover o item em uma posição maior do que a última, deve retornar exception"), Trait("DoubleLinkedList", "RemoveAt")]
        public void Remover_PosicaoMaiorQueAdicionarEmUltimaPosicao_DeveRetornarException()
        {
            //arrange
            var list = new DoubleLinkedList<int>() { 16, 1, 15 };

            //act & assert                      
            Assert.Throws<ArgumentOutOfRangeException>(() => list.InsertAt(4, 99));
        }

        [Fact(DisplayName = "Ao remover o item em uma posição negativa, deve retornar exception"), Trait("DoubleLinkedList", "RemoveAt")]
        public void Remover_PosicaoNegativa_DeveRetornarException()
        {
            //arrange
            var list = new DoubleLinkedList<int>() { 16, 1, 15 };

            //act & assert                      
            Assert.Throws<ArgumentOutOfRangeException>(() => list.RemoveAt(-1));
        }

        [Fact(DisplayName = "Ao remover um item em uma posição válida, o tamanho deve ser decrementado"), Trait("DoubleLinkedList", "RemoveAt")]
        public void Remover_PosicaoValida_DeveDecrementarTamanho()
        {
            //arrange
            var list = new DoubleLinkedList<int>() { 16, 1, 15 };

            //act 
            list.RemoveAt(1);

            //assert
            Assert.Equal(2, list.Count);
        }
        #endregion

        #region RemoveFirst
        [Fact(DisplayName = "Ao remover o primeiro item de uma lista com registros, quantidade deve ser decrementada"), Trait("DoubleLinkedList", "RemoveFirst")]
        public void RemoveFirst_ListaComItens_DeveDecrementarTamanho()
        {
            //arrange
            var list = new DoubleLinkedList<int>() { 16, 1, 15 };

            //act 
            list.RemoveFirst();

            //assert
            Assert.Equal(2, list.Count);
        }

        [Fact(DisplayName = "Ao remover o primeiro item de uma lista com um item, quantidade deve ser decrementada"), Trait("DoubleLinkedList", "RemoveFirst")]
        public void RemoveFirst_ListaComUmItem_DeveDecrementarTamanho()
        {
            //arrange
            var list = new DoubleLinkedList<int>() { 16 };

            //act 
            list.RemoveFirst();

            //assert
            Assert.Equal(0, list.Count);
        }

        [Fact(DisplayName = "Ao tentar remover o primeiro item de uma lista sem itens, deve retornar exception"), Trait("DoubleLinkedList", "RemoveFirst")]
        public void RemoveFirst_ListaVazia_DeveRetornarException()
        {
            //arrange
            var list = new DoubleLinkedList<int>() { };

            //act & assert
            Assert.Throws<InvalidOperationException>(() => list.RemoveFirst());
        }
        #endregion

        #region RemoveLast
        [Fact(DisplayName = "Ao remover o primeiro item de uma lista com registros, quantidade deve ser decrementada"), Trait("DoubleLinkedList", "RemoveLast")]
        public void RemoveLast_ListaComItens_DeveDecrementarTamanho()
        {
            //arrange
            var list = new DoubleLinkedList<int>() { 16, 1, 15 };

            //act 
            list.RemoveLast();

            //assert
            Assert.Equal(2, list.Count);
        }

        [Fact(DisplayName = "Ao remover o primeiro item de uma lista com um item, quantidade deve ser decrementada"), Trait("DoubleLinkedList", "RemoveLast")]
        public void RemoveLast_ListaComUmItem_DeveDecrementarTamanho()
        {
            //arrange
            var list = new DoubleLinkedList<int>() { 16 };

            //act 
            list.RemoveLast();

            //assert
            Assert.Equal(0, list.Count);
        }

        [Fact(DisplayName = "Ao tentar remover o primeiro item de uma lista sem itens, deve retornar exception"), Trait("DoubleLinkedList", "RemoveLast")]
        public void RemoveLast_ListaVazia_DeveRetornarException()
        {
            //arrange
            var list = new DoubleLinkedList<int>() { };

            //act & assert
            Assert.Throws<InvalidOperationException>(() => list.RemoveLast());
        }
        #endregion

        #region Enumerable
        [Fact(DisplayName = "Ao percorrer o IEnumerable, deve retornar o valor se existir na lista"), Trait("DoubleLinkedList", "IEnumerable")]
        public void Pesquisar_IEnumerableDeveRetornarItemCorreto()
        {
            //arrange
            var list = new DoubleLinkedList<int>() { 0, 1, 2, 3 };
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
        [Fact(DisplayName = "Ao seriaizar, deve conter itens em ordem")]
        [Trait("DoubleLinkedList", "Serialize")]
        public void Serialize_PilhaSemItens_TamanhoDeveSerZero()
        {
            //arrange
            var pilha = new DoubleLinkedList<int>();
            pilha.Add(0);
            pilha.Add(10);
            pilha.Add(20);
            //act
            var json = JsonSerializer.Serialize(pilha);

            //assert
            Assert.Equal("[0,10,20]", json);
        }

        [Fact(DisplayName = "Ao deserializar, deve conter itens em ordem")]
        [Trait("DoubleLinkedList", "Deserialize")]
        public void Deserialize_LinkedList_TamanhoDeveSerZero()
        {
            //arrange
            var pilha = new DoubleLinkedList<int>();
            pilha.Add(0);
            pilha.Add(10);
            pilha.Add(20);

            var json = JsonSerializer.Serialize(pilha);

            //act
            var deserialized = JsonSerializer.Deserialize<DoubleLinkedList<int>>(json);

            //assert
            Assert.Equal(0, deserialized.RemoveFirst());
            Assert.Equal(10, deserialized.RemoveFirst());
            Assert.Equal(20, deserialized.RemoveFirst());
        }
        #endregion

        #region Extensions
        [Fact(DisplayName = "Ao chamar a extensão, deve converter um IEnumerable para uma Queue com os mesmos itens")]
        [Trait("DoubleLinkedList", "ToDoublyLinkedList")]
        public void ToDoublyLinkedList_DeveRetornarDoubleLinkedListComItens()
        {
            //arrange
            var lista = new System.Collections.Generic.List<string> { "A", "B", "C", "D" };

            //act
            var linkedList = lista.ToDoublyLinkedList();

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
