using System.Web.Mvc;

namespace Lix.Web.Mvc.Tests.Testing
{
    [Authorize(Roles = "ControllerRole1, ControllerRole2", Users = "ControllerUser1, ControllerUser2")]
    public class AuthorizationTestController : Controller
    {
        [Authorize(Roles = "ActionRole1, ActionRole2", Users = "ActionUser1, ActionUser2")]
        public ActionResult Index()
        {
            return this.View();
        }
    }
}