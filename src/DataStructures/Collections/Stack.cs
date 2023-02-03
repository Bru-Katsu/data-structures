using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DataStructures.Collections
{
    /// <summary>
    /// A classe `Stack` representa uma estrutura de dados LIFO (Last In First Out). 
    /// Os itens são adicionados e removidos apenas na extremidade superior da pilha. 
    /// A classe é implementada usando uma lista duplamente encadeada.
    /// </summary>
    [ComVisible(true)]
    [DebuggerDisplay("Count = {Count}")]
    public class Stack<T> : IEnumerable<T>
    {
        private readonly DoubleLinkedList<T> _storage;

        /// <summary>
        /// Construtor padrão que inicializa uma nova instância da classe <see cref="Stack{T}"/>.
        /// </summary>
        public Stack()
        {
            _storage = new DoubleLinkedList<T>();
        }

        /// <summary>
        /// Obtém o número de elementos na pilha.
        /// </summary>
        public int Count => _storage.Count;

        /// <summary>
        /// Obtém um valor que indica se a pilha está vazia.
        /// </summary>
        public bool IsEmpty => Count == 0;

        /// <summary>
        /// Obtém o elemento no topo da pilha sem removê-lo.
        /// </summary>
        /// <returns>O elemento no topo da pilha.</returns>
        /// <exception cref="InvalidOperationException">Se a pilha estiver vazia.</exception>
        public T Peek()
        {
            if (!IsEmpty)
                return _storage.GetLast();

            throw new InvalidOperationException("A pilha está vazia!");
        }

        /// <summary>
        /// Adiciona um elemento ao topo da pilha.
        /// </summary>
        /// <param name="item">O elemento a ser adicionado.</param>
        public void Push(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Não é permitido valores nulos!");

            _storage.AddLast(item);
        }

        /// <summary>
        /// Remove e retorna o elemento no topo da pilha.
        /// </summary>
        /// <returns>O elemento removido do topo da pilha.</returns>
        /// <exception cref="InvalidOperationException">Se a pilha estiver vazia.</exception>
        public T Pop()
        {
            if (IsEmpty)
                throw new InvalidOperationException("A pilha está vazia!");

            var lastItem = _storage.GetLast();
            _storage.RemoveLast();

            return lastItem;
        }

        #region Enumerable
        /// <summary>
        /// Obtém um enumerador que itera através da pilha.
        /// </summary>
        /// <returns>Um enumerador que itera através da pilha.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new StackEnumerator<T>(_storage);
        }

        /// <summary>
        /// Obtém um enumerador que itera através da pilha.
        /// </summary>
        /// <returns>Um enumerador que itera através da pilha.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new StackEnumerator<T>(_storage);
        }

        private class StackEnumerator<Type> : IEnumerator<Type>
        {
            private readonly DoubleLinkedList<Type> _storage;
            private DoubleLinkedNode<Type> _current;
            public StackEnumerator(DoubleLinkedList<Type> storage)
            {
                _storage = storage;                
            }

            public Type Current => _current.Value;

            object IEnumerator.Current => Current;

            public void Dispose() 
            {
                _current = null;
            }

            public bool MoveNext()
            {
                _current = _current == null ? _storage.Tail : _current.Previous;

                if (_current == null)
                    return false;

                return true;
            }

            public void Reset()
            {
                _current = null;
            }
        }
        #endregion
    }
}
