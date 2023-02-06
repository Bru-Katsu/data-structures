using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace DataStructures.Collections
{
    /// <summary>
    /// Representa uma fila (First-In, First-Out).
    /// Implementada utilizando uma lista encadeada.
    /// </summary>
    /// <typeparam name="T">O tipo de elementos na fila.</typeparam>
    [Serializable]
    [ComVisible(true)]
    [DebuggerDisplay("Count = {Count}")]
    public class Queue<T> : ICollection<T>, ISerializable
    {
        private readonly LinkedList<T> _storage;

        /// <summary>
        /// Construtor padrão que inicializa uma nova instância da classe <see cref="Queue{T}"/>.
        /// </summary>
        public Queue()
        {
            _storage = new LinkedList<T>();
        }

        /// <summary>
        /// Obtém o número de elementos na fila.
        /// </summary>
        public int Count => _storage.Count;

        /// <summary>
        /// Obtém um valor que indica se a fila está vazia.
        /// </summary>
        public bool IsEmpty => Count == 0;

        /// <summary>
        /// Obtém um valor que indica se a fila é somente leitura.
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Método que retorna o primeiro elemento da fila sem removê-lo.
        /// </summary>
        /// <returns>O primeiro elemento da fila</returns>
        public T Peek()
        {
            if(_storage.Count == 0)
                throw new InvalidOperationException("A fila não contém itens!");

            return _storage.GetFirst();
        }

        /// <summary>
        /// Método que adiciona um elemento ao final da fila.
        /// </summary>
        /// <param name="element">Elemento a ser adicionado na fila</param>
        public void Enqueue(T element)
        {
            _storage.AddLast(element);
        }

        /// <summary>
        /// Método que remove e retorna o primeiro elemento da fila.
        /// </summary>
        /// <returns>O primeiro elemento da fila</returns>
        public T Dequeue()
        {
            if(_storage.Count == 0)
                throw new InvalidOperationException("A fila está vazia!");

            var value = _storage.GetFirst();
            _storage.RemoveFirst();

            return value;
        }

        #region Serializable
        protected Queue(SerializationInfo info, StreamingContext context)
        {
            _storage = new LinkedList<T>();

            int count = (int)info.GetValue("Count", typeof(int));
            for (int i = 0; i < count; i++)
            {
                T item = (T)info.GetValue("Item" + i, typeof(T));
                _storage.AddLast(item);
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

        #region Enumerable

        /// <summary>
        /// Método que retorna um enumerador que itera por uma fila.
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            return new QueueEnumerator<T>(_storage);
        }

        /// <summary>
        /// Método que retorna um enumerador que itera por uma fila.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new QueueEnumerator<T>(_storage);
        }

        private class QueueEnumerator<Type> : IEnumerator<Type>
        {
            private readonly LinkedList<Type> _storage;

            private LinkedNode<Type> _current;

            public QueueEnumerator(LinkedList<Type> storage)
            {
                _storage = storage;
            }

            public Type Current => _current.Value;

            object IEnumerator.Current => Current;

            public void Dispose() { }

            public bool MoveNext()
            {
                _current = _current is null ? _storage.Head : _current.Next;

                if (_current == null)
                    return false;

                return true;
            }

            public void Reset()
            {
                _current = null;
            }
        }
        #endregion

        #region Collection
        void ICollection<T>.Add(T item)
        {
            _storage.AddLast(item);
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
        /// Método que remove todos os elementos da fila.
        /// </summary>
        public void Clear()
        {
            _storage.Clear();
        }
        #endregion
    }
}
