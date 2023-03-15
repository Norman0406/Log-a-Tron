using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Security.Cryptography;
using System.Windows.Input;

namespace Logatron.ViewModels
{
    public partial class LogbookEntryViewModel : ObservableObject
    {
        public int Id { get; } = 0;

        [ObservableProperty]
        private DateTime _startTime = DateTime.UtcNow;

        [ObservableProperty]
        private DateTime _endTime = DateTime.UtcNow;

        [ObservableProperty]
        private string _callsign = string.Empty;

        [ObservableProperty]
        private string _name = string.Empty;

        [ObservableProperty]
        private string _comments = string.Empty;

        public enum ViewMode
        {
            Adding,
            Updating
        }

        private ViewMode _mode = ViewMode.Adding;
        public ViewMode Mode
        {
            get { return _mode; }
            set { SetProperty(ref _mode, value); }
        }

        public ICommand ExecuteCommand { get; }
        public ICommand ClearCommand { get; }

        public LogbookEntryViewModel()
        {
            ExecuteCommand = new RelayCommand(() => { });
            ClearCommand = new RelayCommand(() => { });

            Clear();
        }

        public LogbookEntryViewModel(ICommand executeCommand, ICommand clearCommand)
        {
            _mode = ViewMode.Adding;
            ExecuteCommand = executeCommand;
            ClearCommand = clearCommand;

            Clear();
        }

        public LogbookEntryViewModel(Models.Logbook.Entry entry, ICommand command, ICommand clearCommand)
        {
            _mode = ViewMode.Updating;
            ExecuteCommand = command;
            ClearCommand = clearCommand;

            Id = entry.Id;
            StartTime = entry.StartTime;
            EndTime = entry.EndTime;
            Callsign = entry.Callsign;
            Name = entry.Name;
            Comments = entry.Comments;
        }

        public void UpdateEntry(ref Models.Logbook.Entry entry)
        {
            entry.StartTime = StartTime;
            entry.EndTime = EndTime;
            entry.Callsign = Callsign;
            entry.Name = Name;
            entry.Comments = Comments;
        }

        public void Clear()
        {
            StartTime = DateTime.UtcNow;
            EndTime = DateTime.UtcNow;
            Callsign = string.Empty;
            Name = string.Empty;
            Comments = string.Empty;
        }
    }
}
