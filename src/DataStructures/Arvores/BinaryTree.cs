using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DataStructures.Arvores
{
    [ComVisible(true)]
    [DebuggerDisplay("Count = {Count}")]
    public class BinaryTree<T> where T : IComparable<T>
    {
        private readonly BinaryTreeNode<T> _root;
        private readonly int _count;
        public BinaryTree(T item)
        {
            _root = new BinaryTreeNode<T>(item);
        }

        public int Count => _count;

        public void Insert(T item)
        {
            Insert(_root, item);
        }

        public bool Contains(T item)
        {
            var node = Search(_root, item);

            return node != null;
        }

        public void Remove(T item)
        {

        }

        private BinaryTreeNode<T> Search(BinaryTreeNode<T> node, T item)
        {
            if(item.CompareTo(node.Item) == 0 || node == null)
                return node;

            if(item.CompareTo(node.Item) < 0)
                Search(node.Left, item);
            else
                Search(node.Right, item);

            return node;
        }

        private BinaryTreeNode<T> Insert(BinaryTreeNode<T> node, T item)
        {
            if(node == null)
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

        private class BinaryTreeNode<TNode>
        {
            public BinaryTreeNode(T item)
            {
                Left = default;
                Right = default;
                Item = item;
            }

            public BinaryTreeNode<TNode> Left { get; private set; }
            public void SetLeft(BinaryTreeNode<TNode> node)
            {
                Left = node;
            }

            public BinaryTreeNode<TNode> Right { get; private set; }
            public void SetRight(BinaryTreeNode<TNode> node)
            {
                Right = node;
            }

            public T Item { get; }
        }
    }
}
