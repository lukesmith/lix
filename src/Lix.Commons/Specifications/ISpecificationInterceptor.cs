using System;
using System.Linq;

namespace Lix.Commons.Specifications
{
    public interface ISpecificationInterceptor
    {
        void With(ISpecification specification);
        
        void With<TEntity>(Func<IQueryable<TEntity>> func)
            where TEntity : class;

        ISpecification InterceptedBy();
    }
}