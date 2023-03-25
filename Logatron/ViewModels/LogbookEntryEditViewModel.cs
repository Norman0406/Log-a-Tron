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

        public ICommand CommitCommand { get; }
        public ICommand ClearCommand { get; }

        public LogbookEntryEditViewModel()
        {
            CommitCommand = new RelayCommand(() => { });
            ClearCommand = new RelayCommand(() => { });
        }

        public LogbookEntryEditViewModel(ICommand commitCommand, ICommand clearCommand)
        {
            CommitCommand = commitCommand;
            ClearCommand = clearCommand;
        }

        public LogbookEntryEditViewModel(LogbookEntry entry, ICommand commitCommand, ICommand clearCommand)
        {
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
