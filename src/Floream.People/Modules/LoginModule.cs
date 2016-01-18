using System.Configuration;
using System.Linq;
using Floream.People.DataSources.Context;
using Nancy;
using Nancy.Authentication.Forms;
using Floream.People.DataSources.Entities;
using System;

namespace Floream.People.Modules
{
    /// <summary>
    /// Authentication to change profile picture
    /// </summary>
    public class LoginModule : NancyModule
    {
        private readonly PeopleContext _people;
        private readonly Ldap _ldap;

        public LoginModule(PeopleContext people, Ldap ldap)
        {
            _people = people;
            _ldap = ldap;

            Get["/login"] = parameters =>
            {
                // Called when the user visits the login page or is redirected here because
                // an attempt was made to access a restricted resource. It should return
                // the view that contains the login form
                return View["login"];
            };

            Get["/logoff"] = parameters =>
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

                // Authenticate user against AD
                if (!ldap.IsAuthenticated(ConfigurationManager.AppSettings.Get("ldap-domain"), username, password))
                {
                    return View["login", "Unable to validate your account. Please contact the dev team at dev@floream.com"];
                }

                var user = _people.People.FirstOrDefault(p => p.AdUser == username && !p.Hidden && !p.Retired);
                if (user == null)
                {
                    // TODO - Create user, because already ldap authed.
                    var newUser = ldap.GetUser(username);

                    user = new Person
                    {
                        Id = Guid.NewGuid(),
                        AdUser = username,
                        Created = DateTime.Now,
                        Department = newUser.Properties["department"][0].ToString(),
                        Email = newUser.Properties["mail"][0].ToString(),
                        Name = newUser.Properties["displayName"][0].ToString(),
                        Position = newUser.Properties["title"][0].ToString()
                    };

                    user = _people.People.Add(user);
                    _people.SaveChanges();
                }

                return this.LoginAndRedirect(user.Id, null, "/profile");
            };
        }
    }
}