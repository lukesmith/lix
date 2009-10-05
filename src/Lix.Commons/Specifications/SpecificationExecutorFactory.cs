using System;
using System.Collections.Generic;
using System.Linq;

namespace Lix.Commons.Specifications
{
    public class SpecificationExecutorFactory
    {
        private readonly IDictionary<Type, Func<object>> contexts = new Dictionary<Type, Func<object>>();

        public void RegisterContext<TContext>(Func<object> func)
        {
            this.contexts.Add(typeof(TContext), func);
        }

        /// <summary>
        /// Gets the <see cref="ISpecificationExecutor{TEntity}"/> used to execute the <paramref name="specification"/>.
        /// </summary>
        /// <param name="specification">The <see cref="ISpecification"/> to find the <see cref="ISpecificationExecutor{TEntity}"/> for.</param>
        /// <typeparam name="TSpecification">The generic type of the <see cref="ISpecification"/>.</typeparam>
        /// <typeparam name="TEntity">The type of the entity the <see cref="ISpecificationExecutor{TEntity}"/> has.</typeparam>
        /// <returns>
        /// An instance of the <see cref="ISpecificationExecutor{TEntity}"/>.
        /// </returns>
        public ISpecificationExecutor<TEntity> GetExecutor<TSpecification, TEntity>(TSpecification specification)
            where TSpecification : ISpecification
            where TEntity : class
        {
            return this.GetExecutor<TSpecification, TEntity>(specification, false);
        }

        /// <summary>
        /// Gets the <see cref="ISpecificationExecutor{TEntity}"/> used to execute the <paramref name="specification"/>.
        /// </summary>
        /// <param name="specification">The <see cref="ISpecification"/> to find the <see cref="ISpecificationExecutor{TEntity}"/> for.</param>
        /// <param name="intercept">A boolean value indicating whether the <paramref name="specification"/> should be intercepted before the executor is found.</param>
        /// <typeparam name="TSpecification">The generic type of the <see cref="ISpecification"/>.</typeparam>
        /// <typeparam name="TEntity">The type of the entity the <see cref="ISpecificationExecutor{TEntity}"/> has.</typeparam>
        /// <returns>
        /// An instance of the <see cref="ISpecificationExecutor{TEntity}"/>.
        /// </returns>
        ///<exception cref="InvalidOperationException"></exception>
        public ISpecificationExecutor<TEntity> GetExecutor<TSpecification, TEntity>(TSpecification specification, bool intercept)
            where TSpecification : ISpecification
            where TEntity : class
        {
            ISpecification interceptedSpecification = specification;

            if (intercept)
            {
                interceptedSpecification = Specification.Interceptors.GetReplacement(specification);
            }

            var foundExecutorType = FindExecutor<TEntity>(interceptedSpecification);

            if (foundExecutorType == null)
            {
                throw new InvalidOperationException(string.Format("Executor for type {0} not found.", specification.GetType().FullName));
            }

            return this.CreateExecutor(foundExecutorType, interceptedSpecification) as ISpecificationExecutor<TEntity>;
        }

        private object CreateExecutor<TSpecification>(Type executorTypeToCreate, TSpecification specification)
            where TSpecification : ISpecification
        {
            var context = this.FindContextForExecutor(executorTypeToCreate);

            if (context == null)
            {
                throw new InvalidOperationException(string.Format("A context could not be found for the executor {0}", executorTypeToCreate));
            }

            return Activator.CreateInstance(executorTypeToCreate, specification, context);
        }

        private static Type FindExecutor<TEntity>(ISpecification specification)
        {
            var specificationInterfaces = specification.GetType().GetInterfaces();

            var executor = LixObjectFactory.Container.FindTypeFor(containerType => specificationInterfaces.Any(
                                                                   specificationInterface =>
                                                                   LixObjectFactory.DoTypesMatch(specificationInterface, containerType)));

            return executor.ContainsGenericParameters ? executor.MakeGenericType(typeof(TEntity)) : executor;
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

                if (contextsInterfaces.Any(x => LixObjectFactory.DoTypesMatch(x, contextParemeter.ParameterType)))
                {
                    result = contextValue;
                    break;
                }
            }

            return result;
        }
    }
}