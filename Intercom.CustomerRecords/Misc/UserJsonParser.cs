using Intercom.CustomerRecords.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intercom.CustomerRecords.Misc
{
    public class UserJsonParser : IUserJsonParser
    {
        public User ParseJsonLine(string jsonText)
        {
            UserDTO dto = null;

            try
            {
                dto = JsonConvert.DeserializeObject<UserDTO>(jsonText);
                return GetUserModel(dto);
            }
            catch (Exception)
            {
                //TODO: Log Exception
                //swallow exception on purpose
                return null;
            }
        }

        private static User GetUserModel(UserDTO dto)
        {
            Location location = new Location(latitude: dto.latitude, longitude: dto.longitude);
            User user = new User(dto.user_id, dto.name);
            user.Location = location;
            return user;
        }
    }
}
