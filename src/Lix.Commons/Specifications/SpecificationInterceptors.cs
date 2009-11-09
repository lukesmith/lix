using System;
using System.Collections.Generic;

namespace Lix.Commons.Specifications
{
    public class SpecificationInterceptors
    {
        private IDictionary<Type, ISpecificationInterceptor> interceptors;
        private IDictionary<Type, ISpecificationInterceptor> Interceptors
        {
            get
            {
                if (this.interceptors == null)
                {
                    this.interceptors = new Dictionary<Type, ISpecificationInterceptor>();
                }

                return this.interceptors;
            }
        }

        public ISpecificationInterceptor this[Type specificationType]
        {
            get
            {
                return this.Interceptors[specificationType];
            }
        }

        public void Add<TSpecification>(ISpecificationInterceptor interceptor)
            where  TSpecification : ISpecification
        {
            this.Interceptors.Add(typeof(TSpecification), interceptor);
        }

        public void Clear()
        {
            this.Interceptors.Clear();
        }

        public bool HasReplacement(ISpecification specification)
        {
            return this.Interceptors.ContainsKey(specification.GetType());
        }

        public ISpecification GetReplacement(ISpecification specification)
        {
            if (this.HasReplacement(specification))
            {
                var interceptor = this.Interceptors[specification.GetType()];

                if (interceptor != null)
                {
                    return interceptor.InterceptedBy();
                }
            }

            return specification;
        }
    }
}