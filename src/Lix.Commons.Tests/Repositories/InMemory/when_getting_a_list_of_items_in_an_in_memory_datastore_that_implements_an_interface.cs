using System;
using System.Collections.Generic;
using System.Linq;
using Lix.Commons.Repositories;
using Machine.Specifications;

namespace Lix.Commons.Tests.Repositories.InMemory
{
    public class when_getting_a_list_of_items_in_an_in_memory_datastore_that_implements_an_interface
    {
        private static InMemoryDataStore _dataStore;
        private static IEnumerable<IComparable> _listResult;

        private Establish context = () =>
                                        {
                                            _dataStore = new InMemoryDataStore();
                                            _dataStore.Save(new ComparableObject());
                                            _dataStore.Save(new ComparableObject());
                                            _dataStore.Save(new ComparableObject());
                                            _dataStore.Save(new ComparableObject());
                                        };

        private Because of = () => _listResult = _dataStore.List<IComparable>();

        private It should_contain_three_items = () => _listResult.Count().ShouldBeEqualTo(4);

        private class ComparableObject : IComparable
        {
            public int CompareTo(object obj)
            {
                return 0;
            }
        }
    }
}
