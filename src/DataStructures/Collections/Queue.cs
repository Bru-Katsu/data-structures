using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DataStructures.Collections
{
    /// <summary>
    /// Representa uma fila (First-In, First-Out).
    /// Implementada utilizando uma lista encadeada.
    /// </summary>
    /// <typeparam name="T">O tipo de elementos na fila.</typeparam>
    [ComVisible(true)]
    [DebuggerDisplay("Count = {Count}")]
    public class Queue<T> : IEnumerable<T>
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

        /// <summary>
        /// Método que remove todos os elementos da fila.
        /// </summary>
        public void Clear()
        {
            _storage.Clear();
        }

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
    }
}
