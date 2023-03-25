using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Logatron.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Logatron.ViewModels
{
    public partial class LogbookViewModel : ObservableObject
    {
        private readonly Logbook _logbook;

        [ObservableProperty]
        private ObservableCollection<LogbookEntryListViewModel> _entries = new();

        [ObservableProperty]
        private LogbookEntryListViewModel? _selectedEntry;

        [ObservableProperty]
        private LogbookEntryEditViewModel? _entryEdit;

        private ICommand UpdateLogbookCommand { get; }
        public ICommand BeginEditCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand DeleteCommand { get; }

        public LogbookViewModel()
        {
        }

        public LogbookViewModel(Logbook logbook)
        {
            _logbook = logbook;

            UpdateLogbookCommand = new AsyncRelayCommand(UpdateList);
            BeginEditCommand = new AsyncRelayCommand(BeginEdit);
            AddCommand = new AsyncRelayCommand(Add);
            UpdateCommand = new AsyncRelayCommand(Update);
            ClearCommand = new RelayCommand(Clear);
            DeleteCommand = new AsyncRelayCommand(Delete);
        }

        public void Init()
        {
            UpdateLogbookCommand.Execute(null);
            Clear();
        }

        private async Task UpdateList()
        {
            var entries = await _logbook.GetEntries();
            Entries = new ObservableCollection<LogbookEntryListViewModel>(
                entries.Select(entry => new LogbookEntryListViewModel(entry)));
        }

        private async Task Add()
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
            Entries.Add(new LogbookEntryListViewModel(entry));

            Clear();
        }

        private async Task BeginEdit()
        {
            EntryEdit = new LogbookEntryEditViewModel(SelectedEntry.Entry, UpdateCommand, ClearCommand);
        }

        private async Task Update()
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
            var entryInList = Entries.First(e => e.Entry.Id == entry.Id);
            entryInList.Update(entry);
        }

        private void Clear()
        {
            EntryEdit = new LogbookEntryEditViewModel(AddCommand, ClearCommand);
        }

        private async Task Delete()
        {
            if (MessageBox.Show(
                "Are you sure you want to delete this QSO?",
                "Delete QSO?",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                await _logbook.DeleteEntry(SelectedEntry.Entry);
                Entries.Remove(SelectedEntry);
            }
        }
    }
}
