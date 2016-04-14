using System;

namespace Floream.People.Utils
{
    public static class HtmlHelper
    {
        /// <summary>
        /// Convert profile image (in bytes) to base64
        /// </summary>
        public static string GetProfileImage(byte[] picture, string pictureExtension)
        {
            return string.IsNullOrEmpty(pictureExtension) ? 
                "Content/image/default_avatar_150px.png" : 
                string.Format("data:image/{0};base64,{1}", pictureExtension, Convert.ToBase64String(picture));
        }
    }
}