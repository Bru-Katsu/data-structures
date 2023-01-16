using System.Collections;
using System.Runtime.InteropServices;

namespace DataStructures.Listas
{
    [ComVisible(true)]
    public class Queue<T> : IEnumerable<T>
    {
        public Queue()
        {
            _storage = new T[_defaultCapacity];
        }

        public Queue(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException("Negative capacity is not supported!");

            _storage = new T[capacity];
            _hasCapacityDefined = true;
        }

        private const int _minimumGrow = 2;
        private const int _defaultCapacity = 10;

        private readonly bool _hasCapacityDefined = false;
        
        private int _internalSize = 0;
        private T[] _storage;

        private int _head;
        private int _tail;

        public int Size => _internalSize;
        public bool IsEmpty => Size == 0;

        /// <summary>
        /// Complexidade de O(1)
        /// </summary>
        /// <returns></returns>
        public T Peek()
        {
            if(IsEmpty)
                return default;

            return _storage[_head];
        }

        //O(n) only on expand, otherwise is O(1)
        public void Enqueue(T element)
        {
            if (_internalSize == _storage.Length)
            {
                if(_hasCapacityDefined)
                    throw new ArgumentOutOfRangeException("The Queue is full!");
                else
                    Expand();
            }

            _storage[_tail] = element;
            _tail++;
            _internalSize++;
        }

        /// <summary>
        /// Complexidade de O(1)
        /// </summary>
        /// <returns></returns>
        public T Dequeue()
        {
            if (IsEmpty)
                throw new ArgumentOutOfRangeException("Queue is Empty!");

            T dequeued = _storage[_head];
            _storage[_head] = default;
            _head = (_head + 1) % _storage.Length;

            _internalSize--;

            return dequeued;
        }

        //TODO: reavaliar esse método
        //public void Trim() => Redimensionate(_internalSize);

        private void Expand()
        {
            int newCapacity = (int)((long)_storage.Length * 2 / 100);
            if (newCapacity < _storage.Length + _minimumGrow)
            {
                newCapacity = _storage.Length + _minimumGrow;
            }

            Redimensionate(newCapacity);
        }

        private void Redimensionate(int toCapacity)
        {
            var newStorage = new T[toCapacity];
            if (_internalSize > 0)
            {
                if (_head < _tail)
                    Array.Copy(_storage, newStorage, _internalSize);                    
                else
                {
                    Array.Copy(_storage, _head, newStorage, 0, _storage.Length - _head);
                    Array.Copy(_storage, 0, newStorage, _storage.Length - _head, _tail);
                }
            }

            _storage = newStorage;
            _head = 0;
            _tail = (_internalSize == toCapacity) ? 0 : _internalSize;
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

        private class QueueEnumerator<T> : IEnumerator<T>
        {
            private readonly T[] _queue;
            private int _index = -1;

            public QueueEnumerator(T[] queue)
            {
                _queue = queue;
            }

            public T Current => _queue[_index];

            object IEnumerator.Current => Current;

            public void Dispose() { }

            public bool MoveNext()
            {
                _index++;
                return _index < _queue.Length;                
            }

            public void Reset()
            {
                _index = -1;
            }
        }
        #endregion
    }
}
