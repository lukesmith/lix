using System;
using Lix.Commons.Repositories;
using Lix.Commons.Tests.Repositories.Linq2Sql.Examples;

namespace Lix.Commons.Tests.Repositories
{
    public static class IUnitOfWorkExtensions
    {
        [Obsolete]
        public static void Save2(this IUnitOfWork unitOfWork, object entity)
        {
            if (unitOfWork is InMemoryUnitOfWork)
            {
                var imUnitOfWork = unitOfWork as InMemoryUnitOfWork;
                
                imUnitOfWork.CurrentTransactionDataStore.Save(entity);
            }
            else if (unitOfWork is Linq2SqlUnitOfWork)
            {
                var l2sUnitOfWork = unitOfWork as Linq2SqlUnitOfWork;

                (l2sUnitOfWork.DataContext as FoodDataClassesDataContext).Foods.InsertOnSubmit(entity as Food);
            }
            else
            {
                // TODO: Add message to exception
                throw new InvalidOperationException();
            }
        }
    }
}
