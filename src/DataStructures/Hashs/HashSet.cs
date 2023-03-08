using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DataStructures.Hashs
{
    /// <summary>
    /// Representa uma implementação de um HashSet.
    /// </summary>
    /// <typeparam name="T">Tipo do objeto a ser armazenado n HashSet.</typeparam>
    [ComVisible(true)]
    [DebuggerDisplay("Count = {Count}")]    
    public class HashSet<T> : ICollection<T>
    {
        private int _count = 0;
        private readonly HashSetBucket<T>[] _set;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="HashSet{T}"/>.
        /// </summary>
        /// <param name="size">Tamanho do HashSet.</param>
        public HashSet() : this(10) { }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="HashSet{T}"/>.
        /// </summary>
        /// <param name="capacity">Capacidade de armazenamento do HashSet.</param>
        public HashSet(int capacity)
        {
            _set = new HashSetBucket<T>[capacity];
        }

        /// <summary>
        /// Gera posição hash de acordo com a key.
        /// </summary>
        /// <param name="key">Chave a ser usada para gerar hash.</param>
        /// <returns>Hash gerado.</returns>
        private int GenerateHashCode(T item)
        {
            return Math.Abs(item.GetHashCode()) % _set.Length;
        }

        /// <summary>
        /// Obtém a quantidade de itens no HashSet.
        /// </summary>
        public int Count => _count;

        /// <summary>
        /// Obtém a quantidade de itens no HashSet.
        /// </summary>
        public bool IsReadOnly => throw new NotImplementedException();

        /// <summary>
        /// Adiciona um item no HashSet.
        /// </summary>
        /// <param name="item">Item a ser armazenado.</param>
        /// <exception cref="ArgumentNullException">Exceção é lançada se o item for nulo.</exception>
        public void Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Não é permitido item nulo!");

            var hash = GenerateHashCode(item);

            var bucket = _set[hash];
            if (bucket == null)
            {
                _set[hash] = new HashSetBucket<T>() { item };
                _count++;
                return;
            }

            bucket.Add(item);

            _set[hash] = bucket;
            _count++;
        }

        /// <summary>
        /// Verifica se o item existe no HashSet.
        /// </summary>
        /// <param name="item">Item a ser encontrado</param>
        public bool Contains(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Não é permitido item nulo!");

            var hash = GenerateHashCode(item);

            var bucket = _set[hash];

            foreach (var bucketItem in bucket)
            {
                if (bucketItem.GetHashCode() == item.GetHashCode())
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Remove um item do HashSet.
        /// </summary>
        /// <param name="item">Item a ser removido.</param>
        /// <exception cref="ArgumentNullException">Exceção é lançada se o item for nulo.</exception>
        public bool Remove(T item)
        {
            var removed = false;

            if (item == null)
                throw new ArgumentNullException(nameof(item), "Não é permitido item nulo!");

            var hash = GenerateHashCode(item);

            var bucket = _set[hash];

            if (bucket == null)
                throw new ArgumentException("O item informado não existe!", nameof(item));

            int count = 0;
            foreach (var value in bucket)
            {
                if (value.Equals(item))
                {
                    bucket.Remove(item);
                    removed = true;
                }
                else
                    count++;
            }

            if (bucket.Count == 0)
                _set[hash] = null;

            _count--;
            return removed;
        }

        /// <summary>
        /// Remove todos os elementos do HashSet.
        /// </summary>
        public void Clear()
        {
            Array.Clear(_set, 0, _count);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), "Não é permitido array nulo!");

            if (arrayIndex < 0 || arrayIndex > array.Length)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), "Valor fora do intervalo do array!");

            if (array.Length - arrayIndex < Count)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), "Valor fora do intervalo do array!");

            int count = arrayIndex;
            foreach (var item in this)
            {
                array[count] = item;
                count++;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new HashSetEnumerable<T>(_set);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new HashSetEnumerable<T>(_set);
        }

        private class HashSetEnumerable<Type> : IEnumerator<Type>
        {
            private readonly HashSetBucket<T>[] _set;

            public HashSetEnumerable(HashSetBucket<T>[] set)
            {
                _set = set;
            }

            public Type Current => throw new NotImplementedException();

            object IEnumerator.Current => throw new NotImplementedException();

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public bool MoveNext()
            {
                throw new NotImplementedException();
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }
        }
    }

    /// <summary>
    /// Representa um bucket da classe <see cref="HashSet{T}"/>.
    /// </summary>
    /// <typeparam name="T">O tipo de elementos na HashTable.</typeparam>
    [ComVisible(true)]
    [DebuggerDisplay("Count = {Count}")]
    public sealed class HashSetBucket<T> : IEnumerable<T>
    {
        private int _count = 0;
        private HashSetBucketNode<T> _head;
        private HashSetBucketNode<T> _tail;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="HashSetBucket{T}"/>.
        /// </summary>
        public HashSetBucket() { }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="HashSetBucket{T}"/> com um item inicial.
        /// </summary>
        /// <param name="item">Item a ser armazenado.</param>
        public HashSetBucket(T item)
        {
            Initialize(item);
        }

        /// <summary>
        /// A propriedade retorna a quantidade de elementos armazenados na lista.
        /// </summary>
        public int Count => _count;

        /// <summary>
        /// A propriedade retorna o primeiro elemento da lista.
        /// </summary>
        public HashSetBucketNode<T> Head => _head;

        /// <summary>
        /// A propriedade retorna o último elemento da lista.
        /// </summary>
        public HashSetBucketNode<T> Tail => _tail;

        /// <summary>
        /// Inicializa um novo nó de bucket da hashtable
        /// </summary>
        /// <param name="key">A chave de hashing</param>
        /// <param name="item">O valor do nó</param>
        private void Initialize(T item)
        {
            _head = new HashSetBucketNode<T>(item);
            _tail = _head;
            _count++;
        }

        /// <summary>
        /// Método que adiciona um elemento à lista com a chave e o valor informados.
        /// </summary>
        /// <param name="item">Item a ser adicionado.</param>
        public void Add(T item)
        {
            if (_head == null)
            {
                Initialize(item);
                return;
            }

            _tail.Next = new HashSetBucketNode<T>(item);
            _count++;
        }

        /// <summary>
        /// Remove todos os elementos do bucket.
        /// </summary>
        public void Clear()
        {
            _head = null;
            _tail = null;
            _count = 0;
        }

        /// <summary>
        /// Remove um item do bucket.
        /// </summary>
        /// <param name="item">Item a ser removido.</param>
        /// <exception cref="ArgumentException">Caso não tenha o item informado.</exception>
        public void Remove(T item)
        {
            if (_head.Next == null)
            {
                if (!_head.Item.Equals(item))
                    throw new ArgumentException("O Bucket não contém o item informado!", nameof(item));

                _head = null;
                return;
            }

            HashSetBucketNode<T> current = _head;
            HashSetBucketNode<T> previous = default;

            while (!current.Item.Equals(item) && current.Next != null)
            {
                previous = current;
                current = current.Next;
            }

            if (!current.Item.Equals(item))
                throw new ArgumentException("O Bucket não contém o item informado!", nameof(item));

            previous.Next = current.Next;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new HashSetBucketEnumerator<T>(_head);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new HashSetBucketEnumerator<T>(_head);
        }

        private class HashSetBucketEnumerator<Type> : IEnumerator<Type>
        {
            private readonly HashSetBucketNode<Type> _head;
            private HashSetBucketNode<Type> _current;

            public HashSetBucketEnumerator(HashSetBucketNode<Type> head)
            {
                _head = head;
            }

            public Type Current => _current.Item;
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
                _current = null;
            }
        }
    }

    /// <summary>
    /// Representa um nó de um bucket da classe <see cref="HashSetBucket{TKey}"/>.
    /// </summary>
    /// <typeparam name="T">O tipo de elementos no HashBucket.</typeparam>
    public class HashSetBucketNode<T>
    {
        /// <summary>
        /// Construtor padrão que inicializa uma nova instância da classe <see cref="HashSetBucketNode{T}"/>.
        /// </summary>
        /// <param name="item">Item a ser armazenado.</param>
        public HashSetBucketNode(T item)
        {
            Item = item;
        }

        /// <summary>
        /// Obtém o item armazenado.
        /// </summary>
        public T Item { get; }

        /// <summary>
        /// Obtém ou define o próximo nó.
        /// </summary>
        public HashSetBucketNode<T> Next { get; set; }
    }
}
