using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DataStructures.Trees
{
    /// <summary>
    /// Representa uma árvore binária.
    /// </summary>
    /// <typeparam name="T">O tipo de elementos na árvore binária. Deve implementar <see cref="IComparable{T}"/> para permitir comparação entre elementos.</typeparam>
    [ComVisible(true)]
    [DebuggerDisplay("Count = {Count}")]
    public class BinaryTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        private BinaryTreeNode<T> _root;
        private int _count;

        /// <summary>
        /// Construtor padrão que inicializa uma nova instância da classe <see cref="BinaryTree{T}"/>.
        /// </summary>
        public BinaryTree() { }

        /// <summary>
        /// Construtor padrão que inicializa uma nova instância da classe <see cref="BinaryTree{T}"/> com um item inicial.
        /// </summary>
        /// <param name="item">O item inicial da árvore binária.</param>
        public BinaryTree(T item)
        {
            _root = new BinaryTreeNode<T>(item);
            _count++;
        }

        /// <summary>
        /// Obtém o número de itens na árvore binária.
        /// </summary>
        public int Count => _count;

        /// <summary>
        /// Obtém a altura da árvore binária.
        /// </summary>
        public int Height => GetHeight(_root);

        /// <summary>
        /// Obtém a raiz da árvore binária
        /// </summary>
        public BinaryTreeNode<T> Root => _root;

        /// <summary>
        /// Adiciona um item na árvore binária.
        /// </summary>
        /// <param name="item">O item a ser adicionado na árvore binária.</param>
        /// <exception cref="ArgumentNullException">Se o item estiver nulo.</exception>
        public void Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Não é permitido valores nulos!");

            var inserted = Insert(_root, item);

            if (_root == null)
                _root = inserted;

            _count++;
        }

        /// <summary>
        /// Verifica se existe um item na árvore binária.
        /// </summary>
        /// <param name="item">O item a ser buscado na árvore binária.</param>
        /// <exception cref="ArgumentNullException">Se o item estiver nulo.</exception>
        public bool Contains(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Não é permitido valores nulos!");

            return Search(_root, item) != null;
        }

        /// <summary>
        /// Remove um item na árvore binária.
        /// </summary>
        /// <param name="item">O item a ser removido da árvore binária.</param>
        /// <exception cref="ArgumentNullException">Se o item estiver nulo.</exception>
        /// <exception cref="InvalidOperationException">Se a árvore não conter itens.</exception>
        public void Remove(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Não é permitido valores nulos!");

            if (_root == null)
                throw new InvalidOperationException("Não existem itens na árvore!");

            var removed = Remove(_root, null, item);
            if (!removed)
                throw new InvalidOperationException("Não existe o valor informado na árvore!");

            _count--;
        }

        /// <summary>
        /// Método que remove todos os elementos da árvore binária.
        /// </summary>
        public void Clear()
        {
            _root = null;
            _count = 0;
        }

        #region Recursive Methods
        private bool Remove(BinaryTreeNode<T> node, BinaryTreeNode<T> previous, T item)
        {
            if (node == null)
                return false;

            if (item.CompareTo(node.Item) == 0)
            {
                //se não tiver filhos
                if (node.Right == null && node.Left == null)
                {
                    if (previous.Left.Equals(node))
                        previous.SetLeft(null);
                    else
                        previous.SetRight(null);
                }

                //se tiver filho a direita
                if (node.Left == null && node.Right != null)
                {
                    if (previous.Right.Equals(node))
                        previous.Right.SetRight(node.Right);
                    else
                        previous.Left.SetRight(node.Right);
                }

                //se tiver filho a esquerda
                if (node.Right == null && node.Left != null)
                {
                    if (previous.Right.Equals(node))
                        previous.Right.SetLeft(node.Left);
                    else
                        previous.Left.SetLeft(node.Left);
                }

                //se tiver dois filhos
                if (previous.Left != null && node.Right != null)
                {
                    var minimum = GetMinimumValue(node);
                    Remove(node, null, minimum);

                    var newNode = new BinaryTreeNode<T>(minimum, node.Left, node.Right);

                    if (previous.Right.Equals(node))
                        previous.SetRight(newNode);
                    else
                        previous.SetLeft(newNode);
                }

                return true;
            }

            if (item.CompareTo(node.Item) < 0)
                return Remove(node.Left, node, item);
            else
                return Remove(node.Right, node, item);
        }

        private T GetMinimumValue(BinaryTreeNode<T> node)
        {
            if (node.Left == null)
                return node.Item;

            return GetMinimumValue(node.Left);
        }

        private BinaryTreeNode<T> Search(BinaryTreeNode<T> node, T item)
        {
            if (node == null)
                return default;

            if (item.CompareTo(node.Item) == 0)
                return node;

            if (item.CompareTo(node.Item) < 0)
                return Search(node.Left, item);
            else
                return Search(node.Right, item);
        }

        private BinaryTreeNode<T> Insert(BinaryTreeNode<T> node, T item)
        {
            if (node == null)
            {
                node = new BinaryTreeNode<T>(item);
                return node;
            }

            if (item.CompareTo(node.Item) < 0)
            {
                node.SetLeft(Insert(node.Left, item));
            }
            else
            {
                node.SetRight(Insert(node.Right, item));
            }

            return node;
        }

        private int GetHeight(BinaryTreeNode<T> node)
        {
            int max = 0;
            if (node == null)
                return 0;

            int left = GetHeight(node.Left);
            int right = GetHeight(node.Right);

            max = left > right ? left : right;
            return max + 1;
        }
        #endregion

        #region Enumerable
        /// <summary>
        /// Método que retorna um enumerador que itera por uma árvore binária.
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            return new BinaryTreeEnumerator<T>(Root);
        }

        /// <summary>
        /// Método que retorna um enumerador que itera por uma árvore binária.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new BinaryTreeEnumerator<T>(Root);
        }

        private class BinaryTreeEnumerator<Type> : IEnumerator<Type> where Type : IComparable<Type>
        {
            private readonly BinaryTreeNode<Type> _root;
            private readonly IList<Type> _items;
            private int _currentIndex = -1;

            public BinaryTreeEnumerator(BinaryTreeNode<Type> root)
            {
                _root = root;
                _items = new List<Type>();
                TraverseOrdinal(_root);
            }

            public Type Current => _items[_currentIndex];
            object IEnumerator.Current => Current;

            public void Dispose()
            {
                _items.Clear();
            }

            public bool MoveNext()
            {
                _currentIndex++;
                return _currentIndex < _items.Count;
            }

            public void Reset()
            {
                _currentIndex = -1;
            }

            private void TraverseOrdinal(BinaryTreeNode<Type> node)
            {
                TraverseOrdinal(node.Left);
                _items.Add(node.Item);
                TraverseOrdinal(node.Right);
            }
        }
        #endregion
    }

    /// <summary>
    /// Representa um nó de uma árvore binária da classe <see cref="BinaryTree{T}"/>.
    /// </summary>
    /// <typeparam name="T">O tipo de elementos na árvore binária. Deve implementar <see cref="IComparable{T}"/> para permitir comparação entre elementos.</typeparam>
    public class BinaryTreeNode<T> where T : IComparable<T>
    {
        /// <summary>
        /// Inicializa uma nova instância de <see cref="BinaryTreeNode{T}"/> com o item especificado e os nós da esquerda e direita opcionais.
        /// </summary>
        /// <param name="item">O item a ser armazenado no nó.</param>
        /// <param name="left">O nó da esquerda opcional.</param>
        /// <param name="right">O nó da direita opcional.</param>
        public BinaryTreeNode(T item, BinaryTreeNode<T> left = null, BinaryTreeNode<T> right = null)
        {
            Left = left;
            Right = right;
            Item = item;
        }

        /// <summary>
        /// Obtém o nó filho da esquerda deste nó.
        /// </summary>
        public BinaryTreeNode<T> Left { get; private set; }

        /// <summary>
        /// Define o nó filho da esquerda deste nó.
        /// </summary>
        /// <param name="node">O nó a ser definido como filho da esquerda.</param>
        public void SetLeft(BinaryTreeNode<T> node)
        {
            Left = node;
        }

        /// <summary>
        /// Obtém o nó filho da direita deste nó.
        /// </summary>
        public BinaryTreeNode<T> Right { get; private set; }

        /// <summary>
        /// Define o nó filho da direita deste nó.
        /// </summary>
        /// <param name="node">O nó a ser definido como filho da direita.</param>
        public void SetRight(BinaryTreeNode<T> node)
        {
            Right = node;
        }

        /// <summary>
        /// Obtém o item armazenado no nó.
        /// </summary>
        public T Item { get; }
    }
}
