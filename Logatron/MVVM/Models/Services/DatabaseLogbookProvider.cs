using Logatron.Database.Contexts;
using Logatron.Database.DTOs;
using Logatron.MVVM.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logatron.Database.Services
{
    internal class DatabaseLogbookProvider : ILogbookProvider
    {
        private readonly LogbookContextFactory _databaseFactory;

        public DatabaseLogbookProvider(LogbookContextFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        public async Task<IEnumerable<LogbookEntry>> GetEntries()
        {
            using var context = _databaseFactory.CreateContext();
            return await context.Entries.Select(entry => ToLogbookEntry(entry)).ToListAsync();
        }

        private static LogbookEntry ToLogbookEntry(LogbookEntryDTO logbookEntryDTO)
        {
            return new LogbookEntry
            {
                Id = logbookEntryDTO.Id,
                StartTime = logbookEntryDTO.StartTime,
                EndTime = logbookEntryDTO.EndTime,
                Callsign = logbookEntryDTO.Callsign,
                Name = logbookEntryDTO.Name,
                Comments = logbookEntryDTO.Comments
            };
        }
    }
}
