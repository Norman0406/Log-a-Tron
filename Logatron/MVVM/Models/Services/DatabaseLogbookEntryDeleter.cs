using Logatron.Database.Contexts;
using Logatron.MVVM.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Logatron.Database.Services
{
    internal class DatabaseLogbookEntryDeleter : ILogbookEntryDeleter
    {
        private readonly LogbookContextFactory _databaseFactory;

        public DatabaseLogbookEntryDeleter(LogbookContextFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        public async Task DeleteEntry(LogbookEntry entry)
        {
            using var context = _databaseFactory.CreateContext();
            var entryToRemove = context.Entries.First(e => e.Id == entry.Id);
            context.Entries.Remove(entryToRemove);
            await context.SaveChangesAsync();
        }
    }
}
