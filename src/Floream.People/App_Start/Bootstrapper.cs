using Floream.People.DataSources.Context;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace Floream.People
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            // Register new ad user here.
        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);

            container.Register(new PeopleContext());
        }
    }
}