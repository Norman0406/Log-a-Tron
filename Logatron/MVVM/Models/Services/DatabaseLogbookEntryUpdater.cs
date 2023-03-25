using Logatron.Database.Contexts;
using Logatron.Database.DTOs;
using Logatron.MVVM.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Logatron.Database.Services
{
    internal class DatabaseLogbookEntryUpdater : ILogbookEntryUpdater
    {
        private readonly LogbookContextFactory _databaseFactory;

        public DatabaseLogbookEntryUpdater(LogbookContextFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        public async Task UpdateEntry(LogbookEntry entry)
        {
            using var context = _databaseFactory.CreateContext();
            var entryToUpdate = context.Entries.First(e => e.Id == entry.Id);
            UpdateLogbookEntryDTO(entry, ref entryToUpdate);
            context.Entries.Update(entryToUpdate);
            await context.SaveChangesAsync();
        }

        private static void UpdateLogbookEntryDTO(LogbookEntry logbookEntry, ref LogbookEntryDTO logbookEntryDTO)
        {
            logbookEntryDTO.StartTime = logbookEntry.StartTime;
            logbookEntryDTO.EndTime = logbookEntry.EndTime;
            logbookEntryDTO.Callsign = logbookEntry.Callsign;
            logbookEntryDTO.Name = logbookEntry.Name;
            logbookEntryDTO.Comments = logbookEntry.Comments;
        }
    }
}
