using Logatron.Core.Contracts.Services;
using Logatron.Core.Database.DTOs;
using Logatron.Core.Helpers;
using Logatron.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Logatron.Core.Services;

public class DatabaseLogbookService : ILogbookService
{
    private readonly ILogbookContextService _contextService;

    public DatabaseLogbookService(ILogbookContextService contextService)
    {
        _contextService = contextService;
    }

    public async Task<int> GetNumberOfEntries()
    {
        using var context = _contextService.CreateContext();
        return await context.Entries.CountAsync();
    }

    public async Task<IEnumerable<LogbookEntry>> GetEntries(ILogbookService.PagingDefinition paging, ILogbookService.OrderingDefinition ordering)
    {
        using var context = _contextService.CreateContext();

        var skip = (paging.Page - 1) * paging.Limit;

        return await context.Entries
            .OrderBy(ordering.FieldName, ordering.Descending)
            .Skip(skip)
            .Take(paging.Limit)
            .Select(entry => ToLogbookEntry(entry)).ToListAsync();
    }

    public async Task<int> CreateEntry(LogbookEntry entry)
    {
        using var context = _contextService.CreateContext();
        var createdEntity = await context.Entries.AddAsync(ToLogbookEntryDTO(entry));
        await context.SaveChangesAsync();
        return createdEntity.Entity.Id;
    }

    public async Task UpdateEntry(LogbookEntry entry)
    {
        using var context = _contextService.CreateContext();
        var entryToUpdate = context.Entries.First(e => e.Id == entry.Id);
        UpdateLogbookEntryDTO(entry, ref entryToUpdate);
        context.Entries.Update(entryToUpdate);
        await context.SaveChangesAsync();
    }

    public async Task DeleteEntry(LogbookEntry entry)
    {
        using var context = _contextService.CreateContext();
        var entryToRemove = context.Entries.First(e => e.Id == entry.Id);
        context.Entries.Remove(entryToRemove);
        await context.SaveChangesAsync();
    }

    private static LogbookEntry ToLogbookEntry(LogbookEntryDTO logbookEntryDTO)
    {
        return new LogbookEntry
        {
            Id = logbookEntryDTO.Id,
            StartTime = logbookEntryDTO.StartTime.ToLocalTime(),
            EndTime = logbookEntryDTO.EndTime.ToLocalTime(),
            Callsign = logbookEntryDTO.Callsign,
            Name = logbookEntryDTO.Name,
            Comments = logbookEntryDTO.Comments
        };
    }

    private static LogbookEntryDTO ToLogbookEntryDTO(LogbookEntry logbookEntry)
    {
        return new LogbookEntryDTO
        {
            StartTime = logbookEntry.StartTime.ToUniversalTime(),
            EndTime = logbookEntry.EndTime.ToUniversalTime(),
            Callsign = logbookEntry.Callsign,
            Name = logbookEntry.Name,
            Comments = logbookEntry.Comments,
        };
    }

    private static void UpdateLogbookEntryDTO(LogbookEntry logbookEntry, ref LogbookEntryDTO logbookEntryDTO)
    {
        logbookEntryDTO.StartTime = logbookEntry.StartTime.ToUniversalTime();
        logbookEntryDTO.EndTime = logbookEntry.EndTime.ToUniversalTime();
        logbookEntryDTO.Callsign = logbookEntry.Callsign;
        logbookEntryDTO.Name = logbookEntry.Name;
        logbookEntryDTO.Comments = logbookEntry.Comments;
    }
}
