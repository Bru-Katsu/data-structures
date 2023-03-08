using DataStructures.Extensions;
using DataStructures.Trees;
using System;
using System.Linq;
using Xunit;

namespace DataStructures.Tests.Trees
{
    public class BinaryTreeTests
    {
        #region New Instance
        [Fact(DisplayName = "Ao istanciar, não deve conter nenhum item")]
        [Trait("BinaryTree", "Instânciar")]
        public void BinaryTree_DeveCriarArvore()
        {
            // Arrange
            var tree = new BinaryTree<int>();

            // Act & Assert
            Assert.NotNull(tree);
            Assert.Equal(0, tree.Count);
        }

        [Fact(DisplayName = "Ao istanciar, deve conter o item inicial")]
        [Trait("BinaryTree", "Instânciar")]
        public void BinaryTree_DeveCriarArvoreComItemInicial()
        {
            // Arrange
            var tree = new BinaryTree<int>(10);

            // Act & Assert
            Assert.NotNull(tree);
            Assert.Equal(10, tree.Root.Item);
            Assert.Equal(1, tree.Count);
        }
        #endregion

        #region Add
        [Fact(DisplayName = "Deve incrementar quantidade ao adicionar itens")]
        [Trait("BinaryTree", "Add")]
        public void Add_BinaryTree_DeveAdicionarItem()
        {
            // Arrange
            var tree = new BinaryTree<int>(10);

            // Act
            tree.Add(5);
            tree.Add(15);
            tree.Add(7);

            // Assert
            Assert.Equal(4, tree.Count);
        }

        [Fact(DisplayName = "Não deve permitir adicionar valores nulos")]
        [Trait("BinaryTree", "Add")]
        public void Add_BinaryTree_NaoDevePermitirValoresNulos()
        {
            // Arrange
            var tree = new BinaryTree<string>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => tree.Add(null));
        }
        #endregion

        #region Contains
        [Fact(DisplayName = "Ao adicionar um item, deve contê-lo na árvore")]
        [Trait("BinaryTree", "Contains")]
        public void Contains_BinaryTree_DeveConterItem()
        {
            // Arrange
            var tree = new BinaryTree<int>(10) { 5, 15, 7 };

            // Act & Assert
            Assert.True(tree.Contains(10));
            Assert.True(tree.Contains(5));
            Assert.True(tree.Contains(15));
            Assert.True(tree.Contains(7));
        }

        [Fact(DisplayName = "Não deve conter itens não adicionados anteriormente")]
        [Trait("BinaryTree", "Contains")]
        public void Contains_BinaryTree_DeveNaoConterItem()
        {
            // Arrange
            var tree = new BinaryTree<int>(10) { 5, 15, 7 };

            // Act & Assert
            Assert.False(tree.Contains(20));
        }

