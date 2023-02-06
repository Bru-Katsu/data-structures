using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace DataStructures.Collections
{
    /// <summary>
    /// Representa uma lista encadeada.
    /// </summary>
    /// <typeparam name="T">O tipo de elementos na lista encadeada.</typeparam>
    [Serializable]
    [ComVisible(true)]
    [DebuggerDisplay("Count = {Count}")]
    public class LinkedList<T> : ICollection<T>, ISerializable
    {
        private int _lenght;
        private LinkedNode<T> _head;
        private LinkedNode<T> _tail;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="LinkedList{T}"/>.
        /// </summary>
        public LinkedList() { }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="LinkedList{T}"/> com um item inicial.
        /// </summary>
        /// <param name="item">O item inicial da lista encadeada.</param>
        public LinkedList(T item)
        {
            Initialize(item);
        }

        /// <summary>
        /// Obtém ou define o item na posição especificada.
        /// </summary>
        /// <param name="index">A posição do item na lista encadeada.</param>
        /// <exception cref="ArgumentOutOfRangeException">Se a posição estiver fora do tamanho da lista.</exception>
        public T this[int index]
        {
            get => GetItemAt(index);
            set => InsertAt(index, value);
        }

        /// <summary>
        /// Obtém o número de itens na lista encadeada.
        /// </summary>
        public int Count => _lenght;

        /// <summary>
        /// Obtém o nó inicial da lista encadeada.
        /// </summary>
        public LinkedNode<T> Head => _head;

        /// <summary>
        /// Obtém o nó final da lista encadeada.
        /// </summary>
        public LinkedNode<T> Tail => _tail;

        /// <summary>
        /// Obtém um valor que indica se a lista encadeada é somente leitura.
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Adiciona um item no início da lista encadeada.
        /// </summary>
        /// <param name="item">O item a ser adicionado no início da lista encadeada.</param>
        /// <exception cref="ArgumentNullException">Se o item estiver nulo.</exception>
        public void AddFirst(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Não é permitido valores nulos!");

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
        /// Adiciona um item no fim da lista encadeada.
        /// </summary>
        /// <param name="item">O item a ser adicionado no final da lista encadeada.</param>
        /// <exception cref="ArgumentNullException">Se o item estiver nulo.</exception>
        public void AddLast(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Não é permitido valores nulos!");

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
        /// Insere um item em uma posição específica na lista encadeada.
        /// </summary>
        /// <param name="index">A posição onde o item será inserido.</param>
        /// <param name="item">O item a ser inserido.</param>
        /// <exception cref="ArgumentOutOfRangeException">Se a posição estiver fora do tamanho da lista.</exception>
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
        /// Remove o primeiro item da lista encadeada.
        /// </summary>
        /// <returns>O primeiro item da lista encadeada.</returns>
        /// <exception cref="InvalidOperationException">Se a lista estiver vazia.</exception>
        public T RemoveFirst()
        {
            if (_head == null)
                throw new InvalidOperationException("Não há itens na lista!");

            T value = _head.Value;
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
            return value;
        }

        /// <summary>
        /// Remove o último item da lista encadeada.
        /// </summary>
        /// <returns>O último item da lista encadeada.</returns>
        /// <exception cref="InvalidOperationException">Se a lista estiver vazia.</exception>
        public T RemoveLast()
        {
            if (_tail == null)
                throw new InvalidOperationException("Não há itens na lista!");

            T value = _tail.Value;

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
            return value;
        }

        /// <summary>
        /// Remove o item em uma posição específica da lista encadeada.
        /// </summary>
        /// <param name="index">A posição do item a ser removido.</param>
        /// <returns>O item na posição especificada.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Se a posição estiver fora do tamanho da lista.</exception>
        public T RemoveAt(int index)
        {
            ThrowIfNotInIndexRange(index);
            T value;

            if (index == 0)
                value = RemoveFirst();
            else if (index == _lenght - 1)
                value = RemoveLast();
            else
            {

                LinkedNode<T>? previousNode = TraverseTo(index - 1);
                LinkedNode<T> currentNode = previousNode.Next;
                value = currentNode.Value;

                previousNode?.SetNext(currentNode.Next);
                _lenght--;
            }

            return value;
        }

        /// <summary>
        /// Obtém o item em uma posição específica na lista encadeada.
        /// </summary>
        /// <param name="index">A posição do item a ser obtido.</param>
        /// <returns>O item na posição especificada.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Se a posição estiver fora do tamanho da lista.</exception>
        public T GetItemAt(int index)
        {
            ThrowIfNotInIndexRange(index);

            if (index == 0 || _head.Next == null)
                return _head.Value;

            var currentNode = TraverseTo(index);

            return currentNode.Value;
        }

        /// <summary>
        /// Obtém o primeiro item da lista encadeada.
        /// </summary>
        /// <returns>O primeiro item da lista encadeada.</returns>
        public T GetFirst()
        {
            if (_head == null)
                return default;

            return _head.Value;
        }

        /// <summary>
        /// Obtém o último item da lista encadeada.
        /// </summary>'
        /// <returns>O último item da lista encadeada.</returns>
        public T GetLast()
        {
            if (_tail == null)
                return default;

            return _tail.Value;
        }

        /// <summary>
        /// Reverte a ordem dos itens na lista encadeada.
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

        #region Collection
        /// <summary>
        /// Insere um item na posição especificada na lista encadeada.
        /// </summary>
        /// <param name="item">O item a ser adicionado no final da lista encadeada.</param>
        public void Add(T item)
        {
            AddLast(item);
        }

        bool ICollection<T>.Contains(T item)
        {
            throw new NotSupportedException();
        }

        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            throw new NotSupportedException();
        }

        bool ICollection<T>.Remove(T item)
        {
            throw new NotSupportedException();
        }
        /// <summary>
        /// Remove todos os itens da lista encadeada.
        /// </summary>
        public void Clear()
        {
            _head = null;
            _tail = null;
            _lenght = 0;
        }
        #endregion

        #region Serializable
        protected LinkedList(SerializationInfo info, StreamingContext context)
        {
            int count = (int)info.GetValue("Count", typeof(int));
            for (int i = 0; i < count; i++)
            {
                T item = (T)info.GetValue("Item" + i, typeof(T));
                AddLast(item);
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Count", Count);

            int index = 0;
            foreach (T item in this)
            {
                info.AddValue("Item" + index, item);
                index++;
            }
        }
        #endregion
    }

    #region Types
    /// <summary>
    /// Classe LinkedNode que representa um nó de uma lista encadeada da classe <see cref="LinkedList{T}"/>.
    /// </summary>
    /// <typeparam name="T">O tipo de valor armazenado pelo nó</typeparam>
    public class LinkedNode<T>
    {
        /// <summary>
        /// Construtor que cria um nó com o valor especificado.
        /// </summary>
        /// <param name="value">valor a ser armazenado pelo nó.</param>
        public LinkedNode(T value)
        {
            Value = value;
        }

        /// <summary>
        /// Obtém o próximo nó na lista.
        /// </summary>
        public LinkedNode<T> Next { get; private set; }

        /// <summary>
        /// Define o próximo na lista.
        /// </summary>
        /// <param name="node">O nó a ser definido como próximo.</param>
        public void SetNext(LinkedNode<T> node)
        {
            Next = node;
        }

        /// <summary>
        /// Obtém o valor armazenado
        /// </summary>
        public T Value { get; }
    }
    #endregion
}
