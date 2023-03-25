using Logatron.Database.Contexts;
using Logatron.Database.DTOs;
using Logatron.MVVM.Models;
using System.Threading.Tasks;

namespace Logatron.Database.Services
{
    internal class DatabaseLogbookEntryCreator : ILogbookEntryCreator
    {
        private readonly LogbookContextFactory _databaseFactory;

        public DatabaseLogbookEntryCreator(LogbookContextFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        public async Task<int> CreateEntry(LogbookEntry entry)
        {
            using var context = _databaseFactory.CreateContext();
            var createdEntity = await context.Entries.AddAsync(ToLogbookEntryDTO(entry));
            await context.SaveChangesAsync();
            return createdEntity.Entity.Id;
        }

        private static LogbookEntryDTO ToLogbookEntryDTO(LogbookEntry logbookEntry)
        {
            return new LogbookEntryDTO
            {
                StartTime = logbookEntry.StartTime,
                EndTime = logbookEntry.EndTime,
                Callsign = logbookEntry.Callsign,
                Name = logbookEntry.Name,
                Comments = logbookEntry.Comments,
            };
        }
    }
}
