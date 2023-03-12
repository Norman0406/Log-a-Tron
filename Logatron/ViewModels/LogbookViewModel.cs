using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace Logatron.ViewModels
{
    public class LogbookViewModel
    {
        public ObservableCollection<Logbook.Entry> Entries { get; }

        public LogbookViewModel(Logbook.Database database)
        {
            database.Entries.Load();
            Entries = database.Entries.Local.ToObservableCollection();
        }
    }
}
