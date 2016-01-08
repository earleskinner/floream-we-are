using Nancy;

namespace Floream.People.Modules
{
    public class HomeModule : NancyModule
    {

        public HomeModule()
        {

            Get["/"] = parameters =>
            {
                return View["index"];
            };

        }
    }
}