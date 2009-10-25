using System.Collections.Generic;
using System.Linq;
using Lix.Commons.Specifications;
using Lix.Commons.Tests.Examples;
using MbUnit.Framework;

namespace Lix.Commons.Tests.SpecificationExecutors
{
    [TestFixture]
    public abstract class using_a_specification_executor<TExecutor, TSpecification>
        where TExecutor : ISpecificationExecutor<Fish>
        where TSpecification : ISpecification
    {
        protected IEnumerable<Fish> CreateDefaults()
        {
            return new List<Fish>()
                       {
                           new Fish { Description = "Slippery Fish" },
                           new Fish { Description = "Wet Fish" },
                           new Fish { Description = "A fish called wanda" },
                           new Fish { Description = "A smelly fish" }
                       };
        }

        [SetUp(Order = 0)]
        public void SetUp()
        {
            this.PerformSetUp();
        }

        [TearDown(Order = 0)]
        public void TearDown()
        {
            this.PerformTearDown();
        }

        public virtual void PerformTearDown()
        {
        }

        public virtual void PerformSetUp()
        {
        }

        protected abstract TExecutor GetExecutor(TSpecification specification);
        protected abstract TSpecification GetSpecificationForMultipleUniqueResult();
        protected abstract TSpecification GetSpecificationForUniqueResult(string description);

        [Test]
        public virtual void should_list_all_the_entities()
        {
            var executor = this.GetExecutor(this.GetSpecificationForMultipleUniqueResult());

            executor.List().Count().ShouldBeEqualTo(4);
        }

        [Test]
        public virtual void should_get_the_entity_matching_the_specification()
        {
            var executor = this.GetExecutor(this.GetSpecificationForUniqueResult("Slippery Fish")).Get();

            executor.Description.ShouldBeEqualTo("Slippery Fish");
        }

        [Test]
        public virtual void should_list_the_entities_starting_not_including_the_first()
        {
            var result = this.GetExecutor(this.GetSpecificationForMultipleUniqueResult()).List(1, 10);

            result.Any(f => f.Description == "Slippery Fish").ShouldBeEqualTo(false);
        }

        [Test]
        public virtual void should_list_a_sub_selection_of_entities()
        {
            var result = this.GetExecutor(this.GetSpecificationForMultipleUniqueResult()).List(1, 2);

            result.Last().Description.ShouldBeEqualTo("A fish called wanda");
        }

        [Test]
        public virtual void should_count_the_datasource_given_the_specification()
        {
            var result = this.GetExecutor(this.GetSpecificationForMultipleUniqueResult()).Count();

            result.ShouldBeEqualTo(4);
        }

        [Test]
        public virtual void should_return_whether_items_exist_given_the_specification()
        {
            var result = this.GetExecutor(this.GetSpecificationForMultipleUniqueResult()).Exists();

            result.ShouldBeEqualTo(true);
        }
    }
}