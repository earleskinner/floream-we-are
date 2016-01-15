using System.Linq;
using Floream.People.DataSources.Context;
using Nancy;

namespace Floream.People.Modules
{
    public class HomeModule : NancyModule
    {
        private readonly PeopleContext _people;

        public HomeModule(PeopleContext people)
        {
            _people = people;

            Get["/"] = parameters =>
            {                               
                return View["index", _people.People.Where(p => !p.Hidden && !p.Retired).ToList()];
            };
            Post["/search/"] = parameters =>
            {
                var query = (string)Request.Form.query;
                var ppl = _people.People.Where(
                                p => !p.Hidden && 
                                     !p.Retired &&
                                    (p.AdUser.Contains(query) || 
                                     p.Name.Contains(query) || 
                                     p.Email.Contains(query))).ToList();
                return View["index", ppl];
            };
        }
    }
}