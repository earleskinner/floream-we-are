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
using System.Drawing.Imaging;

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

                return View["Profile/Index", identity.Person];
            };

            Post["/Profile/UploadPicture"] = parameters =>
            {
                var file = Request.Files.FirstOrDefault();
                var a = 1;

                var identity = Context.CurrentUser as FloreamIdentity;

                var imageType = file.ContentType.Split('/')[1];

                MemoryStream memStream = new MemoryStream();
                //Resize the image
                Bitmap bmp = ScaleImage(Image.FromStream(file.Value), 125, 125);
                //Save the resized image to a Stream
                var imageFormatConverter = new ImageFormatConverter();
                bmp.Save(memStream, (ImageFormat)imageFormatConverter.ConvertFromString(imageType));                    
                var array = memStream.ToArray();

                var dbPeople = _people.People.FirstOrDefault(p => p.Id == identity.Person.Id);
                dbPeople.PictureExtension = imageType;
                dbPeople.Picture = array;
                _people.SaveChanges();

                identity.Person.Picture = array;
                identity.Person.PictureExtension = imageType;
                Context.CurrentUser = identity;

                return View["Profile/_Picture", dbPeople];
            };

            Post["/Profile/Save"] = parameters =>
            {
                var identity = Context.CurrentUser as FloreamIdentity;

                var dbPeople = _people.People.FirstOrDefault(p => p.Id == identity.Person.Id);
                dbPeople.Position = Request.Form.position; ;
                _people.SaveChanges();

                identity.Person.Position = Request.Form.position;

                return Request.Form.position;
            };

        }

        /// <summary>
        /// Scales an image proportionally.  Returns a bitmap.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="maxWidth"></param>
        /// <param name="maxHeight"></param>
        /// <returns></returns>
        private Bitmap ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            double ratio;
            if(image.Width > image.Height)
                ratio = (double)maxWidth / image.Width;
            else
                ratio = (double)maxHeight / image.Height;
                        
            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
            Bitmap bmp = new Bitmap(newImage);

            return bmp;
        }

        private Image GetImageFromByteArray(byte[] bytes)
        {
            using (var ms = new MemoryStream(bytes))
            {
                return Image.FromStream(ms);
            }
        }

    }
}