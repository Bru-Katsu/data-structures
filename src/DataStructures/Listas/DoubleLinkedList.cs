using System.Collections;
using System.Runtime.InteropServices;

namespace DataStructures.Listas
{
    [ComVisible(true)]
    public class DoubleLinkedList<T> : IEnumerable<T>
    {
        private DoubleLinkedNode<T> _head;
        private DoubleLinkedNode<T> _tail;
        private int _lenght;

        /// <summary>
        /// Obtém ou insere valores na lista
        /// </summary>
        /// <param name="position">posição</param>
        /// <returns><see cref="T"/></returns>
        public T this[int position]
        {
            get => GetValueAt(position);
            set => InsertAt(position, value);
        }

        public int Count => _lenght;

        public DoubleLinkedList() { }
        public DoubleLinkedList(T value)
        {
            Initialize(value);
        }

        /// <summary>
        /// <para>Adiciona um valor ao final da lista</para>
        /// <para>Complexidade de O(1)</para>
        /// </summary>
        /// <param name="value"></param>
        public void AddLast(T value)
        {
            if (_tail == null)
            {
                Initialize(value);
                return;
            }

            var newNode = new DoubleLinkedNode<T>(value, _tail);

            _tail.SetNext(newNode);
            _tail = _tail.Next;

            _lenght++;
        }

        /// <summary>
        /// <para>Adiciona um valor no início da lista</para>
        /// <para>Complexidade de O(1)</para>
        /// </summary>
        /// <param name="value"></param>
        public void AddFirst(T value)
        {
            if (_head == null)
            {
                Initialize(value);
                return;
            }

            var newNode = new DoubleLinkedNode<T>(value);
            newNode.SetNext(_head);
            _head.SetPrevious(newNode);
            _head = newNode;

            _lenght++;
        }

        /// <summary>
        /// <para>Adiciona um valor na posição informada</para>
        /// <para>Complexidade de O(n)</para>
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void InsertAt(int position, T value)
        {
            ThrowIfGreaterThanPositionsRange(position);

            if (_head == null)
                Initialize(value);
            else if (position == 0)
                AddFirst(value);
            else if (position == _lenght)
                AddLast(value);
            else
            {
                DoubleLinkedNode<T> previousNode = TraverseTo(position - 1);
                DoubleLinkedNode<T> currentNode = previousNode.Next;

                var newNode = new DoubleLinkedNode<T>(value);

                previousNode?.SetNext(newNode);

                newNode.SetPrevious(previousNode);
                newNode.SetNext(currentNode);

                currentNode.SetPrevious(newNode);

                _lenght++;
            }
        }

        /// <summary>
        /// <para>Remove valor na posição informada</para>
        /// <para>Complexidade de O(n)</para>
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void RemoveAt(int position)
        {
            ThrowIfNotInPositionsRange(position);

            DoubleLinkedNode<T> previousNode = TraverseTo(position - 1);
            DoubleLinkedNode<T> currentNode = previousNode.Next;

            previousNode?.SetNext(currentNode.Next);
            currentNode.Next.SetPrevious(previousNode);

            _lenght--;
        }

        /// <summary>
        /// <para>Busca valor na posição informada</para>
        /// <para>Complexidade de O(n)</para>
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public T GetValueAt(int position)
        {
            ThrowIfNotInPositionsRange(position);

            if (position == 0 || _head.Next == null)
                return _head.Value;

            var currentNode = TraverseTo(position);

            return currentNode.Value;
        }

        public void Add(T item)
        {
            AddLast(item);
        }

        #region Enumerable
        private class DoubleLinkedListEnumerator<Type> : IEnumerator<Type>
        {
            private readonly DoubleLinkedNode<Type> _head;
            private DoubleLinkedNode<Type> _current = default;

            public DoubleLinkedListEnumerator(DoubleLinkedNode<Type> head)
            {
                _head = head;
            }

            public Type Current => _current.Value;
            object IEnumerator.Current => Current;

            public void Dispose()
            {
                _current = null;
            }

            public bool MoveNext()
            {
                _current = _current is null ? _head : _current.Next;

                if (_current == null)
                    return false;

                return true;
            }

            public void Reset()
            {
                _current = default;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new DoubleLinkedListEnumerator<T>(_head);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new DoubleLinkedListEnumerator<T>(_head);
        }
        #endregion

        private void Initialize(T value)
        {
            _head = new DoubleLinkedNode<T>(value);
            _tail = _head;
            _lenght++;
        }

        private DoubleLinkedNode<T> TraverseTo(int position)
        {
            var currentNode = _head;

            for (int i = 1; i <= position; i++)
                currentNode = currentNode.Next;

            return currentNode;
        }

        private void ThrowIfGreaterThanPositionsRange(int position)
        {
            if (position < 0)
                throw new ArgumentOutOfRangeException(nameof(position), "Não é permitido posições negativas!");

            if (position > _lenght)
                throw new ArgumentOutOfRangeException(nameof(position), "Posição fora do tamanho da lista!");
        }

        private void ThrowIfNotInPositionsRange(int position)
        {
            if (position < 0)
                throw new ArgumentOutOfRangeException(nameof(position), "Não é permitido posições negativas!");

            if (position > _lenght - 1)
                throw new ArgumentOutOfRangeException(nameof(position), "Posição fora do tamanho da lista!");
        }

        private class DoubleLinkedNode<Type>
        {
            public DoubleLinkedNode(Type value)
            {
                Value = value;
            }

            public DoubleLinkedNode(Type value, DoubleLinkedNode<Type> previous) : this(value)
            {
                Previous = previous;
            }

            public DoubleLinkedNode<Type>? Previous { get; private set; }
            public void SetPrevious(DoubleLinkedNode<Type> node)
            {
                Previous = node;
            }

            public DoubleLinkedNode<Type>? Next { get; private set; }
            public void SetNext(DoubleLinkedNode<Type> node)
            {
                Next = node;
            }

            public Type Value { get; }
        }
    }
}
