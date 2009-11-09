using System;
using Lix.Commons.Specifications;

namespace Lix.Commons
{
    public interface IInitializeExpression
    {
        void UseSpecificationInterceptor(Type specificationInterceptor);

        void UseSpecificationInterceptor<TInterceptor>()
            where TInterceptor : ISpecificationInterceptor;

        void UseDefaults();
    }
}