using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Logatron.Models;
using System.Windows.Input;

namespace Logatron.ViewModels
{
    public partial class LogbookEntryEditViewModel : LogbookEntryViewModelBase
    {
        [ObservableProperty]
        public LogbookEntry? _entry;

        /// <summary>
        /// This property will be true if we are adding a new entry to the logbook, and
        /// false if we are updating an existing entry.
        /// </summary>
        [ObservableProperty]
        public bool _isAdding = true;

        public ICommand CommitCommand { get; }
        public ICommand ClearCommand { get; }

        public LogbookEntryEditViewModel()
        {
            CommitCommand = new RelayCommand(() => { });
            ClearCommand = new RelayCommand(() => { });
        }

        public LogbookEntryEditViewModel(ICommand commitCommand, ICommand clearCommand)
        {
            IsAdding = true;
            CommitCommand = commitCommand;
            ClearCommand = clearCommand;
        }

        public LogbookEntryEditViewModel(LogbookEntry entry, ICommand commitCommand, ICommand clearCommand)
        {
            IsAdding = false;
            Entry = entry;
            CommitCommand = commitCommand;
            ClearCommand = clearCommand;

            StartTime = entry.StartTime;
            EndTime = entry.EndTime;
            Callsign = entry.Callsign;
            Name = entry.Name;
            Comments = entry.Comments;
        }
    }
}
