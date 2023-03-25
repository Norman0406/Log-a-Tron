using Logatron.Models;
using System.Threading.Tasks;

namespace Logatron.Services
{
    public interface ILogbookEntryDeleter
    {
        Task DeleteEntry(LogbookEntry entry);
    }
}