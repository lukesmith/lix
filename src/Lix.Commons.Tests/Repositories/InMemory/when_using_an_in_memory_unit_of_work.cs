using System.Collections.Generic;
using System.Linq;
using Lix.Commons.Repositories;
using Lix.Commons.Tests.Examples;
using MbUnit.Framework;

namespace Lix.Commons.Tests.Repositories.InMemory
{
    [TestFixture]
    public class when_using_an_in_memory_unit_of_work : when_using_a_unit_of_work<InMemoryUnitOfWork, Fish>
    {
        private InMemoryDataStore mainDataStore;

        [SetUp(Order = 0)]
        public virtual void SetUp()
        {
            this.mainDataStore = new InMemoryDataStore();
        }

        protected override void SaveToUnitOfWork(InMemoryUnitOfWork unitOfWork, Fish entity)
        {
            unitOfWork.CurrentTransactionDataStore.Save(entity);
        }

        protected override InMemoryUnitOfWork CreateUnitOfWork()
        {
            return new InMemoryUnitOfWork(this.mainDataStore);
        }

        protected override IEnumerable<Fish> List()
        {
            return mainDataStore.List<Fish>();
        }

        [Test]
        public void should_not_return_duplicate_instances_of_a_type_when_listing()
        {
            using (var unitOfWork = this.CreateUnitOfWork())
            {
                unitOfWork.Begin();

                var fishA = new Fish {Id = 2};
                var fishB = new Fish {Id = 5};
                unitOfWork.CurrentTransactionDataStore.Save(fishA);
                unitOfWork.CurrentTransactionDataStore.Save(fishB);
                unitOfWork.Commit(true);

                fishA.Id = 1;
                unitOfWork.CurrentTransactionDataStore.Save(fishA);

                unitOfWork.CurrentTransactionDataStore.List<Fish>().Count().ShouldBeEqualTo(2);
            }
        }

        [Test]
        public void should_not_save_entities_to_the_main_data_source_before_committing()
        {
            using (var unitOfWork = this.CreateUnitOfWork())
            {
                unitOfWork.Begin();

                var fishA = new Fish { Id = 2 };
                this.SaveToUnitOfWork(unitOfWork, fishA);

                this.mainDataStore.List<Fish>().Count().ShouldBeEqualTo(0);
            }
        }

        [Test]
        public void should_save_entities_to_the_main_data_source_after_committing()
        {
            using (var unitOfWork = this.CreateUnitOfWork())
            {
                unitOfWork.Begin();

                var fishA = new Fish { Id = 2 };
                this.SaveToUnitOfWork(unitOfWork, fishA);
                unitOfWork.Commit();

                this.mainDataStore.List<Fish>().Count().ShouldBeEqualTo(1);
            }
        }
    }
}