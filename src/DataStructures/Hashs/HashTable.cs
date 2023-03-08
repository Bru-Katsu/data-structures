using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DataStructures.Hashs
{
    /// <summary>
    /// Representa uma implementação de HashTable.
    /// </summary>
    /// <typeparam name="TKey">Tipo do objeto usado para gerar a chave de hashing.</typeparam>
    /// <typeparam name="TValue">Tipo do objeto a ser armazenado na HashTable.</typeparam>
    [ComVisible(true)]
    [DebuggerDisplay("Count = {Count}")]
    public class HashTable<TKey, TValue> : IEnumerable<HashTableItem<TKey, TValue>>
    {
        private int _count = 0;
        private readonly HashBucket<TKey, TValue>[] _table;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="HashTable{TKey, TValue}"/>.
        /// </summary>
        public HashTable() : this(10) { }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="HashTable{TKey, TValue}"/>.
        /// </summary>
        /// <param name="capacity">Tamanho da HashTable.</param>
        public HashTable(int capacity)
        {
            _table = new HashBucket<TKey, TValue>[capacity];
        }

        /// <summary>
        /// Propriedade indexada para adicionar ou obter um item na HashTable.
        /// </summary>
        /// <param name="key">Chave de hashing.</param>
        /// <returns>Item na HashTable.</returns>
        /// <exception cref="ArgumentNullException">Exceção é lançada se a chave for nula ou em branco.</exception>
        public TValue this[TKey key]
        {
            get => Get(key);
            set => Add(key, value);
        }

        /// <summary>
        /// Obtém a quantidade de itens na HashTable.
        /// </summary>
        public int Count => _count;

        /// <summary>
        /// Gera hash de acordo com a key.
        /// </summary>
        /// <param name="key">Chave a ser usada para gerar hash.</param>
        /// <returns>Hash gerado.</returns>
        private int GenerateHashCode(TKey key)
        {
            return Math.Abs(key.GetHashCode()) % _table.Length;
        }

        /// <summary>
        /// Adiciona um valor vinculado a uma chave na HashTable.
        /// </summary>
        /// <param name="key">Chave de hashing.</param>
        /// <param name="item">Valor a ser armazenado.</param>
        /// <exception cref="ArgumentNullException">Exceção é lançada se a chave for nula ou em branco.</exception>
        public void Add(TKey key, TValue item)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key), "Não é permitido chave nula ou em branco!");

            var hash = GenerateHashCode(key);

            var bucket = _table[hash];
            if (bucket == null)
            {
                _table[hash] = new HashBucket<TKey, TValue>() { { key, item } };
                _count++;
                return;
            }

            bucket.Add(key, item);

            _table[hash] = bucket;
            _count++;
        }

        /// <summary>
        /// Tentar obter um valor armazenado na HashTable
        /// </summary>
        /// <param name="key">Chave de hashing.</param>
        /// <param name="item">Valor armazenado.</param>
        /// <returns>Booleano indicando se o valor foi encontrado ou não.</returns>
        /// <exception cref="ArgumentNullException">Exceção é lançada se a chave for nula ou em branco.</exception>
        public bool TryGet(TKey key, out TValue item)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key), "Não é permitido chave nula ou em branco!");

            var hash = GenerateHashCode(key);

            var bucket = _table[hash];
            if (bucket == null)
            {
                item = default;
                return false;
            }

            foreach (var node in bucket)
            {
                if (node.Key.Equals(key))
                {
                    item = node.Item;
                    return true;
                }
            }

            item = default;
            return false;
        }

        /// <summary>
        /// Remove um registro da HashTable.
        /// </summary>
        /// <param name="key">Chave de hashing.</param>
        /// <exception cref="ArgumentNullException">Exceção é lançada se a chave for nula ou em branco.</exception>
        public void Remove(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key), "Não é permitido chave nula ou em branco!");

            var hash = GenerateHashCode(key);

            var bucket = _table[hash];

            if (bucket == null)
                throw new ArgumentException("Não há itens para a chave informada!", nameof(key));

            bucket.Remove(key);

            if (bucket.Count == 0)
                _table[hash] = null;

            _count--;
        }

        /// <summary>
        /// Obtém um valor armazenado na HashTable.
        /// </summary>
        /// <param name="key">Chave de hashing.</param>
        /// <returns>Valor armazenado.</returns>
        /// <exception cref="ArgumentNullException">Exceção é lançada se a chave for nula ou em branco.</exception>
        /// <exception cref="InvalidOperationException">Exceção é lançada se não existir registro para a chave informada.</exception>
        public TValue Get(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key), "Não é permitido chave nula!");

            var hash = GenerateHashCode(key);

            var bucket = _table[hash];

            if (bucket == null)
                throw new InvalidOperationException("Não há itens na HashTable!");

            foreach (var item in bucket)
            {
                if (item.Key.Equals(key))
                    return item.Item;
            }

            throw new InvalidOperationException("Não existe item para a chave informada!");
        }

        #region Enumerable
        public IEnumerator<HashTableItem<TKey, TValue>> GetEnumerator()
        {
            return new HashTableEnumerator<TKey, TValue>(_table);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new HashTableEnumerator<TKey, TValue>(_table);
        }

        private class HashTableEnumerator<TypeKey, TypeValue> : IEnumerator<HashTableItem<TypeKey, TypeValue>>
        {
            private int _index = -1;
            private HashBucketNode<TypeKey, TypeValue> _current = default;
            private HashBucket<TypeKey, TypeValue>? _bucket = default;

            private readonly HashBucket<TypeKey, TypeValue>[] _table;
            public HashTableEnumerator(HashBucket<TypeKey, TypeValue>[] table)
            {
                _table = table;
            }

            public HashTableItem<TypeKey, TypeValue> Current => new(_current.Key, _current.Item);

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                _bucket = default;
            }

            public bool MoveNext()
            {
                if (_current == null && _index < _table.Length)
                {
                    while (_bucket == null && _index < _table.Length)
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
    /// Representa um bucket da classe <see cref="HashTable{TKey,TValue}"/>.
    /// </summary>
    /// <typeparam name="TKey">Tipo do objeto usado para gerar a chave de hashing.</typeparam>
    /// <typeparam name="TValue">O tipo de elementos na HashTable.</typeparam>
    [ComVisible(true)]
    [DebuggerDisplay("Count = {Count}")]
    internal class HashBucket<TKey, TValue> : IEnumerable<HashBucketNode<TKey, TValue>>
    {
        private int _count;
        private HashBucketNode<TKey, TValue> _head;
        private HashBucketNode<TKey, TValue> _tail;

        /// <summary>
        /// A propriedade retorna a quantidade de elementos armazenados na lista.
        /// </summary>
        public int Count => _count;

        /// <summary>
        /// A propriedade retorna o primeiro elemento da lista.
        /// </summary>
        public HashBucketNode<TKey, TValue> Head => _head;

        /// <summary>
        /// A propriedade retorna o último elemento da lista.
        /// </summary>
        public HashBucketNode<TKey, TValue> Tail => _tail;

        /// <summary>
        /// Construtor padrão da classe.
        /// </summary>
        public HashBucket() { }

        /// <summary>
        /// Construtor que inicializa a classe com a chave e o valor informados.
        /// </summary>
        /// <param name="key">A chave de hashing do elemento a ser armazenado.</param>
        /// <param name="value">O valor do elemento a ser armazenado.</param>
        public HashBucket(TKey key, TValue value)
        {
            Initialize(key, value);
        }

        /// <summary>
        /// Método que adiciona um elemento à lista com a chave e o valor informados.
        /// </summary>
        /// <param name="key">A chave de hashing do elemento a ser adicionado.</param>
        /// <param name="value">O valor do elemento a ser adicionado.</param>
        public void Add(TKey key, TValue value)
        {
            if (_head == null)
            {
                Initialize(key, value);
                return;
            }

            _tail.Next = new HashBucketNode<TKey, TValue>(key, value);
            _count++;
        }

        /// <summary>
        /// Método que remove um elemento da lista a partir da chave informada.
        /// </summary>
        /// <param name="key">A chave de hashing do elemento a ser removido.</param>
        public void Remove(TKey key)
        {
            if (_head.Next == null)
            {
                if (!_head.Key.Equals(key))
                    throw new ArgumentException("Não há itens para a chave informada!", nameof(key));

                _head = null;
                return;
            }

            HashBucketNode<TKey, TValue> current = _head;
            HashBucketNode<TKey, TValue> previous = default;

            while (!current.Key.Equals(key) && current.Next != null)
            {
                previous = current;
                current = current.Next;
            }

            if (!current.Key.Equals(key))
                throw new ArgumentException("Não há itens para a chave informada!", nameof(key));

            previous.Next = current.Next;
        }

        /// <summary>
        /// Inicializa um novo nó de bucket da hashtable
        /// </summary>
        /// <param name="key">A chave de hashing</param>
        /// <param name="item">O valor do nó</param>
        private void Initialize(TKey key, TValue item)
        {
            _head = new HashBucketNode<TKey, TValue>(key, item);
            _tail = _head;
            _count++;
        }

        /// <summary>
        /// Obtém o enumerador para os nós do bucket
        /// </summary>
        /// <returns>O enumerador para os nós do bucket</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new BucketEnumerator<TKey, TValue>(_head);
        }

        /// <summary>
        /// Obtém o enumerador para os nós do bucket
        /// </summary>
        /// <returns>O enumerador para os nós do bucket</returns>
        public IEnumerator<HashBucketNode<TKey, TValue>> GetEnumerator()
        {
            return new BucketEnumerator<TKey, TValue>(_head);
        }

        private class BucketEnumerator<TypeKey, Type> : IEnumerator<HashBucketNode<TypeKey, Type>>
        {
            private readonly HashBucketNode<TypeKey, Type> _head;
            private HashBucketNode<TypeKey, Type> _current;

            public BucketEnumerator(HashBucketNode<TypeKey, Type> head)
            {
                _head = head;
            }

            public HashBucketNode<TypeKey, Type> Current => _current;

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

    /// <summary>
    /// Representa um nó de um bucket da classe <see cref="HashBucket{TKey, TValue}"/>.
    /// </summary>
    /// <typeparam name="TKey">Tipo do objeto usado para gerar a chave de hashing.</typeparam>
    /// <typeparam name="TValue">O tipo de elementos no HashBucket.</typeparam>
    internal class HashBucketNode<TKey, TValue>
    {
        /// <summary>
        /// Construtor padrão que inicializa uma nova instância da classe <see cref="HashBucketNode{TKey, TValue}"/>.
        /// </summary>
        /// <param name="key">Chave de hashing.</param>
        /// <param name="item">Item armazenado.</param>
        public HashBucketNode(TKey key, TValue item)
        {
            Key = key;
            Item = item;
        }

        /// <summary>
        /// Obtém a chave.
        /// </summary>
        public TKey Key { get; }

        /// <summary>
        /// Obtém o item armazenado.
        /// </summary>
        public TValue Item { get; }

        /// <summary>
        /// Obtém ou define o próximo nó.
        /// </summary>
        public HashBucketNode<TKey, TValue> Next { get; set; }
    }

    /// <summary>
    /// Representa um item na HashTable da classe <see cref="HashTable{TKey,TValue}"/>.
    /// </summary>
    /// <typeparam name="TKey">Tipo do objeto usado para gerar a chave de hashing.</typeparam>
    /// <typeparam name="TValue">O tipo de elementos na HashTable.</typeparam>
    [ComVisible(true)]
    public struct HashTableItem<TKey, TValue>
    {
        /// <summary>
        /// Inicializa uma nova instância
        /// </summary>
        /// <param name="key">Chave de hashing</param>
        /// <param name="value">Valor a ser armazenado</param>
        public HashTableItem(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        /// Obtém o objeto usado como chave de hash.
        /// </summary>
        public TKey Key { get; }

        /// <summary>
        /// Obtém o valor armazenado para a chave.
        /// </summary>
        public TValue Value { get; }
    }
}
