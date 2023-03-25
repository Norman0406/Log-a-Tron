using Logatron.Database.DTOs;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Logatron.Database.Contexts
{
    public class LogbookContext : DbContext
    {
        public DbSet<LogbookEntryDTO> Entries { get; set; }

        public string DbPath { get; }

        public LogbookContext(string filename)
        {
            DbPath = Path.Join(App.ConfigDir, filename);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
