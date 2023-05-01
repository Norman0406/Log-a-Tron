using CommunityToolkit.Mvvm.ComponentModel;
using Logatron.Core.Models;

namespace Logatron.ViewModels;

public partial class LogbookEntryListViewModel : LogbookEntryViewModelBase
{
    [ObservableProperty]
    public LogbookEntry _entry;

    [ObservableProperty]
    private DateTime _startTime = DateTime.Now;

    [ObservableProperty]
    private DateTime _endTime = DateTime.Now;

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
