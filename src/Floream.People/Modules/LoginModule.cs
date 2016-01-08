using System.Configuration;
using System.Linq;
using System.Security.Authentication;
using System.Web.UI.WebControls;
using Floream.People.DataSources.Context;
using Nancy;
using Nancy.Authentication.Forms;

namespace Floream.People.Modules
{
    /// <summary>
    /// Authentication to change profile picture
    /// </summary>
    public class LoginModule : NancyModule
    {
        private readonly PeopleContext _people;

        public LoginModule(PeopleContext people)
        {
            _people = people;

            Get["/login"] = parameters =>
            {
                // Called when the user visits the login page or is redirected here because
                // an attempt was made to access a restricted resource. It should return
                // the view that contains the login form
                return View["login"];
            };

            Get["/logout"] = parameters =>
            {
                // Called when the user clicks the sign out button in the application. Should
                // perform one of the Logout actions (see below)
                return this.LogoutAndRedirect("/");
            };

            Post["/login"] = parameters =>
            {
                // Called when the user submits the contents of the login form. Should
                // validate the user based on the posted form data, and perform one of the
                // Login actions (see below)
                var username = (string) Request.Form.username;
                var password = (string) Request.Form.password;

                var user = _people.People.FirstOrDefault(p => p.AdUser == username && !p.Hidden && !p.Retired);
                if (user == null)
                {
                    // Add exception to the view
                    // TODO
                    return View["login"];
                }

                // Authenticate user against AD
                var ldap = new LdapAuth(ConfigurationManager.AppSettings.Get("ldap-path"));
                if (!ldap.IsAuthenticated(ConfigurationManager.AppSettings.Get("ldap-domain"), username, password))
                {
                    // Add exception to the view
                    // TODO
                    return View["login"];
                }

                return this.LoginAndRedirect(user.Id, null, "/profile");
            };
        }
    }
}