using System;
using Floream.People.DataSources.Context;
using Nancy;
using Nancy.Security;
using System.Drawing;
using System.IO;
using System.Linq;
using Floream.People.Models;
using Floream.People.DataSources.Entities;
using Floream.People.Utils;

namespace Floream.People.Modules
{
    public class ProfileModule : NancyModule
    {
        private readonly PeopleContext _people;

        public ProfileModule(PeopleContext people)
        {
            this.RequiresAuthentication();

            _people = people;

            Get["/profile"] = parameters =>
            {
                // call when user visit it's own profile
                var identity = Context.CurrentUser as FloreamIdentity;

                if (identity.Person.Picture == null)
                {
                    identity.Person.Picture = Convert.FromBase64String(Constants.picStormTrooper);
                    identity.Person.PictureExtension = "jpg";
                }

                return View["Profile/Index", identity.Person];
            };

            Post["/Profile/UploadPicture"] = parameters =>
            {
                var file = Request.Files.FirstOrDefault();
                var a = 1;

                var identity = Context.CurrentUser as FloreamIdentity;
                identity.Person.PictureExtension = file.ContentType.Split('/')[1];

                MemoryStream memStream = new MemoryStream();
                file.Value.CopyTo(memStream);
                var array = memStream.ToArray();

                identity.Person.Picture = array;                
                
                _people.SaveChanges();                
                
                return View["Profile/_Picture", identity.Person];
            };
        }

    }
}