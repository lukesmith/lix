using Lix.Commons.Repositories.InMemory;
using Lix.Commons.Tests.Examples;
using MbUnit.Framework;

namespace Lix.Commons.Tests.Repositories.InMemory
{
    [TestFixture]
    public class when_checking_whether_an_in_memory_datastore_contains_an_entity
    {
        [Test]
        public void should_find_the_entity()
        {
            var datastore = new InMemoryDataStore();

            var fish = new Fish();
            datastore.Save(fish);

            datastore.Contains(fish).ShouldBeEqualTo(true);
        }

        [Test]
        public void should_find_the_entity_using_a_custom_equality_comparer()
        {
            var datastore = new InMemoryDataStore();

            var fishA = new Fish { Id = 2 };
            datastore.Save(fishA);
            var fishB = new Fish { Id = 55 };
            datastore.Save(fishB);

            var fishToTest = new Fish { Id = 2 };

            datastore.Contains(fishToTest, new FishIdEqualityComparer()).ShouldBeEqualTo(true);
        }
    }
}
