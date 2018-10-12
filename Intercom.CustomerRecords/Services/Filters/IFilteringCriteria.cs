using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intercom.CustomerRecords.Services.Filters
{
    public interface IFilteringCriteria<T>
    {
        IList<T> DoSearch(IList<T> initialList);
    }
}
