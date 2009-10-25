using System;
using Lix.Commons.Specifications;

namespace Lix.Commons
{
    public class InitializeExpression : IInitializeExpression
    {
        private readonly Container container;

        public InitializeExpression(Container container)
        {
            this.container = container;
        }

        public void UseSpecificationInterceptor(Type specificationInterceptor)
        {
            this.container.Register(specificationInterceptor);
        }

        public void UseSpecificationInterceptor<TInterceptor>()
            where TInterceptor : ISpecificationInterceptor
        {
            this.container.Register(typeof(TInterceptor));
        }

        public void UseDefaults()
        {
            this.UseSpecificationInterceptor<DefaultSpecificationInterceptor>();
            this.RegisterSpecificationExecutor(typeof(IQueryableSpecification<>), typeof(DefaultQueryableSpecificationExecutor<>));
        }
    }
}