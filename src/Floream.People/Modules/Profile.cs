using Floream.People.DataSources.Context;
using Nancy;
using Nancy.Security;

namespace Floream.People.Modules
{
    public class Profile : NancyModule
    {
        private readonly PeopleContext _peopleContext;

        public Profile(PeopleContext peopleContext)
        {
            this.RequiresAuthentication();

            _peopleContext = peopleContext;

        }

    }
}