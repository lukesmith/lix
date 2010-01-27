using System;
using Lix.Commons.Repositories;
using StructureMap;

namespace Lix.StructureMapAdapter
{
    public class UnitOfWorkScope : IDisposable
    {
        private readonly IUnitOfWork unitOfWork = ObjectFactory.GetInstance<IUnitOfWork>();

        public UnitOfWorkScope()
        {
            this.unitOfWork.Begin();
        }

        public void Dispose()
        {
            try
            {
                if (this.unitOfWork.IsActive)
                {
                    this.unitOfWork.Commit();
                }
            }
            catch
            {
                this.unitOfWork.Rollback();
                throw;
            }
            finally
            {
                this.unitOfWork.Dispose();
            }
        }
    }
}