using Microsoft.EntityFrameworkCore;

namespace Logatron.Models.Logbook
{
    public class DatabaseFactory
    {
        private readonly string _filename;

        public DatabaseFactory(string filename)
        {
            _filename = filename;
            Context().Database.Migrate();
        }

        public Database Context()
        {
            return new Database(_filename);
        }
    }
}
