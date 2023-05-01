using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Helpers;
using Logatron.Core.Models;

namespace Logatron.ViewModels;

public partial class LogbookEntryEditViewModel : LogbookEntryViewModelBase
{
    [ObservableProperty]
    public LogbookEntry? _entry;

    [ObservableProperty]
    private DateTimeOffset _startDate;

    [ObservableProperty]
    private TimeSpan _startTime;

    [ObservableProperty]
    private DateTimeOffset _endDate;

    [ObservableProperty]
    private TimeSpan _endTime;

    /// <summary>
    /// This property will be true if we are adding a new entry to the logbook, and
    /// false if we are updating an existing entry.
    /// </summary>
    [ObservableProperty]
    public bool _isAdding = true;

    public ICommand CommitCommand
    {
        get;
    }

    public ICommand ClearCommand
    {
        get;
    }

    public LogbookEntryEditViewModel(ICommand commitCommand, ICommand clearCommand)
    {
        IsAdding = true;
        CommitCommand = commitCommand;
        ClearCommand = clearCommand;

        var now = DateTime.Now;
        StartDate = now.ToDate();
        StartTime = now.ToTime();
        EndDate = now.ToDate();
        EndTime = now.ToTime();
    }

    public LogbookEntryEditViewModel(LogbookEntry entry, ICommand commitCommand, ICommand clearCommand)
    {
        IsAdding = false;
        Entry = entry;
        CommitCommand = commitCommand;
        ClearCommand = clearCommand;

        StartDate = entry.StartTime.ToDate();
        StartTime = entry.StartTime.ToTime();
        EndDate = entry.EndTime.ToDate();
        EndTime = entry.EndTime.ToTime();
        Callsign = entry.Callsign;
        Name = entry.Name;
        Comments = entry.Comments;
    }
}
