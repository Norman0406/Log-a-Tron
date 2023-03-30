using Logatron.Database.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Logatron.Database.Services.ILogbookProvider;

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

        public Task<int> GetNumberOfEntries()
        {
            return Task.FromResult(0);
        }

        public Task<IEnumerable<LogbookEntry>> GetEntries(PagingDefinition paging, OrderingDefinition ordering)
        {
            return Task.FromResult(Enumerable.Empty<LogbookEntry>());
        }

        public Task UpdateEntry(LogbookEntry entry)
        {
            return Task.CompletedTask;
        }
    }
}
