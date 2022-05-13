namespace DataStructures.Listas
{
    public class Pilha<T>
    {
        public Pilha() {
            _armazenamento = new List<T>();
        }

        public Pilha(int capacidade)
        {
            _capacidade = capacidade;
            _armazenamento = new List<T>(capacidade);
        }

        private readonly List<T> _armazenamento;
        private readonly int _capacidade = -1;

        private int _tamanhoInterno = -1;

        public T Topo => ObterElementoDoTopo();
        private T ObterElementoDoTopo()
        {
            if (!Vazia)
                return _armazenamento[_tamanhoInterno];

            return default;
        }

        public int Tamanho => _tamanhoInterno + 1;
        public bool Vazia => Tamanho == 0;

        public void Empilhar(T element)
        {
            if (Tamanho == _capacidade && _capacidade != -1)
                throw new InvalidOperationException("A pilha está cheia!");

            _tamanhoInterno++;
            _armazenamento.Add(element);
        }

        public T Desempilhar()
        {
            if (Vazia)
                throw new InvalidOperationException("A pilha está vazia!");

            _tamanhoInterno--;
            return _armazenamento[_tamanhoInterno + 1];
        }
    }
}
