using DataStructures.Trees;
using System;
using Xunit;

namespace DataStructures.Tests
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

        [Fact(DisplayName = "Não deve permitir conter item após remoção")]
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
    }
}
