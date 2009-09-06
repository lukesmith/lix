using System;
using System.Linq;

namespace Lix.Commons.Specifications
{
    public interface ISpecificationInterceptor
    {
        void With(ISpecification specification);
        
        void With(Func<IQueryable> func);

        ISpecification InterceptedBy();
    }
}