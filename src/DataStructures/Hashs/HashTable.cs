using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DataStructures.Hashs
{
    /// <summary>
    /// Representa uma implementação de HashTable
    /// </summary>
    /// <typeparam name="T">Tipo do objeto a ser armazenado na HashTable</typeparam>
    [ComVisible(true)]
    [DebuggerDisplay("Count = {Count}")]
    public class HashTable<T> : IEnumerable<T>
    {
        private int _count = 0;
        private readonly HashBucket<T>[] _table;

        /// <summary>
        /// Inicializa uma nova instância da classe HashTable
        /// </summary>
        /// <param name="size">Tamanho da HashTable</param>
        public HashTable(int size)
        {
            _table = new HashBucket<T>[size];
        }

        /// <summary>
        /// Propriedade indexada para adicionar ou obter um item na HashTable
        /// </summary>
        /// <param name="key">Chave de hashing</param>
        /// <returns>Item na HashTable</returns>
        /// <exception cref="ArgumentNullException">Exceção é lançada se a chave for nula ou em branco</exception>
        public T this[string key]
        {
            get => Get(key);
            set => Add(key, value);
        }

        /// <summary>
        /// Obtém a quantidade de itens na HashTable
        /// </summary>
        public int Count => _count;

        /// <summary>
        /// Gera hash de acordo com a key.
        /// </summary>
        /// <param name="key">Chave a ser usada para gerar hash</param>
        /// <returns>Hash gerado</returns>
        private int GenerateHashCode(string key)
        {
            return Math.Abs(key.GetHashCode()) % _table.Length;
        }

        /// <summary>
        /// Adiciona um valor vinculado a uma chave na HashTable
        /// </summary>
        /// <param name="key">Chave de hashing</param>
        /// <param name="value">Valor a ser armazenado</param>
        /// <exception cref="ArgumentNullException">Exceção é lançada se a chave for nula ou em branco</exception>
        public void Add(string key, T value)
        {
            if(string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key), "Não é permitido chave nula ou em branco!");

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
        /// Tentar obter um valor armazenado na HashTable
        /// </summary>
        /// <param name="key">Chave de hashing</param>
        /// <param name="value">Valor armazenado</param>
        /// <returns>Booleano indicando se o valor foi encontrado ou não</returns>
        /// <exception cref="ArgumentNullException">Exceção é lançada se a chave for nula ou em branco</exception>
        public bool TryGet(string key, out T value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key), "Não é permitido chave nula ou em branco!");

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
        /// Remove um registro da HashTable
        /// </summary>
        /// <param name="key">Chave de hashing</param>
        /// <exception cref="ArgumentNullException">Exceção é lançada se a chave for nula ou em branco</exception>
        public void Remove(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key), "Não é permitido chave nula ou em branco!");

            var hash = GenerateHashCode(key);

            var bucket = _table[hash];

            if (bucket == null)
                throw new ArgumentException("Não há itens para a chave informada!", nameof(key));

            bucket.Remove(key);

            if (bucket.Length == 0)
                _table[hash] = null;

            _count--;
        }

        /// <summary>
        /// Obtém um valor armazenado na HashTable
        /// </summary>
        /// <param name="key">Chave de hashing</param>
        /// <returns>Valor armazenado</returns>
        /// <exception cref="ArgumentNullException">Exceção é lançada se a chave for nula ou em branco</exception>
        public T Get(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key), "Não é permitido chave nula ou em branco!");

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
    /// Classe que implementa uma lista encadeada usada para armazenar elementos do tipo `T`
    /// associados a uma chave.
    /// </summary>
    internal class HashBucket<T> : IEnumerable<HashBucketNode<T>>
    {
        private int _length;
        private HashBucketNode<T> _head;
        private HashBucketNode<T> _tail;

        /// <summary>
        /// A propriedade `Length` retorna a quantidade de elementos armazenados na lista.
        /// </summary>
        public int Length => _length;

        /// <summary>
        /// A propriedade `Head` retorna o primeiro elemento da lista.
        /// </summary>
        public HashBucketNode<T> Head => _head;

        /// <summary>
        /// A propriedade `Tail` retorna o último elemento da lista.
        /// </summary>
        public HashBucketNode<T> Tail => _tail;

        /// <summary>
        /// Construtor padrão da classe.
        /// </summary>
        public HashBucket() { }

        /// <summary>
        /// Construtor que inicializa a classe com a chave e o valor informados.
        /// </summary>
        /// <param name="key">A chave de hashing do elemento a ser armazenado.</param>
        /// <param name="value">O valor do elemento a ser armazenado.</param>
        public HashBucket(string key, T value) 
        { 
            Initialize(key, value);
        }

        /// <summary>
        /// Método que adiciona um elemento à lista com a chave e o valor informados.
        /// </summary>
        /// <param name="key">A chave de hashing do elemento a ser adicionado.</param>
        /// <param name="value">O valor do elemento a ser adicionado.</param>
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

        /// <summary>
        /// Método que remove um elemento da lista a partir da chave informada.
        /// </summary>
        /// <param name="key">A chave de hashing do elemento a ser removido.</param>
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

        /// <summary>
        /// Inicializa um novo nó de bucket da hashtable
        /// </summary>
        /// <param name="key">A chave de hashing</param>
        /// <param name="value">O valor do nó</param>
        private void Initialize(string key, T value)
        {
            _head = new HashBucketNode<T>(key, value);
            _tail = _head;
            _length++;
        }

        /// <summary>
        /// Obtém o enumerador para os nós do bucket
        /// </summary>
        /// <returns>O enumerador para os nós do bucket</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new BucketEnumerator<T>(_head);
        }

        /// <summary>
        /// Obtém o enumerador para os nós do bucket
        /// </summary>
        /// <returns>O enumerador para os nós do bucket</returns>
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
