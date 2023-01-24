using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DataStructures.Listas
{
    [ComVisible(true)]
    [DebuggerDisplay("Count = {Count}")]
    public class Stack<T> : IEnumerable<T>
    {
        public Stack()
        {
            _storage = new DoubleLinkedList<T>();
        }

        private DoubleLinkedList<T> _storage;

        public int Count => _storage.Count;
        public bool IsEmpty => Count == 0;

        /// <summary>
        /// Complexidade de O(1)
        /// </summary>
        /// <returns></returns>
        public T Peek()
        {
            if (!IsEmpty)
                return _storage.GetLast();

            return default;
        }

        /// <summary>
        /// <para>Adiciona um novo registro na pilha</para>
        /// <para>Complexidade de O(1)</para>
        /// </summary>
        /// <param name="item"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void Push(T item)
        {
            _storage.AddLast(item);
        }

        /// <summary>
        /// Remove um item da pilha
        /// Complexidade de O(1)
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public T Pop()
        {
            if (IsEmpty)
                throw new InvalidOperationException("The Stack is empty!");

            var lastItem = _storage.GetLast();
            _storage.RemoveLast();

            return lastItem;
        }

        #region Enumerable
        public IEnumerator<T> GetEnumerator()
        {
            return new StackEnumerator<T>(_storage);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new StackEnumerator<T>(_storage);
        }

        private class StackEnumerator<Type> : IEnumerator<Type>
        {
            private readonly DoubleLinkedList<Type> _storage;
            private DoubleLinkedNode<Type> _current;
            public StackEnumerator(DoubleLinkedList<Type> storage)
            {
                _storage = storage;                
            }

            public Type Current => _current.Value;

            object IEnumerator.Current => Current;

            public void Dispose() 
            {
                _current = null;
            }

            public bool MoveNext()
            {
                _current = _current == null ? _storage.Tail : _current.Previous;

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
