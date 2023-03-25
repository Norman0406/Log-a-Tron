using Logatron.DTOs;
using Logatron.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logatron.Services
{
    public interface ILogbookProvider
    {
        Task<IEnumerable<LogbookEntry>> GetEntries();
    }
}
