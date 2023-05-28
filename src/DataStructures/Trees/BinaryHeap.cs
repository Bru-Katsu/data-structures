using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DataStructures.Trees
{
    /// <summary>
    /// Representa o modo do heap binário.
    /// </summary>
    [ComVisible(true)]
    public enum HeapMode
    {
        /// <summary>
        /// Representa o modo de heap mínimo.
        /// </summary>
        MinHeap,

        /// <summary>
        /// Representa o modo de heap máximo.
        /// </summary>
        MaxHeap
    }

    /// <summary>
    /// Representa uma estrutura de dados de heap binário.
    /// </summary>
    /// <typeparam name="T">O tipo dos elementos no heap binário. Deve implementar <see cref="IComparable{T}"/> para permitir comparação entre elementos.</typeparam>
    [ComVisible(true)]
    [DebuggerDisplay("Count = {Count}")]
    public class BinaryHeap<T> : ICollection<T> where T : IComparable<T>
    {
        private readonly List<T> _storage = new();
        private readonly HeapMode _mode;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="BinaryHeap{T}"/> com o modo de heap especificado.
        /// </summary>
        /// <param name="mode">O modo de heap a ser utilizado (padrão é <see cref="HeapMode.MaxHeap"/>).</param>
        public BinaryHeap(HeapMode mode = HeapMode.MaxHeap)
        { 
            _mode = mode;
        }

        /// <summary>
        /// Obtém o número de elementos contidos no heap binário.
        /// </summary>
        public int Count => _storage.Count;

        /// <summary>
        /// Obtém um valor que indica se o heap binário é somente leitura.
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Obtém o elemento no topo do heap binário.
        /// </summary>
        public T Top => _storage[0];

        /// <summary>
        /// Adiciona um elemento ao heap binário.
        /// </summary>
        /// <param name="item">O elemento a ser adicionado ao heap binário.</param>
        public void Add(T item)
        {
            _storage.Add(item);
            Heapfy();
        }

        /// <summary>
        /// Realiza a operação de heapify para manter a propriedade de heap.
        /// </summary>
        /// <param name="currentIndex">O índice do elemento atual.</param>
        private void Heapfy()
        {
            var currentIndex = _storage.Count - 1;
            var parentIndex = GetParent(currentIndex);

            while (NeedSwap(currentIndex, parentIndex))
            {
                Swap(currentIndex, parentIndex);
                currentIndex = parentIndex;
                parentIndex = GetParent(currentIndex);
            }
        }

        private bool NeedSwap(int currentIndex, int parentIndex) => _mode == HeapMode.MaxHeap ? IsGreather(currentIndex, parentIndex) : IsLess(currentIndex, parentIndex);

        /// <summary>
        /// Troca dois elementos no heap binário.
        /// </summary>
        /// <param name="a">O índice do primeiro elemento.</param>
        /// <param name="b">O índice do segundo elemento.</param>
        private void Swap(int a, int b)
        {
            var valueA = _storage[a];
            var valueB = _storage[b];

            _storage[a] = valueB;
            _storage[b] = valueA;
        }

        /// <summary>
        /// Verifica se o elemento no índice especificado é maior que seu pai.
        /// </summary>
        /// <param name="currentIndex">O índice do elemento atual.</param>
        /// <param name="otherIndex">O índice do outro elemento.</param>
        /// <returns><c>true</c> se o elemento atual for maior o outro; caso contrário, <c>false</c>.</returns>
        private bool IsGreather(int currentIndex, int otherIndex)
        {
            if (otherIndex == -1)
                return false;

            var current = _storage[currentIndex];
            var other = _storage[otherIndex];
            return current.CompareTo(other) > 0;
        }

        /// <summary>
        /// Verifica se o elemento no índice especificado é menor que seu pai.
        /// </summary>
        /// <param name="currentIndex">O índice do elemento atual.</param>
        /// <param name="otherIndex">O índice do outro elemento.</param>
        /// <returns><c>true</c> se o elemento atual for menor que seu pai; caso contrário, <c>false</c>.</returns>
        private bool IsLess(int currentIndex, int otherIndex)
        {
            if (otherIndex == -1)
                return false;

            var current = _storage[currentIndex];
            var other = _storage[otherIndex];
            return current.CompareTo(other) < 0;
        }

        /// <summary>
        /// Obtém o índice do pai do elemento no índice especificado.
        /// </summary>
        /// <param name="index">O índice do elemento.</param>
        /// <returns>O índice do pai do elemento.</returns>
        public int GetParent(int index)
        {
            return index == 0 ? -1 : (index - 1) / 2;
        }

        /// <summary>
        /// Obtém o índice do filho esquerdo do elemento no índice especificado.
        /// </summary>
        /// <param name="index">O índice do elemento.</param>
        /// <returns>O índice do filho esquerdo do elemento.</returns>
        public int GetLeftChild(int index)
        {
            return (index * 2) + 1;
        }

        /// <summary>
        /// Obtém o índice do filho direito do elemento no índice especificado.
        /// </summary>
        /// <param name="index">O índice do elemento.</param>
        /// <returns>O índice do filho direito do elemento.</returns>
        public int GetRightChild(int index)
        {
            return (index * 2) + 2;
        }

        /// <summary>
        /// Remove todos os elementos do heap binário.
        /// </summary>
        public void Clear()
        {
            _storage.Clear();
        }

        /// <summary>
        /// Determina se o heap binário contém um valor específico.
        /// </summary>
        /// <param name="item">O valor a ser localizado no heap binário.</param>
        /// <returns><c>true</c> se o heap binário contiver o valor especificado; caso contrário, <c>false</c>.</returns>
        public bool Contains(T item)
        {
            return _storage.Contains(item);
        }

        /// <summary>
        /// Copia os elementos do heap binário para um <see cref="Array"/>, começando por um determinado índice do <see cref="Array"/>.
        /// </summary>
        /// <param name="array">O <see cref="Array"/> unidimensional que é o destino dos elementos copiados do heap binário.</param>
        /// <param name="arrayIndex">O índice baseado em zero no <paramref name="array"/> a partir do qual a cópia começa.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            _storage.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Retorna um enumerador que itera pelo heap binário.
        /// </summary>
        /// <returns>Um enumerador que pode ser usado para iterar pelo heap binário.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return _storage.GetEnumerator();
        }

        /// <summary>
        /// Remove a primeira ocorrência de um objeto específico do heap binário.
        /// </summary>
        /// <param name="item">O objeto a ser removido do heap binário.</param>
        /// <returns><c>true</c> se <paramref name="item"/> for removido com sucesso do heap binário; caso contrário, <c>false</c>. Este método também retorna <c>false</c> se <paramref name="item"/> não for encontrado no heap binário.</returns>
        public bool Remove(T item)
        {
            var removed = _storage.Remove(item);
            if (removed)
            {
                Heapfy();
            }
            
            return removed;
        }

        /// <summary>
        /// Retorna um enumerador que itera pelo heap binário.
        /// </summary>
        /// <returns>Um enumerador que pode ser usado para iterar pelo heap binário.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _storage.GetEnumerator();
        }
    }
}
