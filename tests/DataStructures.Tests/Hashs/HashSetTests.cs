using DataStructures.Extensions;
using DataStructures.Hashs;
using DataStructures.Tests.Fixtures.EqualityComparer;
using System;
using Xunit;

namespace DataStructures.Tests.Hashs
{
    [Collection(nameof(EqualityComparerCollection))]
    public class HashSetTests
    {
        private readonly EqualityComparerFixture _fixture;

        public HashSetTests(EqualityComparerFixture fixture)
        {
            _fixture = fixture;
        }

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

        #region Equality Comparision
        [Fact(DisplayName = "Ao buscar registro, deve retornar true")]
        [Trait("HashSet", "Contains")]
        public void Contains_EqualityComparerCorreto_DeveRetornarTrue()
        {
            //arrange
            var comparer = _fixture.CreateComparerFromExpression<int>((t1, t2) => t1 == t2);
            var hashSet = new HashSet<int>(1) { 12 };

            //act & assert
            var exist = hashSet.Contains(12);
            Assert.True(exist);
            Assert.Contains(12, hashSet, comparer);
            Assert.True(hashSet.Remove(12));
        }

        [Fact(DisplayName = "Ao buscar registro, deve retornar false")]
        [Trait("HashSet", "Contains")]
        public void Contains_EqualityComparerErrado_DeveRetornarFalse()
        {
            //arrange
            var comparer = _fixture.CreateComparerFromExpression<int>((t1, t2) => t1 == (t2 + 2));
            var hashSet = new HashSet<int>(1, comparer) { 12 };

            //act & assert
            var exist = hashSet.Contains(12);
            Assert.False(exist);
            Assert.DoesNotContain(12, hashSet, comparer);
            Assert.False(hashSet.Remove(12));
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

        #region Constructor
        [Fact(DisplayName = "Ao instanciar com capacidade negativa, deve retornar exception")]
        [Trait("HashSet", "Constructor")]
        public void Instanciar_HashSetComCapacidadeNegativa_DeveRetornarException()
        {
            //arrange
            var comparer = _fixture.CreateComparerFromExpression<string>((t1, t2) => t1 == t2);

            //act & assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new HashSet<string>(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => new HashSet<string>(-1, comparer));
        }

        [Fact(DisplayName = "Ao instanciar com capacidade zerada, deve retornar exception")]
        [Trait("HashSet", "Constructor")]
        public void Instanciar_HashSetComCapacidadeZerada_DeveRetornarException()
        {
            //arrange
            var comparer = _fixture.CreateComparerFromExpression<string>((t1, t2) => t1 == t2);

            //act & assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new HashSet<string>(0));
            Assert.Throws<ArgumentOutOfRangeException>(() => new HashSet<string>(0, comparer));
        }

        [Fact(DisplayName = "Ao instanciar com capacidade positiva, não deve retornar exception")]
        [Trait("HashSet", "Constructor")]
        public void Instanciar_HashSetComCapacidadePositiva_DeveRetornarException()
        {
            //arrange
            var comparer = _fixture.CreateComparerFromExpression<string>((t1, t2) => t1 == t2);

            //act & assert
            _ = new HashSet<string>(10);
            _ = new HashSet<string>(10, comparer);
        }
        #endregion
    }
}
