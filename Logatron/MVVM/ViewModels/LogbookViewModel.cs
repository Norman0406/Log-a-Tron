using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Logatron.MVVM.Models;
using Logatron.MVVM.Models.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Logatron.MVVM.ViewModels
{
    public partial class LogbookViewModel : ObservableObject
    {
        private readonly Logbook _logbook;

        private ObservableCollection<LogbookEntryListViewModel> _entries = new();

        [ObservableProperty]
        private ICollectionView _entriesView = CollectionViewSource.GetDefaultView(null);

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasSelectedEntry))]
        private LogbookEntryListViewModel? _selectedEntry;

        public bool HasSelectedEntry => SelectedEntry != null;

        [ObservableProperty]
        private LogbookEntryEditViewModel? _entryEdit;

        public ICommand UpdateLogbookCommand => new AsyncRelayCommand(UpdateLogbook);
        public ICommand CreateEntryCommand => new AsyncRelayCommand(CreateEntry);
        public ICommand BeginUpdateEntryCommand => new RelayCommand(BeginUpdateEntry);
        public ICommand UpdateEntryCommand => new AsyncRelayCommand(UpdateEntry);
        public ICommand DeleteEntryCommand => new AsyncRelayCommand(DeleteEntry);
        public ICommand ClearCommand => new RelayCommand(Clear);

        public LogbookViewModel()
        {
            var dummyLogbookService = new DummyLogbookService();
            _logbook = new Logbook(dummyLogbookService, dummyLogbookService, dummyLogbookService, dummyLogbookService);
        }

        public LogbookViewModel(Logbook logbook)
        {
            _logbook = logbook;
        }

        private async Task UpdateLogbook()
        {
            var entries = await _logbook.GetEntries();
            _entries = new ObservableCollection<LogbookEntryListViewModel>(
                entries.Select(entry => new LogbookEntryListViewModel(entry)));

            EntriesView = CollectionViewSource.GetDefaultView(_entries);
            EntriesView.SortDescriptions.Add(new SortDescription(nameof(LogbookEntryViewModelBase.StartTime), ListSortDirection.Descending));
        }

        private async Task CreateEntry()
        {
            LogbookEntry entry = new()
            {
                StartTime = EntryEdit.StartTime,
                EndTime = EntryEdit.EndTime,
                Callsign = EntryEdit.Callsign,
                Name = EntryEdit.Name,
                Comments = EntryEdit.Comments
            };

            var newId = await _logbook.CreateEntry(entry);

            entry.Id = newId;
            _entries.Add(new LogbookEntryListViewModel(entry));

            Clear();
        }

        private void BeginUpdateEntry()
        {
            if (SelectedEntry == null)
            {
                return;
            }

            EntryEdit = new LogbookEntryEditViewModel(SelectedEntry.Entry, UpdateEntryCommand, ClearCommand);
        }

        private async Task UpdateEntry()
        {
            LogbookEntry entry = new()
            {
                Id = EntryEdit.Entry.Id,
                StartTime = EntryEdit.StartTime,
                EndTime = EntryEdit.EndTime,
                Callsign = EntryEdit.Callsign,
                Name = EntryEdit.Name,
                Comments = EntryEdit.Comments
            };

            await _logbook.UpdateEntry(entry);
            var entryInList = _entries.First(e => e.Entry.Id == entry.Id);
            entryInList.Update(entry);
            Clear();
        }

        private async Task DeleteEntry()
        {
            if (SelectedEntry == null)
            {
                return;
            }

            if (MessageBox.Show(
                "Are you sure you want to delete this QSO?",
                "Delete QSO?",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                await _logbook.DeleteEntry(SelectedEntry.Entry);
                _entries.Remove(SelectedEntry);
                Clear();
            }
        }

        private void Clear()
        {
            EntryEdit = new LogbookEntryEditViewModel(CreateEntryCommand, ClearCommand);
        }
    }
}
