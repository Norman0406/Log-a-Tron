using Logatron.Models;
using System.Threading.Tasks;

namespace Logatron.Services
{
    public interface ILogbookEntryCreator
    {
        Task<int> CreateEntry(LogbookEntry entry);
    }
}
