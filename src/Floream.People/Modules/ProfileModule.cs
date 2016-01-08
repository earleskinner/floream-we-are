using Floream.People.DataSources.Context;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Floream.People.Modules
{
    public class ProfileModule : NancyModule
    {
        private PeopleContext _people;

        public ProfileModule(PeopleContext people)
        {
            _people = people;

            Get["/Profile"] = parameters => 
                {
                    return View["Index"];
                };
            //Post["/Profile/UploadPicture"] = parameters => {
            //    Request.Files
            //} ;
        }
    }
}