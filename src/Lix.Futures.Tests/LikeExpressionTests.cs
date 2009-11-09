using System;
using System.Linq;
using Lix.Commons.Tests.Examples;
using Lix.Futures.Extensions;
using Lix.NHibernate.Utilities.Tests.Repositories;
using MbUnit.Framework;

namespace Lix.Futures.Tests
{
    [TestFixture]
    public class LikeExpressionTests : nhibernate_test_setups
    {
        protected override void PerformSetUp()
        {
            base.PerformSetUp();

            this.Session.Save(new Fish { Description = "Hello word0" });
        }

        [Test]
        public void should_not_throw_with_a_constant_expression()
        {
            var query = this.Session.Linq<Fish>().Where(x => x.Description.Like("d_", ComparisonType.EndsWith));

            query.ToList();
        }

        [Test]
        public void should_not_throw_with_a_member_expression()
        {
            const string value = "d_";
            var query = this.Session.Linq<Fish>().Where(x => x.Description.Like(value, ComparisonType.EndsWith));

            query.ToList();
        }

        [Test]
        public void should_not_throw_with_a_invoke_expression()
        {
            Func<string, string> func = x => "he_llo";
            var query = this.Session.Linq<Fish>().Where(x => x.Description.Like(func("hel"), ComparisonType.EndsWith));

            query.ToList();
        }

        [Test]
        public void should_not_throw_with_a_call_expression()
        {
            var query = this.Session.Linq<Fish>().Where(x => x.Description.Like(Test(), ComparisonType.EndsWith));
            
            query.ToList();
        }

        private static string Test()
        {
            return "_test";
        }
    }
}
