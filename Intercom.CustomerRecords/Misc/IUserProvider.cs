using Intercom.CustomerRecords.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intercom.CustomerRecords.Misc
{
    public interface IUserProvider
    {
        List<User> getAllUsers();
    }
}
