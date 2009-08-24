using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Lix.Commons.Repositories.Linq2Sql;
using Lix.Commons.Tests.Repositories.Linq2Sql.Examples;
using MbUnit.Framework;

namespace Lix.Commons.Tests.Repositories.Linq2Sql
{
    [TestFixture]
    public class when_using_a_linq_2_sql_unit_of_work : when_using_a_unit_of_work<Linq2SqlUnitOfWork, Food>
    {
        private FoodDataClassesDataContext dataContext;

        [SetUp(Order = 0)]
        public virtual void SetUp()
        {
            this.dataContext = new FoodDataClassesDataContext(ConfigurationManager.ConnectionStrings["Linq2SqlTests"].ConnectionString);
            this.dataContext.CreateDatabase();
        }

        [TearDown(Order = 0)]
        public void TearDown()
        {
            this.dataContext.DeleteDatabase();
            this.dataContext.Dispose();
        }

        protected override Linq2SqlUnitOfWork CreateUnitOfWork()
        {
            return new Linq2SqlUnitOfWork(this.dataContext);
        }

        protected override IEnumerable<Food> List()
        {
            return this.dataContext.Foods.ToList();
        }
    }
}
