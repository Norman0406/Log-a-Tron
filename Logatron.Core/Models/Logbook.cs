using Logatron.Core.Contracts.Services;

namespace Logatron.Core.Models;

public class Logbook
{
    private readonly ILogbookService _logbookService;

    public Logbook(ILogbookService logbookService)
    {
        _logbookService = logbookService;
    }

    public async Task<int> GetNumberOfEntries()
    {
        return await _logbookService.GetNumberOfEntries();
    }

    public async Task<IEnumerable<LogbookEntry>> GetEntries(ILogbookService.PagingDefinition paging, ILogbookService.OrderingDefinition ordering)
    {
        return await _logbookService.GetEntries(paging, ordering);
    }

    public async Task<int> CreateEntry(LogbookEntry logbookEntry)
    {
        return await _logbookService.CreateEntry(logbookEntry);
    }

    public async Task UpdateEntry(LogbookEntry logbookEntry)
    {
        await _logbookService.UpdateEntry(logbookEntry);
    }

    public async Task DeleteEntry(LogbookEntry logbookEntry)
    {
        await _logbookService.DeleteEntry(logbookEntry);
    }
}
