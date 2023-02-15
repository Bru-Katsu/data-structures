namespace DataStructures.Extensions
{
    /// <summary>
    /// Classe responsável por prover métodos de extensão para converter coleções em estruturas de dados.
    /// </summary>
    public static class DataStructuresExtensions
    {
        /// <summary>
        /// Converte uma coleção em uma BinaryTree.
        /// </summary>
        /// <typeparam name="T">O tipo de dados dos elementos da BinaryTree.</typeparam>
        /// <param name="enumerable">A coleção a ser convertida.</param>
        /// <returns>Uma BinaryTree contendo os elementos da coleção.</returns>
        /// <exception cref="ArgumentNullException">Se o item estiver nulo.</exception>
        public static DataStructures.Trees.BinaryTree<T> ToBinaryTree<T>(this IEnumerable<T> enumerable) where T : IComparable<T>
        {
            var tree = new DataStructures.Trees.BinaryTree<T>();
            foreach (var item in enumerable)
                tree.Add(item);

            return tree;
        }

        /// <summary>
        /// Converte uma coleção em uma HashTable.
        /// </summary>
        /// <typeparam name="TKey">O tipo de dados da chave da HashTable.</typeparam>
        /// <typeparam name="TValue">O tipo de dados dos elementos da coleção.</typeparam>
        /// <param name="enumerable">A coleção a ser convertida.</param>
        /// <param name="keySelector">A função que retorna a chave de cada elemento da coleção.</param>
        /// <returns>Uma HashTable contendo os elementos da coleção.</returns>
        /// <exception cref="ArgumentNullException">Exceção é lançada se a chave for nula ou em branco.</exception>
        public static DataStructures.Hashs.HashTable<TKey, TValue> ToHashTable<TKey, TValue>(this IEnumerable<TValue> enumerable, Func<TValue, TKey> keySelector)
        {
            var hashTable = new DataStructures.Hashs.HashTable<TKey, TValue>(10);
            foreach (var item in enumerable)
            {
                var key = keySelector(item);
                hashTable.Add(key, item);
            }

            return hashTable;
        }

        /// <summary>
        /// Converte uma coleção em uma HashTable.
        /// </summary>
        /// <typeparam name="TKey">O tipo de dados da chave da HashTable.</typeparam>
        /// <typeparam name="TValue">O tipo de dados dos elementos da coleção.</typeparam>
        /// <typeparam name="TElement">O tipo de dados dos elementos a serem adicionados na HashTable.</typeparam>
        /// <param name="enumerable">A coleção a ser convertida.</param>
        /// <param name="keySelector">A função que retorna a chave de cada elemento da coleção.</param>
        /// <param name="elementSelector">A função que retorna o elemento a ser adicionado na HashTable.</param>
        /// <returns>Uma HashTable contendo os elementos da coleção.</returns>
        /// <exception cref="ArgumentNullException">Exceção é lançada se a chave for nula ou em branco.</exception>
        public static DataStructures.Hashs.HashTable<TKey, TElement> ToHashTable<TKey, TValue, TElement>(this IEnumerable<TValue> enumerable, Func<TValue, TKey> keySelector, Func<TValue, TElement> elementSelector)
        {
            var hashTable = new DataStructures.Hashs.HashTable<TKey, TElement>(10);
            foreach (var item in enumerable)
            {
                var key = keySelector(item);
                var element = elementSelector(item);
                hashTable.Add(key, element);
            }

            return hashTable;
        }

        /// <summary>
        /// Converte uma coleção em uma lista encadeada.
        /// </summary>
        /// <typeparam name="T">O tipo de dados dos elementos da lista.</typeparam>
        /// <param name="enumerable">A coleção a ser convertida.</param>
        /// <returns>Uma lista encadeada contendo os elementos da coleção.</returns>
        /// <exception cref="ArgumentNullException">Se o item estiver nulo.</exception>
        public static DataStructures.Collections.LinkedList<T> ToSingleLinkedList<T>(this IEnumerable<T> enumerable)
        {
            var list = new DataStructures.Collections.LinkedList<T>();
            foreach (var item in enumerable)
                list.AddLast(item);

            return list;
        }

        /// <summary>
        /// Converte uma coleção em uma lista duplamente encadeada.
        /// </summary>
        /// <typeparam name="T">O tipo de dados dos elementos da lista.</typeparam>
        /// <param name="enumerable">A coleção a ser convertida.</param>
        /// <returns>Uma lista duplamente encadeada contendo os elementos da coleção.</returns>
        /// <exception cref="ArgumentNullException">Se o item estiver nulo.</exception>
        public static DataStructures.Collections.DoubleLinkedList<T> ToDoublyLinkedList<T>(this IEnumerable<T> enumerable)
        {
            var list = new DataStructures.Collections.DoubleLinkedList<T>();
            foreach (var item in enumerable)
                list.AddLast(item);

            return list;
        }

        /// <summary>
        /// Converte uma coleção em uma fila.
        /// </summary>
        /// <typeparam name="T">O tipo de dados dos elementos da fila.</typeparam>
        /// <param name="enumerable">A coleção a ser convertida.</param>
        /// <returns>Uma fila contendo os elementos da coleção.</returns>
        public static DataStructures.Collections.Queue<T> ToQueue<T>(this IEnumerable<T> enumerable)
        {
            var list = new DataStructures.Collections.Queue<T>();
            foreach (var item in enumerable)
                list.Enqueue(item);

            return list;
        }

        /// <summary>
        /// Converte uma coleção em uma pilha.
        /// </summary>
        /// <typeparam name="T">O tipo de dados dos elementos da pilha.</typeparam>
        /// <param name="enumerable">A coleção a ser convertida.</param>
        /// <returns>Uma pilha contendo os elementos da coleção.</returns>
        public static DataStructures.Collections.Stack<T> ToStack<T>(this IEnumerable<T> enumerable)
        {
            var list = new DataStructures.Collections.Stack<T>();
            foreach (var item in enumerable)
                list.Push(item);

            return list;
        }
    }
}
