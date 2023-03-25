using Logatron.MVVM.Models;
using System.Threading.Tasks;

namespace Logatron.Database.Services
{
    public interface ILogbookEntryUpdater
    {
        Task UpdateEntry(LogbookEntry entry);
    }
}
