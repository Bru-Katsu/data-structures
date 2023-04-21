using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DataStructures.Tests.Fixtures.EqualityComparer
{
    public class EqualityComparerFixture
    {
        public IEqualityComparer<T> CreateComparerFromExpression<T>(Func<T, T, bool> expression) => new ExpressionComparer<T>(expression);
    }

    public class ExpressionComparer<T> : EqualityComparer<T>
    {
        private readonly Func<T, T, bool> _expression;
        public ExpressionComparer(Func<T, T, bool> expression)
        {
            _expression = expression;
        }

        public override bool Equals(T? x, T? y)
        {
            return _expression(x, y);
        }

        public override int GetHashCode([DisallowNull] T obj)
        {
            return obj.GetHashCode();
        }
    }
}
