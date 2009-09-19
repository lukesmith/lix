using Lix.Commons.Repositories.InMemory;

namespace Lix.Commons.Tests.HelperExtensions
{
    public static class InMemoryDataStoreExtensions
    {
        public static void SaveEntities<TEntity>(this InMemoryDataStore dataStore, int numberToSave)
            where TEntity : new()
        {
            for (var i = 0; i < numberToSave; i++)
            {
                dataStore.Save(new TEntity());
            }
        }
    }
}