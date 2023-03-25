using Logatron.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logatron.Models
{
    public class Logbook
    {
        private readonly ILogbookProvider _logbookProvider;
        private readonly ILogbookEntryCreator _entryCreator;
        private readonly ILogbookEntryUpdater _entryUpdater;
        private readonly ILogbookEntryDeleter _entryDeleter;

        public Logbook(
            ILogbookProvider logbookProvider,
            ILogbookEntryCreator entryCreator,
            ILogbookEntryUpdater entryUpdater,
            ILogbookEntryDeleter entryDeleter)
        {
            _logbookProvider = logbookProvider;
            _entryCreator = entryCreator;
            _entryUpdater = entryUpdater;
            _entryDeleter = entryDeleter;
        }

        public async Task<IEnumerable<LogbookEntry>> GetEntries()
        {
            return await _logbookProvider.GetEntries();
        }

        public async Task<int> CreateEntry(LogbookEntry logbookEntry)
        {
            return await _entryCreator.CreateEntry(logbookEntry);
        }

        public async Task UpdateEntry(LogbookEntry logbookEntry)
        {
            await _entryUpdater.UpdateEntry(logbookEntry);
        }

        public async Task DeleteEntry(LogbookEntry logbookEntry)
        {
            await _entryDeleter.DeleteEntry(logbookEntry);
        }
    }
}
