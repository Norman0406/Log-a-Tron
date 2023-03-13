using System;
using System.Windows.Input;

namespace Logatron.ViewModels
{
    public class LogbookEntryViewModel : ViewModelBase
    {
        private readonly Models.Logbook.Database? _database;

        private DateTime _startTime = DateTime.UtcNow;
        public DateTime StartTime
        {
            get { return _startTime; }
            set { SetProperty(ref _startTime, value); }
        }

        private DateTime _endTime = DateTime.UtcNow;
        public DateTime EndTime
        {
            get { return _endTime; }
            set { SetProperty(ref _endTime, value); }
        }

        private string _callsign = string.Empty;
        public string Callsign
        {
            get { return _callsign; }
            set { SetProperty(ref _callsign, value); }
        }

        private string _name = string.Empty;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _comments = string.Empty;
        public string Comments
        {
            get { return _comments; }
            set { SetProperty(ref _comments, value); }
        }

        private ICommand? _addEntryCommand;
        public ICommand AddEntryCommand => _addEntryCommand ??= new Util.CommandHandler(() => AddEntry(), () => true);

        public LogbookEntryViewModel()
        {
            Clear();
        }

        public LogbookEntryViewModel(Models.Logbook.Database database)
        {
            _database = database;
            Clear();
        }

        private void Clear()
        {
            StartTime = DateTime.UtcNow;
            EndTime = DateTime.UtcNow;
            Callsign = string.Empty;
            Name = string.Empty;
            Comments = string.Empty;
        }

        private void AddEntry()
        {
            if (_database == null)
            {
                return;
            }

            var entry = new Models.Logbook.Entry()
            {
                StartTime = StartTime,
                EndTime = EndTime,
                Callsign = Callsign,
                Name = Name,
                Comments = Comments,
            };

            _database.Entries.Add(entry);
            _database.SaveChanges();

            Clear();
        }
    }
}
