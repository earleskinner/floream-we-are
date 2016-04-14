using System.Configuration;
using Floream.People.DataSources.Context;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace Floream.People
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            // Register LDAP instance.
            var ldap = new Ldap(ConfigurationManager.AppSettings.Get("ldap-path"));
            container.Register(ldap);

            // Enable forms auth
            FormsAuthentication.Enable(pipelines,
                new FormsAuthenticationConfiguration
                {
                    RedirectUrl = "~/login",
                    UserMapper = container.Resolve<IUserMapper>()
                }
            );
        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);
            
            container.Register(new PeopleContext());
        }
    }
}