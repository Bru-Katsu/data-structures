using DataStructures.Hashs;
using Xunit;

namespace DataStructures.Tests
{
    public class HashTableTests
    {
        [Fact(DisplayName = "Ao adicionar, deve conter o item na hashtable")]
        [Trait(nameof(HashTable<int>), nameof(HashTable<int>.Add))]
        public void Adicionar_HashSemItens_DeveConterItem()
        {
            //arrange
            var valor = 2;
            var hashTable = new HashTable<int>(10)
            {
                ["valor"] = valor
            };

            //act
            bool contem = hashTable.TryGet("valor", out var valor2);

            //assert
            Assert.True(contem);
            Assert.Equal(valor, valor2);
        }

        [Fact(DisplayName = "Ao adicionar no mesmo endereçamento, deve conter o item na hashtable, de forma encadeada no bucket")]
        [Trait(nameof(HashTable<int>), nameof(HashTable<int>.Add))]
        public void Adicionar_HashComMesmoEnderecamento_DeveAgruparEncadeadoNoBucket()
        {
            //arrange
            var hashTable = new HashTable<int>(1)
            {
                ["dia"] = 28,
                ["mês"] = 12,
                ["ano"] = 2077
            };

            //act
            bool contem = hashTable.TryGet("ano", out var valor);

            //assert
            Assert.True(contem);
            Assert.Equal(2077, valor);
        }
    }
}
