using Nancy;
using Nancy.TinyIoc;

namespace Floream.People
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {

        /// <summary>
        /// Add container configurations
        /// </summary>
        /// <param name="container"></param>
        /// <param name="context"></param>
        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);

        }

    }
}