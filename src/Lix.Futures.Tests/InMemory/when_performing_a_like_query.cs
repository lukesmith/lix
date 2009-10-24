using System.Linq;
using Lix.Commons.Repositories.InMemory;
using Lix.Commons.Tests.Examples;
using Lix.Commons.Tests.Repositories.InMemory.Examples;
using Lix.Futures.Tests.Examples;
using MbUnit.Framework;

namespace Lix.Commons.Tests.Repositories.InMemory
{
    [TestFixture]
    public class when_performing_a_like_query : repository_test_setups<InMemoryUnitOfWork, FishInMemoryRepository, Fish>
    {
        protected override InMemoryUnitOfWork CreateUnitOfWork()
        {
            var dataStore = new InMemoryDataStore();
            return new InMemoryUnitOfWork(dataStore);
        }

        protected override FishInMemoryRepository CreateRepository()
        {
            return new FishInMemoryRepository(this.UnitOfWork);
        }

        public override void SetUp()
        {
            base.SetUp();

            this.UnitOfWork.Begin();

            this.UnitOfWork.Save(new Fish
                                {
                                    Description = "A fish called wanda"
                                });

            this.UnitOfWork.Save(new Fish
                                {
                                    Description = "Once up a time in a land called nod."
                                });

            this.UnitOfWork.Save(new Fish
                                {
                                    Description = "There was a giant timelord."
                                });

            this.UnitOfWork.Save(new Fish
                                {
                                    Description = "There was big and good travel thru time."
                                });

            this.UnitOfWork.Commit(true);
        }

        [Test]
        public void should_find_results_including_search_term_within_text()
        {
            var result = this.Repository.List(new FindFishDescriptionContainingSpecification("time"));

            result.Count().ShouldBeEqualTo(3);
        }

        [Test]
        public void should_find_results_ending_with_search_term()
        {
            var result = this.Repository.List(new FindFishDescriptionEndingWithSpecification("nod."));

            result.Count().ShouldBeEqualTo(1);
        }

        [Test]
        public void should_find_results_beginning_with_search_term()
        {
            var result = this.Repository.List(new FindFishDescriptionStartsWithSpecification("There"));

            result.Count().ShouldBeEqualTo(2);
        }
    }
}