        [Fact(DisplayName = "Não deve permitir buscar por valores nulos")]
        [Trait("BinaryTree", "Contains")]
        public void Contains_BinaryTree_NaoDevePermitirValoresNulos()
        {
            // Arrange
            var tree = new BinaryTree<string>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => tree.Contains(null));
        }
        #endregion

        #region Remove
        [Fact(DisplayName = "Não deve permitir remover valores nulos")]
        [Trait("BinaryTree", "Remove")]
        public void Remove_BinaryTree_NaoDevePermitirValoresNulos()
        {
            // Arrange
            var tree = new BinaryTree<string>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => tree.Remove(null));
        }

        [Fact(DisplayName = "Não deve permitir remover valores quando não conter itens")]
        [Trait("BinaryTree", "Remove")]
        public void Remove_BinaryTree_NaoDevePermitirValoresQuandoNaoHouverItens()
        {
            // Arrange
            var tree = new BinaryTree<string>();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => tree.Remove("item"));
        }

        [Fact(DisplayName = "Ao tentar remover valor inexistente, deve retornar false")]
        [Trait("BinaryTree", "Remove")]
        public void Remove_BinaryTree_AoRemoverValorInexistenteDeveRetornarFalse()
        {
            // Arrange
            var tree = new BinaryTree<string>() { "Japão", "Brasil" };

            // Act & Assert
            Assert.False(tree.Remove("Argentina"));
        }

        [Fact(DisplayName = "Não deve conter item após remoção")]
        [Trait("BinaryTree", "Remove")]
        public void Remove_BinaryTree_NaoDeveConterItemAposRemocao()
        {
            // Arrange
            var tree = new BinaryTree<int>() { 5, 20, 90, 23 };

            // Act
            tree.Remove(23);

            // Assert
            Assert.False(tree.Contains(23));
            Assert.Equal(3, tree.Count);
        }
        #endregion

        #region Clear
        [Fact(DisplayName = "Não deve conter item após a chamada do método Clear")]
        [Trait("BinaryTree", "Clear")]
        public void Clear_BinaryTree_NaoDeveConterItemAposClear()
        {
            // Arrange
            var tree = new BinaryTree<int>() { 5, 20, 90, 23 };

            // Act
            tree.Clear();

            // Assert            
            Assert.Equal(0, tree.Count);
            Assert.Equal(0, tree.Height);
        }
        #endregion

        #region Height
        [Fact(DisplayName = "Ao conter itens, deve retornar a altura da árvore binária de acordo com o números de iterações máximas necessárias para percorre-lá em uma busca")]
        [Trait("BinaryTree", "Height")]
        public void Height_BinaryTree_AlturaDeveEstarCorreta()
        {
            // Arrange
            var tree = new BinaryTree<int>();
            tree.Add(6);
            tree.Add(10);
            tree.Add(15);
            tree.Add(18);
            tree.Add(5);
            tree.Add(3);

            // Act & Assert
            Assert.Equal(4, tree.Height);
        }

        [Fact(DisplayName = "Ao não conter itens, deve retornar zero")]
        [Trait("BinaryTree", "Height")]
        public void Height_BinaryTree_NaoConterItensDeveTerAlturaZerada()
        {
            // Arrange
            var tree = new BinaryTree<int>();

            // Act & Assert
            Assert.Equal(0, tree.Height);
        }
        #endregion

        #region CopyTo
        [Fact(DisplayName = "Ao copiar os itens para um array, deve conter todos os itens da árvore binária")]
        [Trait("BinaryTree", "CopyTo")]
        public void CopyTo_BinaryTree_DeveConterTodosItensNoArray()
        {
            // Arrange
            var tree = new BinaryTree<int>()
            {
                5, 10, 50, 19, 2, 3, 7, 42
            };

            // Act
            var arr = new int[tree.Count];
            tree.CopyTo(arr, 0);

            // Assert
            Assert.Contains(tree, (item) =>
            {
                return arr.Contains(item);
            });
        }

        [Fact(DisplayName = "Ao tentar copiar os itens para um array nulo, deve retornar exception")]
        [Trait("BinaryTree", "CopyTo")]
        public void CopyTo_BinaryTree_ArrayNuloDeveRetornarException()
        {
            // Arrange
            var tree = new BinaryTree<int>()
            {
                5, 10, 50, 19, 2, 3, 7, 42
            };

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => tree.CopyTo(null, 0));
        }

        [Fact(DisplayName = "Ao tentar copiar os itens para fora do intervalo do array, deve retornar exception")]
        [Trait("BinaryTree", "CopyTo")]
        public void CopyTo_BinaryTree_ArrayIndexForaDoIntervaloDeveRetornarException()
        {
            // Arrange
            var tree = new BinaryTree<int>()
            {
                5, 10, 50, 19, 2, 3, 7, 42
            };

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => tree.CopyTo(new int[tree.Count], tree.Count + 1));
            Assert.Throws<ArgumentOutOfRangeException>(() => tree.CopyTo(new int[tree.Count], -1));
        }

        [Fact(DisplayName = "Caso array não comporte todo o conteúdo da BinaryTree, deve retornar exception")]
        [Trait("BinaryTree", "CopyTo")]
        public void CopyTo_BinaryTree_ArrayMenorQueBinaryTreeDeveRetornarException()
        {
            // Arrange
            var tree = new BinaryTree<int>()
            {
                5, 10, 50, 19, 2, 3, 7, 42
            };

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => tree.CopyTo(new int[tree.Count], tree.Count - 1));

        }
        #endregion

        #region Extensions
        [Fact(DisplayName = "Ao chamar a extensão, deve converter um IEnumerable para uma Queue com os mesmos itens")]
        [Trait("BinaryTree", "ToBinaryTree")]
        public void ToBinaryTree_DeveRetornarBinaryTreeComItens()
        {
            //arrange
            var lista = new System.Collections.Generic.List<string> { "A", "B", "C", "D" };

            //act
            var tree = lista.ToBinaryTree();

            //assert
            Assert.Equal(lista.Count, tree.Count);

            foreach (var item in lista)
            {
                Assert.True(tree.Contains(item));
            }
        }
        #endregion
    }
}
