using System;
using System.Collections.Generic;
using System.Linq;
using Lix.Commons.Repositories;
using Lix.Commons.Specifications;
using Lix.Commons.Tests.Examples;

namespace Lix.Commons.Tests.HelperExtensions
{
    public class RepositoryTestHelpers
    {
        public static void TestRepositoryMethodInterceptsTheSpecification<TRepository, TEntity, TSpecificationToIntercept>(TRepository repository, Action<TRepository, TSpecificationToIntercept> action)
            where TRepository : IRepository<TEntity>
            where TEntity : class
            where TSpecificationToIntercept : ISpecification, new()
        {
            LixObjectFactory.Initialize(x => x.UseSpecificationInterceptor<FakeSpecificationInterceptorToEnsureInterceptionCalled<TEntity>>());

            var interceptWith = new Func<IQueryable<TEntity>>(() => new List<TEntity>().AsQueryable());
            var interceptor = Specification.Intercept<TSpecificationToIntercept>();
            interceptor.With(interceptWith);

            action(repository, new TSpecificationToIntercept());

            (interceptor as FakeSpecificationInterceptorToEnsureInterceptionCalled<TEntity>).WasIntercepted.ShouldBeEqualTo(true);
        }
    }
}