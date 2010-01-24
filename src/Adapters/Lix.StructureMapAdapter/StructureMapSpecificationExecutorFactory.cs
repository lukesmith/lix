﻿using System;
using System.Collections.Generic;
using System.Linq;
using Lix.Commons.Repositories;
using Lix.Commons.Specifications;
using StructureMap;
using StructureMap.Pipeline;

namespace Lix.StructureMapAdapter
{
    public static class ConfigurationExpressionExtensions
    {
        public static void RegisterLixDefaults(this ConfigurationExpression configurationExpression)
        {
            configurationExpression.For(typeof (IQueryableSpecification<>)).Use(typeof (DefaultQueryableSpecification<>));
        }
    }

    public class StructureMapSpecificationExecutorFactory : ISpecificationExecutorFactory
    {
        private static readonly Type ClosedSpecificationInterfaceType = typeof(ISpecification);
        private static readonly Type OpenSpecificationExecutorInterfaceType = typeof(ISpecificationExecutor<,>);
        private readonly IContainer container;

        public StructureMapSpecificationExecutorFactory(IContainer container)
        {
            this.container = container;
        }

        public ISpecificationExecutor<TEntity> CreateExecutor<TSpecification, TEntity, TRepository>(TSpecification specification, TRepository repository)
            where TSpecification : ISpecification
            where TRepository : IQueryRepository<TEntity>
            where TEntity : class
        {
            var specificationsInterfaces = specification.GetType().GetInterfaces();
            var specificationInterface = specificationsInterfaces.First(IsSpecificationExecutorInterface<TEntity>);

            var openSpecificationExecutorType = OpenSpecificationExecutorInterfaceType.MakeGenericType(specificationInterface, typeof(TEntity));
            var arguments = new ExplicitArguments(new Dictionary<string, object> {{"specification", specification}, {"repository", repository}});

            return (ISpecificationExecutor<TEntity>)this.container.GetInstance(openSpecificationExecutorType, arguments);
        }

        /// <summary>
        /// Determines whether the type is an <see cref="ISpecification"/> that has a single generic argument accepting <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type the specification must accept.</typeparam>
        /// <param name="type">The type to determine whether it is a <see cref="ISpecification"/>.</param>
        /// <returns>[true] if correct, [false] if otherwise.</returns>
        private static bool IsSpecificationExecutorInterface<TEntity>(Type type)
            where TEntity : class
        {
            if (ClosedSpecificationInterfaceType.IsAssignableFrom(type) && type != ClosedSpecificationInterfaceType)
            {
                var genericArguments = type.GetGenericArguments();
                return genericArguments.Length == 1 && genericArguments.Contains(typeof(TEntity));
            }

            return false;
        }
    }
}
