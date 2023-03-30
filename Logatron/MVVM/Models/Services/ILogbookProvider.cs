using Logatron.MVVM.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logatron.Database.Services
{
    public interface ILogbookProvider
    {
        public struct PagingDefinition
        {
            public int Page { get; set; }
            public int Limit { get; set; }
        }

        public struct OrderingDefinition
        {
            public string FieldName { get; set; }
            public bool Descending { get; set; }
        }

        Task<int> GetNumberOfEntries();

        Task<IEnumerable<LogbookEntry>> GetEntries(PagingDefinition paging, OrderingDefinition ordering);
    }
}
