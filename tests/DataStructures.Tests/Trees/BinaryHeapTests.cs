using DataStructures.Trees;
using System;
using Xunit;

namespace DataStructures.Tests.Trees
{
    public class BinaryHeapTests
    {
        #region Add
        [Fact(DisplayName = "Adicionar item, deve incrementar a contagem do heap")]
        [Trait("BinaryHeap", "Add")]
        public void Add_UmItem_HeapDeveConterItem()
        {
            // Arrange
            var heap = new BinaryHeap<int>();

            // Act
            heap.Add(5);

            // Assert
            Assert.Equal(1, heap.Count);
            Assert.Equal(5, heap.Top);
        }

        [Fact(DisplayName = "Adicionar vários itens em max-heap, deve adicioná-los ao heap na ordem correta")]
        [Trait("BinaryHeap", "Add")]
        public void Add_MultiplosItensMaxHeap_HeapDeveConterItens()
        {
            // Arrange
            var heap = new BinaryHeap<int>();

            // Act
            heap.Add(5);
            heap.Add(10);
            heap.Add(3);
            heap.Add(8);

            // Assert
            Assert.Equal(4, heap.Count);
            Assert.Equal(10, heap.Top);
        }

        [Fact(DisplayName = "Adicionar vários itens em min-heap, deve adicioná-los ao heap na ordem correta")]
        [Trait("BinaryHeap", "Add")]
        public void Add_MultiplosItensMinHeap_HeapDeveConterItens()
        {
            // Arrange
            var heap = new BinaryHeap<int>(HeapMode.MinHeap);

            // Act
            heap.Add(5);
            heap.Add(10);
            heap.Add(3);
            heap.Add(8);

            // Assert
            Assert.Equal(4, heap.Count);
            Assert.Equal(3, heap.Top);
        }
        #endregion

        #region Remove
        [Fact(DisplayName = "Ao remover item existente do heap em max-heap, deve removê-lo do heap e reordenar")]
        [Trait("BinaryHeap", "Remove")]
        public void Remove_ItemExisteNoHeapMaxHeap_ItemDeveSerRemovidoDoHeap()
        {
            // Arrange
            var heap = new BinaryHeap<int>();
            heap.Add(5);
            heap.Add(10);
            heap.Add(3);

            // Act
            var removed = heap.Remove(10);

            // Assert
            Assert.True(removed);
            Assert.Equal(2, heap.Count);
            Assert.Equal(5, heap.Top);
        }

        [Fact(DisplayName = "Ao remover item existente do heap em min-heap, deve removê-lo do heap e reordenar")]
        [Trait("BinaryHeap", "Remove")]
        public void Remove_ItemExisteNoHeapMinHeap_ItemDeveSerRemovidoDoHeap()
        {
            // Arrange
            var heap = new BinaryHeap<int>(HeapMode.MinHeap);
            heap.Add(5);
            heap.Add(10);
            heap.Add(3);

            // Act
            var removed = heap.Remove(3);

            // Assert
            Assert.True(removed);
            Assert.Equal(2, heap.Count);
            Assert.Equal(5, heap.Top);
        }

        [Fact(DisplayName = "Ao tentar remover item inexistente do heap, não deve removê-lo do heap")]
        [Trait("BinaryHeap", "Remove")]
        public void Remove_ItemNaoExisteNoHeap_DeveRetornarFalse()
        {
            // Arrange
            var heap = new BinaryHeap<int>();
            heap.Add(5);
            heap.Add(10);
            heap.Add(3);

            // Act
            var removed = heap.Remove(8);

            // Assert
            Assert.False(removed);
            Assert.Equal(3, heap.Count);
            Assert.Equal(10, heap.Top);
        }
        #endregion

        #region Clear
        [Fact(DisplayName = "Ao limpar heap com elementos, deve remover todos os elementos do heap")]
        [Trait("BinaryHeap", "Clear")]
        public void Clear_HeapNaoVazio_DeveEsvaziarHeap()
        {
            // Arrange
            var heap = new BinaryHeap<int>();
            heap.Add(5);
            heap.Add(10);
            heap.Add(3);

            // Act
            heap.Clear();

            // Assert
            Assert.Equal(0, heap.Count);
            Assert.Throws<ArgumentOutOfRangeException>(() => heap.Top);
        }
        #endregion

        #region Contains
        [Fact(DisplayName = "Ao verificar se heap contém item existente, deve retornar true")]
        [Trait("BinaryHeap", "Contains")]
        public void Contains_ItemExisteNoHeap_DeveRetornarTrue()
        {
            // Arrange
            var heap = new BinaryHeap<int>();
            heap.Add(5);
            heap.Add(10);
            heap.Add(3);

            // Act
            var contains = heap.Contains(10);

            // Assert
            Assert.True(contains);
        }

        [Fact(DisplayName = "Ao verificar se heap contém item inexistente, deve retornar false")]
        [Trait("BinaryHeap", "Contains")]
        public void Contains_ItemNaoExisteNoHeap_DeveRetornarFalse()
        {
            // Arrange
            var heap = new BinaryHeap<int>();
            heap.Add(5);
            heap.Add(10);
            heap.Add(3);

            // Act
            var contains = heap.Contains(8);

            // Assert
            Assert.False(contains);
        }
        #endregion

        #region CopyTo
        [Fact(DisplayName = "Ao copiar elementos do heap para um array, deve copiar na ordem correta")]
        [Trait("BinaryHeap", "CopyTo")]
        public void CopyTo_HeapNaoVazio_ArrayDeveCopiarDoHeap()
        {
            // Arrange
            var heap = new BinaryHeap<int>();
            heap.Add(5);
            heap.Add(10);
            heap.Add(3);
            var array = new int[3];

            // Act
            heap.CopyTo(array, 0);

            // Assert
            Assert.Equal(new int[] { 10, 5, 3 }, array);
        }
        #endregion

        #region Enumerator
        [Fact(DisplayName = "Ao percorrer o enumerador do heap, deve enumerar corretamente os elementos")]
        [Trait("BinaryHeap", "Enumerator")]
        public void GetEnumerator_HeapNaoVazio_DevePercorrerElementos()
        {
            // Arrange
            var heap = new BinaryHeap<int>();
            heap.Add(5);
            heap.Add(10);
            heap.Add(3);
            var expected = new int[] { 10, 5, 3 };

            // Act
            var index = 0;
            foreach (var item in heap)
            {
                // Assert
                Assert.Equal(expected[index], item);
                index++;
            }
        }
        #endregion
    }
}
