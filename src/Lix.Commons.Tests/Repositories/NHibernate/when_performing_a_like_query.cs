using System.Linq;
using Lix.Commons.Tests.Examples;
using Lix.Commons.Tests.Examples.Specifications;
using Lix.Commons.Tests.Repositories.NHibernate.Examples;
using MbUnit.Framework;

namespace Lix.Commons.Tests.Repositories.NHibernate
{
    [TestFixture]
    public class when_performing_a_like_query : nhibernate_test_setups
    {
        private FishNHibernateRepository fishRepository;

        public override void SetUp()
        {
            base.SetUp();

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

            fishRepository = new FishNHibernateRepository(this.UnitOfWork);
        }

        [Test]
        public void should_find_results_including_search_term_within_text()
        {
            var result = fishRepository.List(new FindFishDescriptionContainingSpecification("time"));

            result.Count().ShouldBeEqualTo(3);
        }

        [Test]
        public void should_find_results_ending_with_search_term()
        {
            var result = fishRepository.List(new FindFishDescriptionEndingWithSpecification("nod."));

            result.Count().ShouldBeEqualTo(1);
        }

        [Test]
        public void should_find_results_beginning_with_search_term()
        {
            var result = fishRepository.List(new FindFishDescriptionStartsWithSpecification("There"));

            result.Count().ShouldBeEqualTo(2);
        }
    }
}
