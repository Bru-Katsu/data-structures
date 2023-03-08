using DataStructures.Extensions;
using DataStructures.Hashs;
using Xunit;

namespace DataStructures.Tests.Hashs
{
    public class HashSetTests
    {
        #region Add
        [Fact(DisplayName = "Ao adicionar, deve conter o item no hashset")]
        [Trait("HashSet", "Add")]
        public void Adicionar_HashSetSemItens_DeveConterItem()
        {
            //arrange
            var valor = 2;
            var hashSet = new HashSet<int>(10)
            {
                valor
            };

            //act
            bool contem = hashSet.Contains(2);

            //assert
            Assert.True(contem);
        }

        [Fact(DisplayName = "Ao adicionar no mesmo endereçamento, deve conter o item no hashset, de forma encadeada no bucket")]
        [Trait("HashSet", "Add")]
        public void Adicionar_HashSetComMesmoEnderecamento_DeveAgruparEncadeadoNoBucket()
        {
            //arrange
            var hashSet = new HashSet<int>(1)
            {
                28,
                12,
                2077
            };

            //act
            bool contem = hashSet.Contains(2077);

            //assert
            Assert.True(contem);            
        }

        [Fact(DisplayName = "Ao adicionar no mesmo endereçamento, deve incrementar quantidade")]
        [Trait("HashSet", "Add")]
        public void Adicionar_HashSetComRegistrosNoMesmoBucket_DeveIncrementarQuantidade()
        {
            //arrange
            var hashSet = new HashSet<int>(1)
            {
                28,
                12,
                2077
            };

            //act
            hashSet.Add(7);

            //assert
            Assert.Equal(4, hashSet.Count);
        }

        [Fact(DisplayName = "Ao adicionar, deve incrementar quantidade")]
        [Trait("HashSet", "Add")]
        public void Adicionar_HashSetComRegistrosDispersos_DeveIncrementarQuantidade()
        {
            //arrange
            var hashSet = new HashSet<int>(4)
            {
                28,
                12,
                2077
            };

            //act
            hashSet.Add(7);

            //assert
            Assert.Equal(4, hashSet.Count);
        }
        #endregion

        #region Remove
        [Fact(DisplayName = "Ao remover no mesmo endereçamento, deve remover o item encadeado no bucket")]
        [Trait("HashSet", "Remove")]
        public void Remover_HashSetComMesmoEnderecamento_DeveRemoverEncadeadoNoBucket()
        {
            //arrange
            var hashSet = new HashSet<int>(1)
            {
                28,
                12,
                2077
            };

            //act
            hashSet.Remove(2077);
            bool contem = hashSet.Contains(2077);

            //assert
            Assert.False(contem);
        }

        [Fact(DisplayName = "Ao remover registro, deve decrementar a quantidade")]
        [Trait("HashSet", "Remove")]
        public void Remover_HashSetComRegistrosNoMesmoBucket_DeveDecrementarQuantidade()
        {
            //arrange
            var hashSet = new HashSet<int>(1)
            {
                28,
                12,
                2077
            };

            //act
            hashSet.Remove(2077);

            //assert
            Assert.Equal(2, hashSet.Count);
        }

        [Fact(DisplayName = "Ao remover registro, deve decrementar a quantidade")]
        [Trait("HashSet", "Remove")]
        public void Remover_HashSetComRegistrosDispersos_DeveDecrementarQuantidade()
        {
            //arrange
            var hashSet = new HashSet<int>(3)
            {
                28,
                12,
                2077
            };

            //act
            hashSet.Remove(2077);

            //assert
            Assert.Equal(2, hashSet.Count);
        }

        [Fact(DisplayName = "Ao remover no mesmo endereçamento, deve remover o item encadeado no bucket")]
        [Trait("HashSet", "Remove")]
        public void Remover_HashSetSemItemComRespectivaChave_DeveRetornarFalse()
        {
            //arrange
            var hashSet = new HashSet<int>(1) { 12 };

            //act & assert
            Assert.False(hashSet.Remove(15));
        }
        #endregion

        #region Extensions
        [Fact(DisplayName = "Ao chamar a extensão, deve converter um IEnumerable para uma Queue com os mesmos itens")]
        [Trait("HashSet", "ToHashSet")]
        public void ToHashSet_SomenteSeletorChave_DeveRetornarHashTableComItens()
        {
            //arrange
            //any non-sense data, just to make it works
            var lista = new System.Collections.Generic.List<int> { 1, 5, 36, 7, 9 };

            //act
            var hashSet = lista.ToHashSet();

            //assert
            Assert.Equal(lista.Count, hashSet.Count);

            foreach (var item in lista)
            {
                Assert.True(hashSet.Contains(item));
            }
        }
        #endregion
    }
}
