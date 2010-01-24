using Lix.Commons.Tests;
using Lix.Web.Mvc.Testing;
using MbUnit.Framework;

namespace Lix.Web.Mvc.Tests.Testing
{
    [TestFixture]
    public class AuthorizationHelperTests
    {
        [Test]
        [Row("ControllerRole1")]
        [Row("ControllerRole2")]
        public void should_evaluate_to_true_for_controller_level_role(string role)
        {
            AuthorizationHelper.TestRoleAuthorization<AuthorizationTestController>(x => x.Index(), role).ShouldBeEqualTo(true);
        }

        [Test]
        [Row("ActionRole1")]
        [Row("ActionRole2")]
        public void should_evaluate_to_true_for_action_level_role(string role)
        {
            AuthorizationHelper.TestRoleAuthorization<AuthorizationTestController>(x => x.Index(), role).ShouldBeEqualTo(true);
        }

        [Test]
        [Row("ControllerUser1")]
        [Row("ControllerUser2")]
        public void should_evaluate_to_true_for_controller_level_user(string user)
        {
            AuthorizationHelper.TestUserAuthorization<AuthorizationTestController>(x => x.Index(), user).ShouldBeEqualTo(true);
        }

        [Test]
        [Row("ActionUser1")]
        [Row("ActionUser2")]
        public void should_evaluate_to_true_for_action_level_user(string user)
        {
            AuthorizationHelper.TestUserAuthorization<AuthorizationTestController>(x => x.Index(), user).ShouldBeEqualTo(true);
        }

        [Test]
        public void should_evaluate_to_false_for_controller_level_role()
        {
            AuthorizationHelper.TestRoleAuthorization<AuthorizationTestController>(x => x.Index(), "NoControllerRole").ShouldBeEqualTo(false);
        }

        [Test]
        public void should_evaluate_to_false_for_action_level_role()
        {
            AuthorizationHelper.TestRoleAuthorization<AuthorizationTestController>(x => x.Index(), "NoActionRole").ShouldBeEqualTo(false);
        }

        [Test]
        public void should_evaluate_to_false_for_controller_level_user()
        {
            AuthorizationHelper.TestUserAuthorization<AuthorizationTestController>(x => x.Index(), "NoControllerUser").ShouldBeEqualTo(false);
        }

        [Test]
        public void should_evaluate_to_false_for_action_level_user()
        {
            AuthorizationHelper.TestUserAuthorization<AuthorizationTestController>(x => x.Index(), "NoActionUser").ShouldBeEqualTo(false);
        }
    }
}