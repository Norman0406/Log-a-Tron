using Logatron.MVVM.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logatron.Database.Services
{
    public interface ILogbookProvider
    {
        Task<IEnumerable<LogbookEntry>> GetEntries();
    }
}
