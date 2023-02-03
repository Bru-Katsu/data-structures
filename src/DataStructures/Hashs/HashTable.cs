using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DataStructures.Hashs
{
    [ComVisible(true)]
    [DebuggerDisplay("Count = {Count}")]
    public class HashTable<T> : IEnumerable<T>
    {
        private int _count = 0;
        private readonly HashBucket<T>[] _table;
        public HashTable(int size)
        {
            _table = new HashBucket<T>[size];
        }

        /// <summary>
        /// Adiciona ou Obtém um item na HashTable
        /// </summary>
        /// <param name="key">Chave de hashing</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public T this[string key]
        {
            get => Get(key);
            set => Add(key, value);
        }

        /// <summary>
        /// Quantidade de itens
        /// </summary>
        public int Count => _count;

        /// <summary>
        /// Gera hash de acordo com a key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int GenerateHashCode(string key)
        {
            return Math.Abs(key.GetHashCode()) % _table.Length;
        }

        /// <summary>
        /// <para>Adiciona um valor vinculado a uma chave</para>
        /// <para>Complexidade O(1)</para>
        /// </summary>
        /// <param name="key">Chave de hashing</param>
        /// <param name="value">Valor a ser armazenado</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void Add(string key, T value)
        {
            var hash = GenerateHashCode(key);

            var bucket = _table[hash];
            if (bucket == null)
            {
                _table[hash] = new HashBucket<T>() { { key, value } };
                _count++;
                return;
            }

            bucket.Add(key, value);

            _table[hash] = bucket;
            _count++;
        }

        /// <summary>
        /// Retorna um valor armazenado na HashTable. Caso não exista nenhum valor para a chave, retorna como false.
        /// </summary>
        /// <param name="key">Chave de hashing</param>
        /// <param name="value">Valor armazenado</param>
        /// <returns></returns>
        public bool TryGet(string key, out T value)
        {
            var hash = GenerateHashCode(key);

            var bucket = _table[hash];
            if (bucket == null)
            {
                value = default;
                return false;
            }

            foreach (var item in bucket)
            {
                if (item.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase))
                {
                    value = item.Data;
                    return true;
                }                    
            }

            value = default;
            return false;
        }

        /// <summary>
        /// <para>Remove o registro da hashtable</para>
        /// <para>Complexidade O(n)</para>
        /// </summary>
        /// <param name="key">Chave de hashing</param>
        /// <exception cref="ArgumentException"></exception>
        public void Remove(string key)
        {
            var hash = GenerateHashCode(key);

            var bucket = _table[hash];

            if (bucket == null)
                throw new ArgumentException("Não há itens para a chave informada!", nameof(key));

            bucket.Remove(key);

            if (bucket.Length == 0)
                _table[hash] = null;

            _count--;
        }

        public T Get(string key)
        {
            var hash = GenerateHashCode(key);

            var bucket = _table[hash];

            if (bucket == null)
                throw new InvalidOperationException("Não há itens na HashTable!");

            foreach (var item in bucket)
            {
                if (item.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase))
                    return item.Data;
            }

            throw new InvalidOperationException("Não existe item para a chave informada!");
        }

        #region Enumerable
        public IEnumerator<T> GetEnumerator()
        {
            return new HashTableEnumerator<T>(_table);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new HashTableEnumerator<T>(_table);
        }

        private class HashTableEnumerator<Type> : IEnumerator<Type>
        {
            private int _index = -1;
            private HashBucketNode<Type> _current = default;
            private HashBucket<Type>? _bucket = default;

            private readonly HashBucket<Type>[] _table;
            public HashTableEnumerator(HashBucket<Type>[] table)
            {
                _table = table;
            }

            public Type Current => _current.Data;

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                _bucket = default;
            }

            public bool MoveNext()
            {
                if (_current == null && _index < _table.Length)
                {
                    while(_bucket == null && _index < _table.Length)
                    {
                        _index++;
                        _bucket = _table[_index];
                    }
                }

                _current = _current == null ? _bucket.Head : _current.Next;

                return _index < _table.Length;
            }

            public void Reset()
            {
                _index = -1;
            }
        }
        #endregion      
    }

    /// <summary>
    /// Linked list based hash bucket
    /// </summary>
    internal class HashBucket<T> : IEnumerable<HashBucketNode<T>>
    {
        private int _length;
        private HashBucketNode<T> _head;
        private HashBucketNode<T> _tail;

        public int Length => _length;
        public HashBucketNode<T> Head => _head;
        public HashBucketNode<T> Tail => _tail;

        public HashBucket() { }
        public HashBucket(string key, T value) 
        { 
            Initialize(key, value);
        }

        public void Add(string key, T value)
        {
            if (_head == null)
            {
                Initialize(key, value);
                return;
            }

            _tail.Next = new HashBucketNode<T>(key, value);
            _length++;
        }

        public void Remove(string key)
        {
            if (_head.Next == null)
            {
                if(_head.Key != key)
                    throw new ArgumentException("Não há itens para a chave informada!", nameof(key));

                _head = null;
                return;
            }

            HashBucketNode<T> current = _head;
            HashBucketNode<T> previous = default;

            while (current.Key != key && current.Next != null)
            {
                previous = current;
                current = current.Next;
            }

            if (!current.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase))
                throw new ArgumentException("Não há itens para a chave informada!", nameof(key));

            previous.Next = current.Next;
        }

        private void Initialize(string key, T value)
        {
            _head = new HashBucketNode<T>(key, value);
            _tail = _head;
            _length++;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new BucketEnumerator<T>(_head);
        }

        public IEnumerator<HashBucketNode<T>> GetEnumerator()
        {
            return new BucketEnumerator<T>(_head);
        }

        private class BucketEnumerator<Type> : IEnumerator<HashBucketNode<Type>>
        {
            private readonly HashBucketNode<Type> _head;
            private HashBucketNode<Type> _current;

            public BucketEnumerator(HashBucketNode<Type> head)
            {
                _head = head;
            }

            public HashBucketNode<Type> Current => _current;

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
    }

    internal class HashBucketNode<NodeType>
    {
        public HashBucketNode(string key, NodeType data)
        {
            Key = key;
            Data = data;
        }

        public string Key { get; }
        public NodeType Data { get; }
        public HashBucketNode<NodeType> Next { get; set; }
    }
}
