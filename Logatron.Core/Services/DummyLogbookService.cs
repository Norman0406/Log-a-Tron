using Logatron.Core.Contracts.Services;
using Logatron.Core.Helpers;
using Logatron.Core.Models;

namespace Logatron.Core.Services;

public class DummyLogbookService : ILogbookService
{
    private readonly IList<LogbookEntry> _dummyEntries = Enumerable.Range(1, 148)
        .Select(i => new LogbookEntry()
        {
            Callsign = "HB9HTX"
        })
        .ToList();

    public Task<int> CreateEntry(LogbookEntry entry)
    {
        var newId = _dummyEntries.Max(x => x.Id);
        entry.Id = newId;
        _dummyEntries.Add(entry);
        return Task.FromResult(newId);
    }

    public Task DeleteEntry(LogbookEntry entry)
    {
        _dummyEntries.Remove(entry);
        return Task.CompletedTask;
    }

    public Task<int> GetNumberOfEntries()
    {
        return Task.FromResult(_dummyEntries.Count);
    }

    public Task<IEnumerable<LogbookEntry>> GetEntries(ILogbookService.PagingDefinition paging, ILogbookService.OrderingDefinition ordering)
    {
        var skip = (paging.Page - 1) * paging.Limit;

        var results = _dummyEntries
            .AsQueryable()
            .OrderBy(ordering.FieldName, ordering.Descending)
            .Skip(skip)
            .Take(paging.Limit)
            .AsEnumerable();

        return Task.FromResult(results);
    }

    public Task UpdateEntry(LogbookEntry entry)
    {
        var entryToUpdate = _dummyEntries.FirstOrDefault(e => e.Id == entry.Id) ?? throw new InvalidOperationException("Did not find entry");

        entryToUpdate.StartTime = entry.StartTime;
        entryToUpdate.EndTime = entry.EndTime;
        entryToUpdate.Callsign = entry.Callsign;
        entryToUpdate.Name = entry.Name;
        entryToUpdate.Comments = entry.Comments;

        return Task.CompletedTask;
    }
}
