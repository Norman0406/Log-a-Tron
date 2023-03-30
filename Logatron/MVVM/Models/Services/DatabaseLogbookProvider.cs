using Logatron.Database.Contexts;
using Logatron.Database.DTOs;
using Logatron.Database.Util;
using Logatron.MVVM.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Logatron.Database.Services.ILogbookProvider;

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

        public async Task<IEnumerable<LogbookEntry>> GetEntries(PagingDefinition paging, OrderingDefinition ordering)
        {
            using var context = _databaseFactory.CreateContext();

            var skip = (paging.Page - 1) * paging.Limit;

            return await context.Entries
                .OrderBy(ordering.FieldName, ordering.Descending)
                .Skip(skip)
                .Take(paging.Limit)
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
