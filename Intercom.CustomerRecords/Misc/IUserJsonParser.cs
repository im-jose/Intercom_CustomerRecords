using Intercom.CustomerRecords.Models;

namespace Intercom.CustomerRecords.Misc
{
    public interface IUserJsonParser
    {
        User ParseJsonLine(string jsonText);
    }
}