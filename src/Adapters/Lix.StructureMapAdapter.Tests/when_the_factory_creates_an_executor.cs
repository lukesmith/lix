using System;
using System.Collections.Generic;
using System.Linq;
using Lix.Commons;
using Lix.Commons.Repositories;
using Lix.Commons.Specifications;
using Machine.Specifications;
using Moq;
using Container=StructureMap.Container;
using It=Machine.Specifications.It;

namespace Lix.StructureMapAdapter.Tests
{
    public class when_the_factory_creates_an_executor
    {
        private static ISpecificationExecutorFactory _specificationExecutorFactory;
        private static ISpecificationExecutor<StubEntity> _executorInstance;
        private static StubSpecification _specification;
        private static Mock<IQueryRepository<StubEntity>> _repository;

        private Establish context = () =>
        {
            var container = new Container();
            container.Configure(x => x.For<ISpecificationExecutor<IQueryableSpecification<StubEntity>, StubEntity>>().Use<StubSpecificationExecutor<StubEntity>>());
            _specificationExecutorFactory = new StructureMapSpecificationExecutorFactory(container);
            _specification = new StubSpecification();

            _repository = new Mock<IQueryRepository<StubEntity>>();
            _repository.Setup(x => x.RepositoryQuery).Returns(new List<StubEntity>().AsQueryable());
        };

        private Because of = () => _executorInstance = _specificationExecutorFactory.CreateExecutor<StubSpecification, StubEntity, IQueryRepository<StubEntity>>(_specification, _repository.Object);

        private It should_not_be_null = () => _executorInstance.ShouldNotBeNull();

        private class StubSpecification : IQueryableSpecification<StubEntity>
        {
            public IQueryable<StubEntity> Build(IQueryable<StubEntity> context)
            {
                throw new NotImplementedException();
            }

            public bool IsSatisfiedBy(StubEntity entity)
            {
                throw new NotImplementedException();
            }

            public object Build(object context)
            {
                throw new NotImplementedException();
            }
        }

        public class StubEntity
        {
            public StubEntity()
            {
            }
        }

        private class StubSpecificationExecutor<TEntity> : ISpecificationExecutor<IQueryableSpecification<StubEntity>, TEntity>
            where TEntity : class
        {
            public StubSpecificationExecutor(IQueryableSpecification<StubEntity> specification, ILinqRepository<TEntity> repository)
            {
            }

            public IQueryableSpecification<StubEntity> Specification
            {
                get { throw new NotImplementedException(); }
            }

            public TEntity Get()
            {
                throw new NotImplementedException();
            }

            public IEnumerable<TEntity> List()
            {
                throw new NotImplementedException();
            }

            public PagedResult<TEntity> List(int startIndex, int pageSize)
            {
                throw new NotImplementedException();
            }

            public long Count()
            {
                throw new NotImplementedException();
            }

            public bool Exists()
            {
                throw new NotImplementedException();
            }
        }
    }
}
