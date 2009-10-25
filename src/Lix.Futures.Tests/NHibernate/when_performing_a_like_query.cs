using System.Linq;
using Lix.Commons.Tests;
using Lix.Commons.Tests.Examples;
using Lix.Futures.Tests.Examples;
using Lix.NHibernate.Utilities.Tests.Repositories;
using Lix.NHibernate.Utilities.Tests.Repositories.Examples;
using MbUnit.Framework;

namespace Lix.Futures.Tests.NHibernate
{
    [TestFixture]
    public class when_performing_a_like_query : nhibernate_test_setups
    {
        private FishNHibernateRepository fishRepository;

        protected override void PerformSetUp()
        {
            this.UnitOfWork.Session.Save(new Fish
                                     {
                                         Description = "A fish called wanda"
                                     });
            this.UnitOfWork.Session.Save(new Fish
                                     {
                                         Description = "Once up a time in a land called nod."
                                     });
            this.UnitOfWork.Session.Save(new Fish
                                     {
                                         Description = "There was a giant timelord."
                                     });
            this.UnitOfWork.Session.Save(new Fish
                                     {
                                         Description = "There was big and good travel thru time."
                                     });

            this.UnitOfWork.Commit(true);

            this.fishRepository = new FishNHibernateRepository(this.UnitOfWork);
        }

        [Test]
        public void should_find_results_including_search_term_within_text()
        {
            var result = this.fishRepository.List(new FindFishDescriptionContainingSpecification("time"));

            result.Count().ShouldBeEqualTo(3);
        }

        [Test]
        public void should_find_results_ending_with_search_term()
        {
            var result = this.fishRepository.List(new FindFishDescriptionEndingWithSpecification("nod."));

            result.Count().ShouldBeEqualTo(1);
        }

        [Test]
        public void should_find_results_beginning_with_search_term()
        {
            var result = this.fishRepository.List(new FindFishDescriptionStartsWithSpecification("There"));

            result.Count().ShouldBeEqualTo(2);
        }
    }
}