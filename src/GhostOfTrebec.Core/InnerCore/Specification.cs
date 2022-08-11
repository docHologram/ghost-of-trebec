using System;
using System.Linq.Expressions;

namespace GhostOfTrebec.Core.InnerCore
{
    public abstract class Specification<T>
    {
        public abstract Expression<Func<T, bool>> ToExpression();
    }
}
