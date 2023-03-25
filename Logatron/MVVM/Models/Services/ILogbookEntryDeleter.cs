using Logatron.MVVM.Models;
using System.Threading.Tasks;

namespace Logatron.Database.Services
{
    public interface ILogbookEntryDeleter
    {
        Task DeleteEntry(LogbookEntry entry);
    }
}