using System;
using System.Collections.Generic;
using System.Linq;

namespace Lix.Commons.Specifications
{
    public class SpecificationExecutorFactory
    {
        private static readonly IDictionary<Type, Type> SpecificationExecutors = new Dictionary<Type, Type>();
        private readonly IDictionary<Type, Func<object>> contexts = new Dictionary<Type, Func<object>>();

        public static ISpecificationExecutorInitializer Initialize()
        {
            return new DefaultSpecificationExecutorInitializer();
        }

        public static void RegisterSpecificationExecutor(Type specificationType, Type specificationExecutorType)
        {
            if (!specificationType.GetInterfaces().Contains(typeof(ISpecification)))
            {
                throw new ArgumentException("specificationType");
            }

            if (!specificationExecutorType.GetInterfaces().Any(x => DoTypesMatch(x, typeof(ISpecificationExecutor<>))))
            {
                throw new ArgumentException("specificationExecutorType");
            }

            if (SpecificationExecutors.ContainsKey(specificationType))
            {
                SpecificationExecutors[specificationType] = specificationExecutorType;
            }
            else
            {
                SpecificationExecutors.Add(specificationType, specificationExecutorType);
            }
        }

        public static void ClearExecutors()
        {
            SpecificationExecutors.Clear();
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
            var specificationInterfaces = specification.GetType().GetInterfaces();
            Type result = null;

            foreach (var executorType in SpecificationExecutors)
            {
                var implementorType = executorType.Key;

                if (specificationInterfaces.Any(x => DoTypesMatch(x, implementorType)))
                {
                    result = executorType.Value;
                    break;
                }
            }

            return result;
        }

        private object FindContextForExecutor(Type executorType)
        {
            var executorsConstructorParameters = executorType.GetConstructors()[0].GetParameters();
            
            object result = null;
            foreach (var context in this.contexts)
            {
                var contextValue = context.Value();
                var contextsInterfaces = contextValue.GetType().GetInterfaces();

                var contextParemeter = executorsConstructorParameters.FirstOrDefault(x => x.Name == "context");

                if (contextParemeter == null)
                {
                    throw new InvalidOperationException("A constructor argument named 'context' could not be found.");
                }

                if (contextsInterfaces.Any(x => DoTypesMatch(x, contextParemeter.ParameterType)))
                {
                    result = contextValue;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Checks whether the two types match, including whether they are the same generic type.
        /// </summary>
        /// <param name="typeA">The first type.</param>
        /// <param name="typeB">The second type.</param>
        /// <returns>
        /// true if the types match; otherwise false.
        /// </returns>
        private static bool DoTypesMatch(Type typeA, Type typeB)
        {
            if (typeA == typeB)
            {
                return true;
            }

            if (typeA.IsGenericType && typeB.GetGenericTypeDefinition() == typeA.GetGenericTypeDefinition())
            {
                return true;
            }

            return false;
        }
    }
}