using System.Runtime.InteropServices;

namespace DataStructures.Listas
{
    [ComVisible(true)]
    [Serializable]
    public class Stack<T>
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

        //O(1)
        public T Peek()
        {
            if (!IsEmpty)
                return _storage[_internalSize];

            return default;
        }

        //O(n) only on resize, otherwise is O(1)
        public void Push(T element)
        {
            if (Size == _storage.Length)
            {
                if (_hasCapacityDefined)
                    throw new ArgumentOutOfRangeException("The Stack is full!");
                else
                    Array.Resize(ref _storage, _growthFactor * _storage.Length);
            }

            _internalSize++;
            _storage[_internalSize] = element;
        }

        //O(1)
        public T Pop()
        {
            if (IsEmpty)
                throw new InvalidOperationException("The Stack is empty!");

            var lastElement = _storage[_internalSize];
            _storage[_internalSize] = default;

            _internalSize--;

            return lastElement;
        }
    }
}
