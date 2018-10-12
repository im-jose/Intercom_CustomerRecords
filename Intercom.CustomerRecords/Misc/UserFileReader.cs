using Intercom.CustomerRecords.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Intercom.CustomerRecords.Misc
{
    public class UserFileReader : IUserProvider
    {
        private const string FILENAME = "customers.txt";
        private readonly IUserJsonParser userParser;

        public UserFileReader(IUserJsonParser userParser)
        {
            this.userParser = userParser;
        }
        
        public List<User> getAllUsers()
        {
            List<User> users = new List<User>();
            string[] lines = ReadUsersFile();

            if (lines != null && lines.Count() > 0)
            {
                foreach (string line in lines)
                {
                    User user = userParser.ParseJsonLine(line);
                    if(null != user)
                    {
                        users.Add(user);
                    }
                }
            }

            List<User> sortedUserList = users.OrderBy(x => x.Id).ToList();
            return sortedUserList;
        }

        private static string[] ReadUsersFile()
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, @"Data\", FILENAME);

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CustomerFile"]))
            {
                filePath = ConfigurationManager.AppSettings["CustomerFile"];
            }

            if(!File.Exists(filePath))
            {
                throw new FileNotFoundException("Customer file can't be found, please check the path provided", filePath);
            }

            return File.ReadAllLines(filePath);
        }
    }
}
