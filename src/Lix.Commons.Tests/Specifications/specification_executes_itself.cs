using System;
using System.Collections.Generic;
using Lix.Commons.Repositories;
using Lix.Commons.Specifications;
using Lix.Commons.Specifications.Executors;
using Lix.Commons.Tests.Examples;
using Machine.Specifications;

namespace Lix.Commons.Tests.Specifications
{
    public class specification_executes_itself
    {
        protected static IReportingRepository<Person> repository;
        protected static SpecificationStub specification;

        private Establish context = () =>
                                        {
                                            repository = new RepositoryStub();
                                            specification = new SpecificationStub();
                                        };

        public class SpecificationStub : ISpecification<IList<Person>, Person>, ISpecificationExecutor<Person>
        {
            public bool GetCalled;
            public bool ListCalled;
            public bool PagedListCalled;
            public bool CountCalled;
            public bool ExistsCalled;
            public bool SetRepositoryCalled;

            public Person Build(IList<Person> context)
            {
                throw new NotImplementedException();
            }

            public Person Get()
            {
                this.GetCalled = true;
                return null;
            }

            public IEnumerable<Person> List()
            {
                this.ListCalled = true;
                return null;
            }

            public PagedResult<Person> List(int startIndex, int pageSize)
            {
                this.PagedListCalled = true;
                return null;
            }

            public long Count()
            {
                this.CountCalled = true;
                return 0;
            }

            public bool Exists()
            {
                this.ExistsCalled = true;
                return false;
            }

            public void SetContext(object context)
            {
                throw new NotImplementedException();
            }

            Person ISpecification<IList<Person>, Person>.Build()
            {
                throw new NotImplementedException();
            }

            public void SetContext(IList<Person> context)
            {
                throw new NotImplementedException();
            }

            public object Build()
            {
                throw new NotImplementedException();
            }

            public void SetRepository(IReportingRepository<Person> repository)
            {
                this.SetRepositoryCalled = true;
            }
        }

        public class RepositoryStub : RepositoryBase<Person>
        {
            public override Person Add(Person entity)
            {
                throw new NotImplementedException();
            }

            public override void Remove(Person entity)
            {
                throw new NotImplementedException();
            }
        }
    }
}