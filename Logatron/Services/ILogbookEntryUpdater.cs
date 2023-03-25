using Logatron.Models;
using System.Threading.Tasks;

namespace Logatron.Services
{
    public interface ILogbookEntryUpdater
    {
        Task UpdateEntry(LogbookEntry entry);
    }
}
