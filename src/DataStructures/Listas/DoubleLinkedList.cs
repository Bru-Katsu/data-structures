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
        /// <param name="index">posição</param>
        /// <returns><see cref="T"/></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public T this[int index]
        {
            get => GetItemAt(index);
            set => InsertAt(index, value);
        }

        public int Count => _lenght;

        public DoubleLinkedNode<T> Head => _head;
        public DoubleLinkedNode<T> Tail => _tail;

        public DoubleLinkedList() { }
        public DoubleLinkedList(T item)
        {
            Initialize(item);
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

            var newNode = new DoubleLinkedNode<T>(item, _tail);

            _tail.SetNext(newNode);
            _tail = _tail.Next;

            _lenght++;
        }

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

            var newNode = new DoubleLinkedNode<T>(item);
            newNode.SetNext(_head);
            _head.SetPrevious(newNode);
            _head = newNode;

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
            else if (index == _lenght)
                AddLast(item);
            else
            {
                DoubleLinkedNode<T> previousNode = TraverseTo(index - 1);
                DoubleLinkedNode<T> currentNode = previousNode.Next;

                var newNode = new DoubleLinkedNode<T>(item);

                previousNode?.SetNext(newNode);

                newNode.SetPrevious(previousNode);
                newNode.SetNext(currentNode);

                currentNode.SetPrevious(newNode);

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
                next.SetPrevious(null);

                _head = null;
                _head = next;
            }

            _lenght--;
        }

        /// <summary>
        /// <para>Remove o último valor da lista</para>
        /// <para>Complexidade de O(1)</para>
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public void RemoveLast()
        {
            if (_tail == null)
                throw new InvalidOperationException("Não há itens na lista!");

            if (_tail.Previous == null)
            {
                _tail = null;
            }
            else
            {
                var previous = _tail.Previous;
                previous.SetNext(null);

                _tail = null;
                _tail = previous;
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
                DoubleLinkedNode<T> previousNode = TraverseTo(index - 1);
                DoubleLinkedNode<T> currentNode = previousNode.Next;

                previousNode?.SetNext(currentNode.Next);
                currentNode.Next.SetPrevious(previousNode);

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
        /// <para>Adiciona um valor ao final da lista</para>
        /// <para>Complexidade de O(1)</para>
        /// </summary>
        /// <param name="value"></param>
        public void Add(T item)
        {
            AddLast(item);
        }

        /// <summary>
        /// <para>Limpa a lista</para>
        /// <para>Complexidade de O(n)</para>
        /// </summary>
        public void Clear()
        {
            _head = null;
            _tail = null;
            _lenght = 0;
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

        private DoubleLinkedNode<T> TraverseTo(int index)
        {
            var currentNode = _head;

            for (int i = 1; i <= index; i++)
                currentNode = currentNode.Next;

            return currentNode;
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

    }
    public class DoubleLinkedNode<Type>
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
