using Logatron.Core.Contracts.Services;
using Microsoft.EntityFrameworkCore;

namespace Logatron.Core.Database.Contexts;

public class LogbookContextService : ILogbookContextService
{
    private readonly string _filename;

    public LogbookContextService(string filename)
    {
        _filename = filename;

        using var context = CreateContext();
        context.Database.Migrate();
    }

    public LogbookContext CreateContext()
    {
        return new LogbookContext(_filename);
    }
}
