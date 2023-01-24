using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace DataStructures.Listas
{
    [ComVisible(true)]
    [DebuggerDisplay("Count = {Count}")]
    public class Queue<T> : IEnumerable<T>
    {
        public Queue()
        {
            _storage = new LinkedList<T>();
        }
       
        private LinkedList<T> _storage;

        public int Count => _storage.Count;
        public bool IsEmpty => Count == 0;

        /// <summary>
        /// <para>Acessa o primeiro item da fila, mas sem remover</para>
        /// <para>Complexidade de O(1)</para>
        /// </summary>
        /// <returns></returns>
        public T Peek()
        {
            if(_storage.Count == 0)
                return default;

            return _storage.GetFirst();
        }

        /// <summary>
        /// <para>Adiciona um item na fila</para>
        /// <para>Complexidade de O(1)</para>
        /// </summary>
        /// <returns></returns>
        public void Enqueue(T element)
        {
            _storage.AddLast(element);
        }

        /// <summary>
        /// <para>Remove um item da fila</para>
        /// <para>Complexidade de O(1)</para>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public T Dequeue()
        {
            if(_storage.Count == 0)
                throw new InvalidOperationException("A fila está vazia!");

            var value = _storage.GetFirst();
            _storage.RemoveFirst();

            return value;
        }

        /// <summary>
        /// <para>Limpa a fila</para>
        /// <para>Complexidade de O(1)</para>
        /// </summary>
        public void Clear()
        {
            _storage.Clear();
        }

        #region Enumerable
        public IEnumerator<T> GetEnumerator()
        {
            return new QueueEnumerator<T>(_storage);
        }

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
