using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using Floream.People.DataSources.Entities;

namespace Floream.People
{
    public class Ldap
    {
        private readonly string _path;

        public Ldap(string path)
        {
            _path = path;
        }

        public bool IsAuthenticated(string domain, string username, string pwd)
        {
            try
            {
                var domainAndUsername = domain + @"\" + username;
                var entry = new DirectoryEntry(_path, domainAndUsername, pwd);

                //Bind to the native AdsObject to force authentication.
                var search = new DirectorySearcher(entry)
                {
                    Filter = "(SAMAccountName=" + username + ")"
                };
                var result = search.FindOne();
                return result != null;

            }
            catch
            {
                return false;
            }
        }

        public SearchResultCollection GetUsers()
        {
            try
            {
                var entry = new DirectoryEntry(_path);

                //Bind to the native AdsObject to force authentication.
                var search = new DirectorySearcher(entry)
                {
                    //Filter = "(SAMAccountName=" + username + ")"
                };

                search.PropertiesToLoad.Add("cn");
                search.PropertiesToLoad.Add("mail");
                search.PropertiesToLoad.Add("ou");
                var results = search.FindAll();

                return results.Count > 0 ? null : results;
            }
            catch
            {
                return null;
            }
        }
    }
}