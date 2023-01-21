using DataStructures.Hashs;
using System;
using Xunit;

namespace DataStructures.Tests
{
    public class HashTableTests
    {
        #region Add
        [Fact(DisplayName = "Ao adicionar, deve conter o item na hashtable")]
        [Trait(nameof(HashTable<int>), nameof(HashTable<int>.Add))]
        public void Adicionar_HashTableSemItens_DeveConterItem()
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
        public void Adicionar_HashTableComMesmoEnderecamento_DeveAgruparEncadeadoNoBucket()
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

        [Fact(DisplayName = "Ao adicionar no mesmo endereçamento, deve incrementar quantidade")]
        [Trait(nameof(HashTable<int>), nameof(HashTable<int>.Add))]
        public void Adicionar_HashTableComRegistrosNoMesmoBucket_DeveIncrementarQuantidade()
        {
            //arrange
            var hashTable = new HashTable<int>(1)
            {
                ["dia"] = 28,
                ["mês"] = 12,
                ["ano"] = 2077
            };

            //act
            hashTable.Add("dia_2", 7);

            //assert
            Assert.Equal(4, hashTable.Count);
        }

        [Fact(DisplayName = "Ao adicionar, deve incrementar quantidade")]
        [Trait(nameof(HashTable<int>), nameof(HashTable<int>.Add))]
        public void Adicionar_HashTableComRegistrosDispersos_DeveIncrementarQuantidade()
        {
            //arrange
            var hashTable = new HashTable<int>(4)
            {
                ["dia"] = 28,
                ["mês"] = 12,
                ["ano"] = 2077
            };

            //act
            hashTable.Add("dia_2", 7);

            //assert
            Assert.Equal(4, hashTable.Count);
        }
        #endregion

        #region Remove
        [Fact(DisplayName = "Ao remover no mesmo endereçamento, deve remover o item encadeado no bucket")]
        [Trait(nameof(HashTable<int>), nameof(HashTable<int>.Remove))]
        public void Remover_HashTableComMesmoEnderecamento_DeveRemoverEncadeadoNoBucket()
        {
            //arrange
            var hashTable = new HashTable<int>(1)
            {
                ["dia"] = 28,
                ["mês"] = 12,
                ["ano"] = 2077
            };

            //act
            hashTable.Remove("ano");
            bool contem = hashTable.TryGet("ano", out int valor);

            //assert
            Assert.False(contem);
            Assert.Equal(0, valor);
        }

        [Fact(DisplayName = "Ao remover registro, deve decrementar a quantidade")]
        [Trait(nameof(HashTable<int>), nameof(HashTable<int>.Remove))]
        public void Remover_HashTableComRegistrosNoMesmoBucket_DeveDecrementarQuantidade()
        {
            //arrange
            var hashTable = new HashTable<int>(1)
            {
                ["dia"] = 28,
                ["mês"] = 12,
                ["ano"] = 2077
            };

            //act
            hashTable.Remove("ano");

            //assert
            Assert.Equal(2, hashTable.Count);
        }

        [Fact(DisplayName = "Ao remover registro, deve decrementar a quantidade")]
        [Trait(nameof(HashTable<int>), nameof(HashTable<int>.Remove))]
        public void Remover_HashTableComRegistrosDispersos_DeveDecrementarQuantidade()
        {
            //arrange
            var hashTable = new HashTable<int>(3)
            {
                ["dia"] = 28,
                ["mês"] = 12,
                ["ano"] = 2077
            };

            //act
            hashTable.Remove("ano");

            //assert
            Assert.Equal(2, hashTable.Count);
        }

        [Fact(DisplayName = "Ao remover no mesmo endereçamento, deve remover o item encadeado no bucket")]
        [Trait(nameof(HashTable<int>), nameof(HashTable<int>.Remove))]
        public void Remover_HashTableSemItemComRespectivaChave_DeveRetornarException()
        {
            //arrange
            var hashTable = new HashTable<int>(1) { ["mês"] = 12 };

            //act & assert
            Assert.Throws<ArgumentException>(() => hashTable.Remove("ano"));            
        }
        #endregion

        #region Get
        [Fact(DisplayName = "Ao tentar acessar um registro com a hashtable sem itens, deve estourar exception")]
        [Trait(nameof(HashTable<int>), nameof(HashTable<int>.Get))]
        public void Get_HashTableSemItens_DeveRetornarException()
        {
            //arrange
            var hashTable = new HashTable<int>(1) { };

            //act & assert
            Assert.Throws<InvalidOperationException>(() => hashTable.Get("ano"));
        }

        [Fact(DisplayName = "Ao tentar acessar um registro com a hashtable sem itens, deve estourar exception")]
        [Trait(nameof(HashTable<int>), nameof(HashTable<int>.Get))]
        public void Get_HashTableSemChaveEspecifica_DeveRetornarException()
        {
            //arrange
            var hashTable = new HashTable<int>(1) { ["item"] = 50 };

            //act & assert
            Assert.Throws<InvalidOperationException>(() => hashTable.Get("chave_inexistente"));
        }

        [Fact(DisplayName = "Ao tentar acessar um registro com a hashtable sem itens, deve estourar exception")]
        [Trait(nameof(HashTable<int>), nameof(HashTable<int>.Get))]
        public void Get_HashTableComItens_DeveRetornarItem()
        {
            //arrange
            var hashTable = new HashTable<int>(1) { ["item"] = 50 };

            //act
            var valor = hashTable.Get("item");
            
            //assert
            Assert.Equal(50, valor);
        }
        #endregion
    }
}
