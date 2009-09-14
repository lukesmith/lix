using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Lix.Commons.Specifications;
using Lix.Commons.Specifications.NHibernate;

namespace Lix.Commons.Repositories
{
    public class SpecificationExecutionEngine
    {
        private static IDictionary<Type, Type> specificationExecutors = new Dictionary<Type, Type>();
        private IDictionary<Type, Func<object>> contexts = new Dictionary<Type, Func<object>>();

        static SpecificationExecutionEngine()
        {
            specificationExecutors.Add(typeof(IQueryableSpecification<>), typeof(QueryableSpecificationExecutor<>));
            specificationExecutors.Add(typeof(INHibernateCriteriaSpecification), typeof(NHibernateCriteriaSpecificationExecutor<>));
        }

        public void RegisterContext<TContext>(Func<object> func)
        {
            this.contexts.Add(typeof(TContext), func);
        }

        public ISpecificationExecutor<TEntity> GetExecutor<TSpecification, TEntity>(TSpecification specification)
            where TSpecification : ISpecification
            where TEntity : class
        {
            return this.GetExecutor<TSpecification, TEntity>(specification, false);
        }

        public ISpecificationExecutor<TEntity> GetExecutor<TSpecification, TEntity>(TSpecification specification, bool intercept)
            where TSpecification : ISpecification
            where TEntity : class
        {
            ISpecification interceptedSpecification = specification;

            if (intercept)
            {
                interceptedSpecification = Specification.Interceptors.GetReplacement(specification);
            }

            var foundExecutorType = FindExecutor(interceptedSpecification);

            if (foundExecutorType == null)
            {
                throw new InvalidOperationException(string.Format("Executor for type {0} not found.", specification.GetType().FullName));
            }

            var executor = this.CreateExecutor<ISpecification, TEntity>(foundExecutorType, interceptedSpecification);

            return executor as ISpecificationExecutor<TEntity>;
        }

        private object CreateExecutor<TSpecification, TEntity>(Type executorType, TSpecification specification)
            where TSpecification : ISpecification
            where TEntity : class
        {
            var context = this.FindContextForExecutor(executorType);

            if (context == null)
            {
                throw new InvalidOperationException(string.Format("A context could not be found for the executor {0}", executorType));
            }
            
            if (executorType.ContainsGenericParameters)
            {
                var genericType = executorType.MakeGenericType(typeof(TEntity));

                return Activator.CreateInstance(genericType, specification, context);
            }

            return Activator.CreateInstance(executorType, specification, context);
        }

        private static Type FindExecutor(ISpecification specification)
        {
            var interfaces = specification.GetType().GetInterfaces();
            Type foundExecutor = null;

            foreach (var executorType in specificationExecutors)
            {
                var implementorType = executorType.Key.GetType();

                foreach (var @interface in interfaces)
                {
                    if (implementorType == @interface.GetType())
                    {
                        foundExecutor = executorType.Value;
                        break;
                    }
                }

                if (foundExecutor != null)
                {
                    break;
                }
            }

            return foundExecutor;
        }

        private object FindContextForExecutor(Type executorType)
        {
            var executorsConstructorParameters = executorType.GetConstructors()[0].GetParameters();
            
            object context = null;
            foreach (var contextA in contexts)
            {
                var contextValue = contextA.Value();
                var contextsInterfaces = contextValue.GetType().GetInterfaces();

                var contextParemeter = executorsConstructorParameters[1];

                foreach (var type in contextsInterfaces)
                {
                    if (contextParemeter.ParameterType.GetGenericTypeDefinition() == type.GetGenericTypeDefinition())
                    {
                        context = contextValue;
                        break;
                    }
                }

                if (context != null)
                {
                    break;
                }
            }

            return context;
        }
    }
}
