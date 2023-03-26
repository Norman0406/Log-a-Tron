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

        public async Task<int> GetNumberOfEntries()
        {
            using var context = _databaseFactory.CreateContext();
            return await context.Entries.CountAsync();
        }

        public async Task<IEnumerable<LogbookEntry>> GetEntries(int page, int limit)
        {
            using var context = _databaseFactory.CreateContext();

            var skip = (page - 1) * limit;

            return await context.Entries
                .Skip(skip)
                .Take(limit)
                .OrderBy(c => c.StartTime)
                .Select(entry => ToLogbookEntry(entry)).ToListAsync();
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
