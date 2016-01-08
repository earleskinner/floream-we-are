using System;
using System.DirectoryServices;
using System.Text;

namespace Floream.People
{
    public class LdapAuth
    {
        private string _path;
        private string _filterAttribute;

        public LdapAuth(string path)
        {
            _path = path;
        }

        public bool IsAuthenticated(string domain, string username, string pwd)
        {
            var domainAndUsername = domain + @"\" + username;
            var entry = new DirectoryEntry(_path, domainAndUsername, pwd);

            try
            {	//Bind to the native AdsObject to force authentication.			

                var search = new DirectorySearcher(entry)
                {
                    Filter = "(SAMAccountName=" + username + ")"
                };

                search.PropertiesToLoad.Add("cn");
                var result = search.FindOne();

                if (null == result)
                {
                    return false;
                }

                //Update the new path to the user in the directory.
                _path = result.Path;
                _filterAttribute = (string)result.Properties["cn"][0];
            }
            catch (Exception ex)
            {
                throw new Exception("Error authenticating user. " + ex.Message);
            }

            return true;
        }

        public string GetGroups()
        {
            var search = new DirectorySearcher(_path)
            {
                Filter = "(cn=" + _filterAttribute + ")"
            };
            search.PropertiesToLoad.Add("memberOf");
            var groupNames = new StringBuilder();
            try
            {
                var result = search.FindOne();

                var propertyCount = result.Properties["memberOf"].Count;

                for (var propertyCounter = 0; propertyCounter < propertyCount; propertyCounter++)
                {
                    var dn = (string)result.Properties["memberOf"][propertyCounter];

                    var equalsIndex = dn.IndexOf("=", 1, StringComparison.Ordinal);
                    var commaIndex = dn.IndexOf(",", 1, StringComparison.Ordinal);
                    if (-1 == equalsIndex)
                    {
                        return null;
                    }

                    groupNames.Append(dn.Substring((equalsIndex + 1), (commaIndex - equalsIndex) - 1));
                    groupNames.Append("|");

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error obtaining group names. " + ex.Message);
            }
            return groupNames.ToString();
        } 
    }
}