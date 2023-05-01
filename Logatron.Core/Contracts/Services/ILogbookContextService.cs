using Logatron.Core.Database.Contexts;

namespace Logatron.Core.Contracts.Services;

public interface ILogbookContextService
{
    public LogbookContext CreateContext();
}
