using System.Linq;
using Lix.Commons.Tests;
using Lix.Commons.Tests.Examples;
using Lix.Futures.Tests.Examples;
using Lix.NHibernate.Utilities.Tests.Repositories;
using Lix.StructureMapAdapter;
using MbUnit.Framework;

namespace Lix.Futures.Tests.NHibernate
{
    [TestFixture]
    public class when_performing_a_like_query : nhibernate_test_setups
    {
        private FutureFishNHibernateRepository fishRepository;

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

            var container = new StructureMap.Container();
            container.Configure(x => x.Scan(cfg =>
                                                {
                                                    cfg.TheCallingAssembly();
                                                    cfg.AssemblyContainingType<Fish>();
                                                    cfg.With(new QueryableSpecificationExecutorRegistrationConvention());
                                                    cfg.WithDefaultConventions();
                                                }));
            this.fishRepository = new FutureFishNHibernateRepository(this.UnitOfWork, new StructureMapSpecificationExecutorFactory(container));

        }

        [Test]
        public void should_find_results_including_search_term_within_text()
        {
            var result = this.fishRepository.List(new FindFishContainingDescription("time"));

            result.Count().ShouldBeEqualTo(3);
        }

        [Test]
        public void should_find_results_ending_with_search_term()
        {
            var result = this.fishRepository.List(new FindFishEndingWithDescription("nod."));

            result.Count().ShouldBeEqualTo(1);
        }

        [Test]
        public void should_find_results_beginning_with_search_term()
        {
            var result = this.fishRepository.List(new FindFishStartingWithDescription("There"));

            result.Count().ShouldBeEqualTo(2);
        }
    }
}