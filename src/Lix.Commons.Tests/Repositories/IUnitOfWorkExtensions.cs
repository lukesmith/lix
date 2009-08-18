using System;
using System.Collections.Generic;
using Lix.Commons.Repositories;
using Lix.Commons.Repositories.InMemory;
using Lix.Commons.Repositories.NHibernate;

namespace Lix.Commons.Tests.Repositories
{
    public static class IUnitOfWorkExtensions
    {
        public static void Save(this IUnitOfWork unitOfWork, object entity)
        {
            if (unitOfWork is NHibernateUnitOfWork)
            {
                var nhUnitOfWork = unitOfWork as NHibernateUnitOfWork;
                nhUnitOfWork.Session.Save(entity);
            }
            else if (unitOfWork is InMemoryUnitOfWork)
            {
                var imUnitOfWork = unitOfWork as InMemoryUnitOfWork;
                
                imUnitOfWork.CurrentTransactionDataStore.Save(entity);
            }
            else
            {
                // TODO: Add message to exception
                throw new InvalidOperationException();
            }
        }
    }
}
