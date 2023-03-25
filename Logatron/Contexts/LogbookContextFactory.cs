using Microsoft.EntityFrameworkCore;

namespace Logatron.Database
{
    public class LogbookContextFactory
    {
        private readonly string _filename;

        public LogbookContextFactory(string filename)
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
}
