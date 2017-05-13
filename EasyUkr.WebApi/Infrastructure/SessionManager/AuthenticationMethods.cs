using System.Web;
using System.Web.Mvc;

namespace EasyUkr.WebApi.Infrastructure.SessionManager
{
    /// <summary>
    /// For fast checking admin` account
    /// </summary>
    public static class AuthenticationMethods
    {
       public static bool IsAuthorized => HttpContext.Current.User.IsInRole("AppAdmin") &&
                                           HttpContext.Current.User.Identity.IsAuthenticated;

        /// <summary>
        /// If it is admin` account,will redirect to right way or wrong.
        /// </summary>
        /// <param name="rightView"></param>
        /// <param name="wrongView"></param>
        /// <returns></returns>
        public static ActionResult RedirectToView(ActionResult rightView, ActionResult wrongView)
        {
            return IsAuthorized ? rightView : wrongView;
        }
    }
}