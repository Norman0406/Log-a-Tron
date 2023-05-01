using Logatron.Core.Models;

namespace Logatron.Core.Contracts.Services;

public interface ILogbookService
{
    public struct PagingDefinition
    {
        public int Page
        {
            get; set;
        }
        public int Limit
        {
            get; set;
        }
    }

    public struct OrderingDefinition
    {
        public string FieldName
        {
            get; set;
        }
        public bool Descending
        {
            get; set;
        }
    }

    Task<int> GetNumberOfEntries();

    Task<IEnumerable<LogbookEntry>> GetEntries(PagingDefinition paging, OrderingDefinition ordering);

    Task<int> CreateEntry(LogbookEntry entry);

    Task UpdateEntry(LogbookEntry entry);

    Task DeleteEntry(LogbookEntry entry);
}
