using Logatron.Database.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logatron.MVVM.Models.Services
{
    public class DummyLogbookService : ILogbookProvider, ILogbookEntryCreator, ILogbookEntryDeleter, ILogbookEntryUpdater
    {
        public Task<int> CreateEntry(LogbookEntry entry)
        {
            return Task.FromResult(0);
        }

        public Task DeleteEntry(LogbookEntry entry)
        {
            return Task.CompletedTask;
        }

        public Task<IEnumerable<LogbookEntry>> GetEntries()
        {
            return Task.FromResult(Enumerable.Empty<LogbookEntry>());
        }

        public Task UpdateEntry(LogbookEntry entry)
        {
            return Task.CompletedTask;
        }
    }
}
