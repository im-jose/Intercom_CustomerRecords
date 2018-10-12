using Intercom.CustomerRecords.Models;

namespace Intercom.CustomerRecords.Misc
{
    public interface ICustomerJsonParser
    {
        Customer ParseJsonLine(string jsonText);
    }
}