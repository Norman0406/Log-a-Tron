using Logatron.MVVM.Models;
using System.Threading.Tasks;

namespace Logatron.Database.Services
{
    public interface ILogbookEntryCreator
    {
        Task<int> CreateEntry(LogbookEntry entry);
    }
}
