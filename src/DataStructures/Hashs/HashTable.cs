using DataStructures.Extensions;
using System.Collections;
using System.Runtime.InteropServices;

namespace DataStructures.Hashs
{
    [ComVisible(true)]
    public class HashTable<T> : IEnumerable<T>
    {
        private readonly Bucket<T>[] _table;
        public HashTable(int size)
        {
            _table = new Bucket<T>[size];
        }

        /// <summary>
        /// Adiciona ou Obtém um item na HashTable
        /// </summary>
        /// <param name="key">Chave de hashing</param>
        /// <returns></returns>
        public T this[string key]
        {
            get => Get(key);
            set => Add(key, value);
        }

        /// <summary>
        /// Gera hash de acordo com a key. Pretendo melhorar isso futuramente.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int GenerateHashCode(string key)
        {
            int hash = 0;
            for (var i = 0; i < key.Length; i++)
            {
                hash = (hash + key.GetCharCodeAt(i) * i) % _table.Length;
            }

            return hash;
        }

        /// <summary>
        /// <para>Adiciona um valor vinculado a uma chave</para>
        /// <para>Melhor caso tem complexidade de O(1)</para>
        /// <para>Pior caso em caso de colisão de hashs tem complexidade de O(n)</para>
        /// </summary>
        /// <param name="key">Chave de hashing</param>
        /// <param name="value">Valor a ser armazenado</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void Add(string key, T value)
        {
            var hash = GenerateHashCode(key);
            var newBucket = new Bucket<T>(key, value);

            var rootBucket = _table[hash];
            if (rootBucket == null)
            {
                _table[hash] = newBucket;
                return;
            }

            var cursorBucket = rootBucket;
            while(cursorBucket.Overflow != null)
            {
                cursorBucket = cursorBucket.Overflow;

                if(cursorBucket.Key == key)
                    throw new InvalidOperationException("This key is already registered!");
            }

            cursorBucket.Overflow = newBucket;

            _table[hash] = rootBucket;
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

            var rootBucket = _table[hash];
            if (rootBucket == null)
            {
                value = default;
                return false;
            }

            if(rootBucket.Overflow == null)
            {
                value = rootBucket.Data;
                return true;
            }

            Bucket<T> cursorBucket = rootBucket;
            while (cursorBucket.Overflow != null)
            {
                cursorBucket = cursorBucket.Overflow;
                if (cursorBucket.Key == key)
                {
                    value = cursorBucket.Data;
                    return true;
                }
            }

            value = default;
            return false;
        }

        private T Get(string key)
        {
            var hash = GenerateHashCode(key);

            var rootBucket = _table[hash];
            if (rootBucket == null)
            {
                throw new InvalidOperationException("Não há itens na HashTable!");
            }

            if (rootBucket.Overflow == null)
            {
                return rootBucket.Data;
            }

            Bucket<T> cursorBucket = rootBucket;
            while (cursorBucket.Overflow != null)
            {
                cursorBucket = cursorBucket.Overflow;
                if (cursorBucket.Key == key)
                {
                    return cursorBucket.Data;
                }
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
            private Bucket<Type>? _current = default;

            private readonly Bucket<Type>[] _table;
            public HashTableEnumerator(Bucket<Type>[] table)
            {
                _table = table;
            }

            public Type Current => _current.Data;

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                _current = default;
            }

            public bool MoveNext()
            {
                if(_current == null && _index < _table.Length)
                {
                    _index++;
                    _current = _table[_index];
                }

                if(_current != null)
                {
                    _current = _current.Overflow;
                }

                return _index < _table.Length;
            }

            public void Reset()
            {
                _index = -1;
            }
        }
        #endregion

        private class Bucket<T>
        {
            public Bucket(string key, T data)
            {
                Key = key;
                Data = data;
            }

            public string Key { get; }
            public T Data { get; }
            public Bucket<T>? Overflow { get; set; }
        }
    }
}
