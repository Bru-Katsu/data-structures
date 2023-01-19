using System.Collections;
using System.Runtime.InteropServices;

namespace DataStructures.Listas
{
    [ComVisible(true)]
    public class LinkedList<T> : IEnumerable<T>
    {
        private int _lenght;
        private LinkedNode<T> _head;
        private LinkedNode<T> _tail;

        public LinkedList() { }
        public LinkedList(T value)
        {
            Initialize(value);
        }

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

            _tail.SetNext(new LinkedNode<T>(value));
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

            var currentNode = new LinkedNode<T>(value);
            currentNode.SetNext(_head);

            _head = currentNode;
            _lenght++;
        }

        /// <summary>
        /// <para>Adiciona um valor na posição informada</para>
        /// <para>Complexidade de O(n)</para>
        /// </summary>
        /// <param name="value"></param>
        public void InsertAt(int position, T value)
        {
            ThrowIfGreaterThanPositionsRange(position);

            if (_head == null)
                Initialize(value);
            else if (position == 0)
                AddFirst(value);
            else
            {
                LinkedNode<T>? previousNode = TraverseTo(position - 1);
                LinkedNode<T> currentNode = previousNode.Next;

                var newNode = new LinkedNode<T>(value);

                newNode.SetNext(currentNode);
                previousNode?.SetNext(newNode);

                _lenght++;
            }
        }

        /// <summary>
        /// <para>Remove valor na posição informada</para>
        /// <para>Complexidade de O(n)</para>
        /// </summary>
        /// <param name="value"></param>
        public void RemoveAt(int position)
        {
            ThrowIfNotInPositionsRange(position);

            LinkedNode<T>? previousNode = TraverseTo(position - 1);
            LinkedNode<T> currentNode = previousNode.Next;

            previousNode?.SetNext(currentNode.Next);
            _lenght--;
        }

        /// <summary>
        /// <para>Busca valor na posição informada</para>
        /// <para>Complexidade de O(n)</para>
        /// </summary>
        /// <param name="value"></param>
        public T GetValueAt(int position)
        {
            ThrowIfNotInPositionsRange(position);

            if (position == 0 || _head.Next == null)
                return _head.Value;

            var currentNode = TraverseTo(position);

            return currentNode.Value;
        }
        
        /// <summary>
        /// Inverte a lista
        /// </summary>
        public void Reverse()
        {
            LinkedNode<T> current = _head, newTail = _head;
            LinkedNode<T> previous = default, next = default;

            while (current != null)
            {
                next = current.Next;

                current.SetNext(previous);

                previous = current;
                current = next;
            }

            _head = previous;
            _tail = newTail;
        }

        private void Initialize(T value)
        {
            _head = new LinkedNode<T>(value);
            _tail = _head;
            _lenght++;
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

        private LinkedNode<T> TraverseTo(int position)
        {
            var currentNode = _head;

            for (int i = 1; i <= position; i++)
                currentNode = currentNode.Next;

            return currentNode;
        }

        /// <summary>
        /// <para>Adiciona um valor ao fim da lista</para>
        /// <para>Complexidade de O(1)</para>
        /// </summary>
        /// <param name="value"></param>
        public void Add(T item)
        {
            AddLast(item);
        }

        #region Enumerable
        public IEnumerator<T> GetEnumerator()
        {
            return new LinkedListEnumerator<T>(_head);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new LinkedListEnumerator<T>(_head);
        }

        private class LinkedListEnumerator<Type> : IEnumerator<Type>
        {
            private readonly LinkedNode<Type> _head;
            private LinkedNode<Type> _current = default;
            public LinkedListEnumerator(LinkedNode<Type> node)
            {
                _head = node;
                _current = default;
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
        #endregion

        #region Types
        private class LinkedNode<Type>
        {
            public LinkedNode(Type value)
            {
                Value = value;
            }

            public Type Value { get; }

            public LinkedNode<Type> Next { get; private set; }
            internal void SetNext(LinkedNode<Type> node)
            {
                Next = node;
            }
        }
        #endregion
    }
}
