using System;
using System.Configuration;
using Floream.People.DataSources.Context;
using Nancy;
using Nancy.Security;
using System.Drawing;
using System.IO;
using System.Linq;
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
                var user = _people.People.FirstOrDefault(p => p.AdUser == identity.UserName);

                return View["profile", user];
            };

            Post["/profile/upload"] = parameters =>
            {
                var file = Request.Files.FirstOrDefault();
                if (file == null)
                {
                    return new Response().WithStatusCode(HttpStatusCode.BadRequest);
                }
                
                var identity = Context.CurrentUser as FloreamIdentity;
                var imageType = file.ContentType.Split('/')[1];
                var imageHeight = int.Parse(ConfigurationManager.AppSettings.Get("profile-image-height"));
                var imageWidth = int.Parse(ConfigurationManager.AppSettings.Get("profile-image-width"));

                var memStream = new MemoryStream();
                var img = Image.FromStream(file.Value);
                if (img.Height > imageHeight || img.Width > imageWidth)
                {
                    // Resize the image
                    var bmp = ScaleImage(img, imageWidth, imageHeight);
                    // Save the resized image to a stream
                    var imageFormatConverter = new ImageFormatConverter();
                    var imageObj = imageFormatConverter.ConvertFromString(imageType);
                    if (imageObj != null)
                    {
                        bmp.Save(memStream, (ImageFormat) imageObj);
                    }
                }
                else
                {
                    img.Save(memStream, img.RawFormat);
                }
                     
                var array = memStream.ToArray();

                // Update the user's profile
                var user = _people.People.FirstOrDefault(p => p.AdUser == identity.UserName);
                if (user != null)
                {
                    user.PictureExtension = imageType;
                    user.Picture = array;
                    _people.SaveChanges();
                }

                return Response.AsText(HtmlHelper.GetProfileImage(array, imageType));
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