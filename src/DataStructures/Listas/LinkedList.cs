using System.Collections;
using System.Runtime.InteropServices;

namespace DataStructures.Listas
{
    [ComVisible(true)]
    public class LinkedList<T> : IEnumerable<T>, ICollection<T>
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
        public bool IsReadOnly => false;

        /// <summary>
        /// <para>Adiciona um valor ao final da lista</para>
        /// <para>Complexidade de O(1)</para>
        /// </summary>
        /// <param name="value"></param>
        public void Append(T value)
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
        public void Prepend(T value)
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
            ThrowIfInvalid(position);

            if (_head == null)
                Initialize(value);
            else if (position == 0)
                Prepend(value);
            else if (position == _lenght)
                Append(value);
            else
            {
                LinkedNode<T> currentNode = _head;
                LinkedNode<T>? beforeNode = default;

                for (int i = 1; i <= position; i++)
                {
                    beforeNode = currentNode;
                    currentNode = currentNode.Next;
                }

                var newNode = new LinkedNode<T>(value);

                newNode.SetNext(currentNode);
                beforeNode?.SetNext(newNode);
            }
        }

        /// <summary>
        /// <para>Remove valor na posição informada</para>
        /// <para>Complexidade de O(n)</para>
        /// </summary>
        /// <param name="value"></param>
        public void RemoveAt(int position)
        {
            ThrowIfInvalid(position);

            LinkedNode<T> currentNode = _head;
            LinkedNode<T>? beforeNode = default;

            for (int i = 1; i <= position; i++)
            {
                beforeNode = currentNode;
                currentNode = currentNode.Next;
            }

            beforeNode?.SetNext(currentNode.Next);
        }

        /// <summary>
        /// <para>Busca valor na posição informada</para>
        /// <para>Complexidade de O(n)</para>
        /// </summary>
        /// <param name="value"></param>
        public T GetValueAt(int position)
        {
            ThrowIfInvalid(position);

            if (position == 0 || _head.Next == null)
                return _head.Value;

            var currentNode = _head;

            for (int i = 1; i <= position; i++)
                currentNode = currentNode.Next;

            return currentNode.Value;
        }

        #region Collection        
        /// <summary>
        /// <para>Adiciona um valor ao fim da lista</para>
        /// <para>Complexidade de O(1)</para>
        /// </summary>
        /// <param name="value"></param>
        public void Add(T item)
        {
            Append(item);
        }

        /// <summary>
        /// Limpa lista
        /// </summary>
        public void Clear()
        {
            _head = null;
            _tail = null;
        }

        public bool Contains(T item)
        {
            var node = _head;
            while(node != null)
            {
                if (node.Value.Equals(item))
                {
                    return true;
                }

                node = node.Next;
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ThrowIfInvalid(arrayIndex);

            var node = _head;
            for (int i = 0; i < _lenght; i++)
            {
                if (i < arrayIndex)
                    continue;

                array[i] = node.Value;
                node = node.Next;
            }
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Enumerable
        public IEnumerator<T> GetEnumerator()
        {
            return new LinkedListEnumerator<T>(_head);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new LinkedListEnumerator<T>(_head);
        }

        private class LinkedListEnumerator<T> : IEnumerator<T>
        {
            private readonly LinkedNode<T> _head;
            private LinkedNode<T> _current = default;
            public LinkedListEnumerator(LinkedNode<T> node)
            {
                _head = node;
                _current = default;
            }

            public T Current => _current.Value;

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

        private void Initialize(T value)
        {
            _head = new LinkedNode<T>(value);
            _tail = _head;
            _lenght++;
        }

        private void ThrowIfInvalid(int position)
        {
            if (position < 0)
                throw new ArgumentOutOfRangeException(nameof(position), "Não é permitido posições negativas!");

            if (position > _lenght)
                throw new ArgumentOutOfRangeException(nameof(position), "Posição fora do tamanho da lista!");
        }

        #region Types
        private class LinkedNode<T>
        {
            public LinkedNode(T value)
            {
                Value = value;
            }

            public T Value { get; }

            public LinkedNode<T> Next { get; private set; }
            internal void SetNext(LinkedNode<T> node)
            {
                Next = node;
            }
        }
        #endregion
    }
}
