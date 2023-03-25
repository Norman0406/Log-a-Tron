using CommunityToolkit.Mvvm.ComponentModel;
using Logatron.MVVM.Models;
using System;

namespace Logatron.MVVM.ViewModels
{
    public partial class LogbookEntryListViewModel : LogbookEntryViewModelBase
    {
        [ObservableProperty]
        public LogbookEntry _entry;

        public LogbookEntryListViewModel(LogbookEntry entry)
        {
            Update(entry);
        }

        public void Update(LogbookEntry entry)
        {
            if (entry.Id != entry.Id)
            {
                throw new InvalidOperationException("Cannot update entry, id's do not match");
            }

            Entry = entry;
            StartTime = entry.StartTime;
            EndTime = entry.EndTime;
            Callsign = entry.Callsign;
            Name = entry.Name;
            Comments = entry.Comments;
        }
    }
}
