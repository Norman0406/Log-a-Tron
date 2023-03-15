using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Logatron.Models.Logbook;
using Microsoft.EntityFrameworkCore;
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
        private readonly DatabaseManager _dbManager;

        [ObservableProperty]
        private IEnumerable<LogbookEntryViewModel> _entries;

        [ObservableProperty]
        private LogbookEntryViewModel? _selectedEntry;

        [ObservableProperty]
        private LogbookEntryViewModel? _logbookEntryViewModel;

        public ICommand BeginEditCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand DeleteCommand { get; }

        public LogbookViewModel()
        {
        }

        public LogbookViewModel(DatabaseManager dbManager)
        {
            _dbManager = dbManager;
            _entries = new ObservableCollection<LogbookEntryViewModel>();

            BeginEditCommand = new AsyncRelayCommand(BeginEdit);
            AddCommand = new AsyncRelayCommand(Add);
            UpdateCommand = new AsyncRelayCommand(Update);
            ClearCommand = new RelayCommand(Clear);
            DeleteCommand = new AsyncRelayCommand(Delete);
        }

        public void Init()
        {
            UpdateList();

            // TODO: default sorting doesn't yet work
        }

        private void UpdateList()
        {
            var context = _dbManager.Context();
            context.Entries.Load();
            Entries = context.Entries.Local.ToObservableCollection().Select(entry =>
                new LogbookEntryViewModel(entry, UpdateCommand, ClearCommand));

            Clear();
        }

        private async Task Add()
        {
            var context = _dbManager.Context();
            Entry entry = new();
            LogbookEntryViewModel.UpdateEntry(ref entry);

            context.Add(entry);
            context.SaveChanges();

            UpdateList();
        }

        private async Task BeginEdit()
        {
            LogbookEntryViewModel = _entries.FirstOrDefault(entry => entry.Id == SelectedEntry.Id);
        }

        private async Task Update()
        {
            var context = _dbManager.Context();
            context.Entries.Load();

            var entry = await context.Entries.FirstAsync(entry => entry.Id == LogbookEntryViewModel.Id);
            LogbookEntryViewModel.UpdateEntry(ref entry);

            context.SaveChanges();

            UpdateList();
        }

        private void Clear()
        {
            LogbookEntryViewModel = new LogbookEntryViewModel(AddCommand, ClearCommand);
        }

        private async Task Delete()
        {
            if (MessageBox.Show(
                "Are you sure you want to delete this QSO?",
                "Delete QSO?",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                var context = _dbManager.Context();
                context.Entries.Load();
                var entry = await context.Entries.FirstAsync(entry => entry.Id == SelectedEntry.Id);
                context.Remove(entry);
                context.SaveChanges();

                UpdateList();
            }
        }
    }
}
