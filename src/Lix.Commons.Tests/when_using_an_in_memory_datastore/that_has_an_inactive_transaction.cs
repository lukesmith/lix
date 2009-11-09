using System.Linq;
using Lix.Commons.Repositories;
using Lix.Commons.Tests.Examples;
using MbUnit.Framework;

namespace Lix.Commons.Tests.when_using_an_in_memory_datastore
{
    [TestFixture]
    public class that_has_an_inactive_transaction
    {
        private InMemoryDataStore dataStore;

        [SetUp]
        public void SetUp()
        {
            this.dataStore = new InMemoryDataStore();
        }

        [TearDown]
        public void TearDown()
        {
            this.dataStore = null;
        }

        [Test]
        public void should_save_to_the_data_store()
        {
            var entity = new Fish { Description = "slippery fish" };
            this.dataStore.Save(entity);

            this.dataStore.Contains(entity).ShouldBeEqualTo(true);
        }

        [Test]
        public void should_remove_from_the_data_store()
        {
            var entity = new Fish { Description = "slippery fish" };
            this.dataStore.Save(entity);

            this.dataStore.Remove(entity);
            this.dataStore.Contains(entity).ShouldBeEqualTo(false);
        }

        [Test]
        public void should_use_the_data_store_when_looking_whether_an_entity_is_contained()
        {
            var entity = new Fish {Description = "slippery fish"};
            this.dataStore.Save(entity);

            this.dataStore.Contains(entity).ShouldBeEqualTo(true);
        }

        [Test]
        public void should_clear_the_data_store()
        {
            var entity = new Fish { Description = "slippery fish" };
            this.dataStore.Save(entity);

            this.dataStore.Clear();

            this.dataStore.Count().ShouldBeEqualTo(0);
        }

        [Test]
        public void should_list_data_store_entities()
        {
            var entity = new Fish { Description = "slippery fish" };
            this.dataStore.Save(entity);

            this.dataStore.List<Fish>().Any().ShouldBeEqualTo(true);
        }

        [Test]
        public void should_enumerate_the_data_store()
        {
            var entity = new Fish { Description = "slippery fish" };
            this.dataStore.Save(entity);

            this.dataStore.GetEnumerator().ShouldBeTheSameObjectAs(
                this.dataStore.GetEnumerator());
        }
    }
}