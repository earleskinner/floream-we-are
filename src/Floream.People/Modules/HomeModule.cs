using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Floream.People.DataSources.Context;
using Floream.People.DataSources.Entities;
using Nancy;
using Nancy.Security;
using Floream.People.Models;
using Floream.People.Utils;
using System.Drawing;

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
                // call when user visit the home page

                //var peopleList = (from p in _people.People
                //                  select new PersonProfile{
                //                      Email = p.Email,
                //                      Name = p.Name,
                //                      Id = p.Id,
                //                      Picture = p.PictureExtension
                //                      PictureExtension = p.PictureExtension
                //                      Position = p.Position
                //                  }

                var peopleList = new List<PersonProfile>();

                foreach (var p in _people.People)
                {
                    peopleList.Add(new PersonProfile
                    {
                        Id = p.Id,
                        Department = p.Department,
                        Email = p.Email,
                        Name = p.Name,
                        Picture = p.Picture == null ? Convert.FromBase64String(Constants.picStormTrooper) : p.Picture,
                        PictureExtension = String.IsNullOrEmpty(p.PictureExtension) ? "jpg" : p.PictureExtension,
                        Position = p.Position
                    });
                }
                                
                return View["index", peopleList];
            };
        }
    }
}