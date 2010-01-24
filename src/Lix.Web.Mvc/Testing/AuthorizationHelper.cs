using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Lix.Web.Mvc.Testing
{
    public static class AuthorizationHelper
    {
        public static bool TestRoleAuthorization<TController>(Expression<Func<TController, ActionResult>> action, string role)
            where TController : Controller
        {
            return IsValidAuthorization(action, x => x.Roles, role);
        }

        public static bool TestUserAuthorization<TController>(Expression<Func<TController, ActionResult>> action, string user)
            where TController : Controller
        {
            return IsValidAuthorization(action, x => x.Users, user);
        }

        private static bool IsValidAuthorization<TController>(Expression<Func<TController, ActionResult>> action, Func<AuthorizeAttribute, string> validate, string expected)
            where TController : Controller
        {
            var result = false;
            foreach (var attribute in GetAuthorizationAttributes(action))
            {
                result = validate.Invoke(attribute).Split(',').Any(x => x.Trim().CompareTo(expected) == 0);

                if (result)
                {
                    break;
                }
            }

            return result;
        }

        private static IEnumerable<AuthorizeAttribute> GetAuthorizationAttributes<TController>(Expression<Func<TController, ActionResult>> action)
            where TController : Controller
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            var body = action.Body as MethodCallExpression;

            if (body == null)
            {
                throw new ArgumentException("Expression body must be a method call", "action");
            }

            var controllerType = typeof(TController);
            var actionMethod = controllerType.GetMethod(body.Method.Name);
            var authorizeAttributes = actionMethod.GetCustomAttributes(typeof(AuthorizeAttribute), true)
                .Union(controllerType.GetCustomAttributes(typeof(AuthorizeAttribute), true))
                .Cast<AuthorizeAttribute>();

            return authorizeAttributes;
        }
    }
}