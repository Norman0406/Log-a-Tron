using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Logatron.Models.Logbook
{
    public class Database : DbContext
    {
        public DbSet<Entry> Entries { get; set; }

        public string DbPath { get; }

        public Database(string filename)
        {
            DbPath = Path.Join(App.ConfigDir, filename);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
