using System.Collections.Generic;
using System.Linq;
using Lix.Commons.Repositories.InMemory;
using Lix.Commons.Tests.Examples;
using MbUnit.Framework;

namespace Lix.Commons.Tests.Repositories.InMemory
{
    [TestFixture]
    public class when_using_an_in_memory_unit_of_work : when_using_a_unit_of_work<InMemoryUnitOfWork>
    {
        private InMemoryDataStore mainDataStore;
        private InMemoryUnitOfWork unitOfWork;

        [SetUp(Order = 0)]
        public virtual void SetUp()
        {
            this.mainDataStore = new InMemoryDataStore();
            this.unitOfWork = new InMemoryUnitOfWork(this.mainDataStore);
        }

        protected override InMemoryUnitOfWork GetUnitOfWork()
        {
            return this.unitOfWork;
        }

        protected override void Save(Fish entity)
        {
            this.unitOfWork.Save(entity);
        }

        protected override IEnumerable<Fish> List()
        {
            return mainDataStore.List<Fish>();
        }

        [Test]
        public void should_not_return_duplicate_instances_of_a_type_when_listing()
        {
            using (this.unitOfWork)
            {
                this.unitOfWork.Begin();

                var fishA = new Fish {Id = 2};
                var fishB = new Fish {Id = 5};
                this.unitOfWork.Save(fishA);
                this.unitOfWork.Save(fishB);
                this.unitOfWork.Commit(true);

                fishA.Id = 1;
                this.unitOfWork.Save(fishA);

                this.unitOfWork.CurrentTransactionDataStore.List<Fish>().Count().ShouldBeEqualTo(2);
            }
        }
    }
}