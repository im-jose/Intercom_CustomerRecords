using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intercom.CustomerRecords.Models;
using System.IO;

namespace Intercom.CustomerRecords.Misc
{
    public class UserFileReader : IUserProvider
    {
        private const string FILENAME = "customers.txt";
        IUserJsonParser userParser;

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

            if(!File.Exists(filePath))
            {
                throw new FileNotFoundException("Customer file can't be found, please check the path provided", filePath);
            }

            return File.ReadAllLines(filePath);
        }
    }
}
