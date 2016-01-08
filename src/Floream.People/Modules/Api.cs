using Floream.People.DataSources.Context;
using Floream.People.Enums;
using Floream.People.Models;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses.Negotiation;
using System;
using System.Linq;

namespace Floream.People.Modules
{
    public class Api : NancyModule
    {
        private readonly PeopleContext _people;

        public Api(PeopleContext people)
            : base("/api")
        {

            _people = people;

            Get["/people"] = Search;

        }

        private Negotiator Search(dynamic parameters)
        {
            // search people
            SearchModel searchModel = this.Bind();

            var query = _people.People.AsQueryable();

            if (searchModel != null)
            {
                if (!string.IsNullOrWhiteSpace(searchModel.q))
                {
                    string[] tokens = searchModel.q.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    query = query.Where(p => tokens.All(t => p.Name.Contains(t)));
                }

                switch (searchModel.sort)
                {
                    case Sort.Descending:
                        query = query.OrderByDescending(p => p.Name);
                        break;
                    default:
                        query = query.OrderBy(p => p.Name);
                        break;
                }
            }

            var results = query.Select(p => new { p.Name, p.Position })
                .ToList();

            return Negotiate.WithModel(results);
        }

    }
}