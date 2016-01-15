using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Floream.People.Utils
{
    public static class HtmlHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public static string GetProfileImage(byte[] picture, string pictureExtension)
        {
            if (String.IsNullOrEmpty(pictureExtension))
                return "Content/image/default_avatar_150px.png";

            return String.Format("data:image/{0};base64,{1}", pictureExtension, Convert.ToBase64String(picture));
        }
    }
}