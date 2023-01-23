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
        public LinkedList(T item)
        {
            Initialize(item);
        }

        /// <summary>
        /// Obtém ou insere valores na lista
        /// </summary>
        /// <param name="index">posição</param>
        /// <returns><see cref="T"/></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public T this[int index]
        {
            get => GetItemAt(index);
            set => InsertAt(index, value);
        }

        public int Count => _lenght;

        public LinkedNode<T> Head => _head;
        public LinkedNode<T> Tail => _tail;

        /// <summary>
        /// <para>Adiciona um valor no início da lista</para>
        /// <para>Complexidade de O(1)</para>
        /// </summary>
        /// <param name="item"></param>
        public void AddFirst(T item)
        {
            if (_head == null)
            {
                Initialize(item);
                return;
            }

            var currentNode = new LinkedNode<T>(item);
            currentNode.SetNext(_head);

            _head = currentNode;
            _lenght++;
        }

        /// <summary>
        /// <para>Adiciona um valor ao final da lista</para>
        /// <para>Complexidade de O(1)</para>
        /// </summary>
        /// <param name="item"></param>
        public void AddLast(T item)
        {
            if (_tail == null)
            {
                Initialize(item);
                return;
            }

            _tail.SetNext(new LinkedNode<T>(item));
            _tail = _tail.Next;
            _lenght++;
        }

        /// <summary>
        /// <para>Adiciona um valor na posição informada</para>
        /// <para>Complexidade de O(n)</para>
        /// </summary>
        /// <param name="item"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void InsertAt(int index, T item)
        {
            ThrowIfGreaterThanIndexRange(index);

            if (_head == null)
                Initialize(item);
            else if (index == 0)
                AddFirst(item);
            else
            {
                LinkedNode<T>? previousNode = TraverseTo(index - 1);
                LinkedNode<T> currentNode = previousNode.Next;

                var newNode = new LinkedNode<T>(item);

                newNode.SetNext(currentNode);
                previousNode?.SetNext(newNode);

                _lenght++;
            }
        }

        /// <summary>
        /// <para>Remove o primeiro valor da lista</para>
        /// <para>Complexidade de O(1)</para>
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public void RemoveFirst()
        {
            if (_head == null)
                throw new InvalidOperationException("Não há itens na lista!");

            if (_head.Next == null)
            {
                _head = null;
            }
            else
            {
                var next = _head.Next;
                _head = null;
                _head = next;
            }

            _lenght--;
        }

        /// <summary>
        /// <para>Remove o último valor da lista</para>
        /// <para>Complexidade de O(n)</para>
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public void RemoveLast()
        {
            if (_tail == null)
                throw new InvalidOperationException("Não há itens na lista!");

            if (_head.Next == null && _lenght == 1)
            {
                _tail = null;
            }
            else
            {
                var beforeLast = TraverseTo(_lenght - 2);

                _tail = null;
                _tail = beforeLast;
            }

            _lenght--;
        }

        /// <summary>
        /// <para>Remove valor na posição informada</para>
        /// <para>Complexidade de O(n)</para>
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void RemoveAt(int index)
        {
            ThrowIfNotInIndexRange(index);

            if (index == 0)
                RemoveFirst();
            else if (index == _lenght - 1)
                RemoveLast();
            else
            {
                LinkedNode<T>? previousNode = TraverseTo(index - 1);
                LinkedNode<T> currentNode = previousNode.Next;

                previousNode?.SetNext(currentNode.Next);
                _lenght--;
            }
        }

        /// <summary>
        /// <para>Busca valor na posição informada</para>
        /// <para>Complexidade de O(n)</para>
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public T GetItemAt(int index)
        {
            ThrowIfNotInIndexRange(index);

            if (index == 0 || _head.Next == null)
                return _head.Value;

            var currentNode = TraverseTo(index);

            return currentNode.Value;
        }

        /// <summary>
        /// <para>Retorna o último registro da lista</para>
        /// <para>Complexidade de O(1)</para>
        /// </summary>
        /// <returns></returns>
        public T GetLast()
        {
            if (_tail == null)
                return default;

            return _tail.Value;
        }

        /// <summary>
        /// <para>Retorna o primeiro registro da lista</para>
        /// <para>Complexidade de O(1)</para>
        /// </summary>
        /// <returns></returns>
        public T GetFirst()
        {
            if (_head == null)
                return default;

            return _head.Value;
        }

        /// <summary>
        /// <para>Inverte a lista</para>
        /// <para>Complexidade de O(n)</para>
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

        /// <summary>
        /// <para>Limpa a lista</para>
        /// <para>Complexidade de O(1)</para>
        /// </summary>
        public void Clear()
        {
            _head = null;
            _tail = null;
            _lenght = 0;
        }

        private void Initialize(T item)
        {
            _head = new LinkedNode<T>(item);
            _tail = _head;
            _lenght++;
        }

        private void ThrowIfGreaterThanIndexRange(int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index), "Não é permitido posições negativas!");

            if (index > _lenght)
                throw new ArgumentOutOfRangeException(nameof(index), "Posição fora do tamanho da lista!");
        }

        private void ThrowIfNotInIndexRange(int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index), "Não é permitido posições negativas!");

            if (index > _lenght - 1)
                throw new ArgumentOutOfRangeException(nameof(index), "Posição fora do tamanho da lista!");
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
    }

    #region Types
    public class LinkedNode<Type>
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
