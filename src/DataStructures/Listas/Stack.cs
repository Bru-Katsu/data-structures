using System.Collections;
using System.Runtime.InteropServices;

namespace DataStructures.Listas
{
    [ComVisible(true)]
    public class Stack<T> : IEnumerable<T>
    {
        public Stack()
        {
            _storage = new T[_defaultCapacity];
        }

        public Stack(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException("Negative capacity is not supported!");

            _hasCapacityDefined = true;
            _storage = new T[capacity];
        }

        private const int _defaultCapacity = 10;
        private const int _growthFactor = 2;

        private readonly bool _hasCapacityDefined = false;
        
        private int _internalSize = -1;
        private T[] _storage;

        public int Size => _internalSize + 1;
        public bool IsEmpty => Size == 0;

        /// <summary>
        /// Complexidade de O(1)
        /// </summary>
        /// <returns></returns>
        public T Peek()
        {
            if (!IsEmpty)
                return _storage[_internalSize];

            return default;
        }

        /// <summary>
        /// <para>Melhor caso tem complexidade de O(1)</para>
        /// <para>Pior caso (redimensionamento) tem complexidade de O(n)</para>
        /// </summary>
        /// <param name="item"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void Push(T item)
        {
            if (Size == _storage.Length)
            {
                if (_hasCapacityDefined)
                    throw new ArgumentOutOfRangeException("The Stack is full!");
                else
                    Array.Resize(ref _storage, _growthFactor * _storage.Length);
            }

            _internalSize++;
            _storage[_internalSize] = item;
        }

        /// <summary>
        /// Complexidade de O(1)
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public T Pop()
        {
            if (IsEmpty)
                throw new InvalidOperationException("The Stack is empty!");

            var lastItem = _storage[_internalSize];
            _storage[_internalSize] = default;

            _internalSize--;

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
            private readonly Type[] _stackedStorage;
            private int _tail;
            public StackEnumerator(Type[] stackedStorage)
            {
                _stackedStorage = stackedStorage;
                _tail = _stackedStorage.Length - 1;
            }

            public Type Current => _stackedStorage[_tail];

            object IEnumerator.Current => Current;

            public void Dispose() { }

            public bool MoveNext()
            {
                _tail--;
                return _tail >= 0;
            }

            public void Reset()
            {
                _tail = _stackedStorage.Length - 1;
            }
        }
        #endregion
    }
}
