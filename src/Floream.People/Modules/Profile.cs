using System;
using Floream.People.DataSources.Context;
using Nancy;
using Nancy.Security;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Floream.People.Modules
{
    public class Profile : NancyModule
    {
        private readonly PeopleContext _people;

        public Profile(PeopleContext people)
        {
            //this.RequiresAuthentication();

            _people = people;

            Get["/profile"] = parameters =>
            {
                //// call when user visit it's own profile
                //var identity = Context.CurrentUser as FloreamIdentity;

                var person = new DataSources.Entities.Person
                {
                    Id = Guid.NewGuid(),
                    Hidden = false,
                    Created = DateTime.Now,
                    Name = "Mauro",
                    Position = "Developer",
                    Retired = false
                };

                //return View["profile", identity.Person];
                return View["Profile/Index", person];
            };

            Post["/Profile/UploadPicture"] = parameters =>
            {
                var file = Request.Files.FirstOrDefault();
                var a = 1;
                //var identity = Context.CurrentUser as FloreamIdentity;
                //identity.Person.Picture = file;

                //_people.SaveChanges();

                //src="data:image/gif;base64,R0lGODlhEAAQAMQAAORHHOVSKudfOulrSOp3WOyDZu6QdvCchPGolfO0o/XBs/fNwfjZ0frl3/zy7////wAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACH5BAkAABAALAAAAAAQABAAAAVVICSOZGlCQAosJ6mu7fiyZeKqNKToQGDsM8hBADgUXoGAiqhSvp5QAnQKGIgUhwFUYLCVDFCrKUE1lBavAViFIDlTImbKC5Gm2hB0SlBCBMQiB0UjIQA7"

                return null;
            };
        }

    }
}