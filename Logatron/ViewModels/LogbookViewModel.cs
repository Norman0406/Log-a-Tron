using Logatron.Models.Logbook;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Logatron.ViewModels
{
    public class LogbookViewModel : ViewModelBase
    {
        private readonly ObservableCollection<Entry> _entries;
        public IEnumerable<Entry> Entries => _entries;

        private LogbookEntryViewModel? _logbookEntryViewModel;
        public LogbookEntryViewModel? LogbookEntryViewModel
        {
            get { return _logbookEntryViewModel; }
            set { SetProperty(ref _logbookEntryViewModel, value); }
        }

        public LogbookViewModel()
        {
            _entries = new ObservableCollection<Entry>();
        }

        public LogbookViewModel(Database database)
        {
            database.Entries.Load();

            _entries = database.Entries.Local.ToObservableCollection();

            _logbookEntryViewModel = new LogbookEntryViewModel(database);

            // TODO: default sorting doesn't yet work
        }
    }
}
