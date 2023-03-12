using Microsoft.EntityFrameworkCore;
using System;

namespace Logatron.Logbook
{
    public class Database : DbContext
    {
        public DbSet<Entry> Entries { get; set; }

        public string DbPath { get; }

        public Database(string filename)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, filename);

            Database.EnsureCreated();
        }

        //// The following configures EF to create a Sqlite database file in the
        //// special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
