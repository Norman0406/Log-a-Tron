using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Helpers;
using Logatron.Core.Models;
using Microsoft.UI.Xaml.Controls;

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

    [ObservableProperty]
    private bool _shouldUpdateStartTime = true;

    [ObservableProperty]
    private bool _updateStartTimeEnabled = true;

    [ObservableProperty]
    private bool _shouldUpdateEndTime = true;

    [ObservableProperty]
    private bool _updateEndTimeEnabled = true;

    /// <summary>
    /// This property will be true if we are adding a new entry to the logbook, and
    /// false if we are updating an existing entry.
    /// </summary>
    [ObservableProperty]
    public bool _isAdding = true;

    /// <summary>
    /// This property will become true once a user changed data in the UI, so that the timer can stop
    /// automatically updating the start time.
    /// </summary>
    private bool _dataChanged = false;

    /// <summary>
    /// prevent updating _dataChanged when the timer made changes to the date and time ui elements.
    /// </summary>
    private bool _ignoreUpdates = true;

    /// <summary>
    /// This timer automatically updates the start time, until a user started entering something.
    /// </summary>
    private readonly Timer? _timeUpdateTimer;

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

        // set up the timer that will update our start and end times until data has been entered
        _timeUpdateTimer = new(new TimerCallback(UpdateTime));
        _timeUpdateTimer.Change(0, 1000);

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
        ShouldUpdateStartTime = false;
        UpdateStartTimeEnabled = false;
        ShouldUpdateEndTime = false;
        UpdateEndTimeEnabled = false;

        StartDate = entry.StartTime.ToDate();
        StartTime = entry.StartTime.ToTime();
        EndDate = entry.EndTime.ToDate();
        EndTime = entry.EndTime.ToTime();
        Callsign = entry.Callsign;
        Name = entry.Name;
        Comments = entry.Comments;
    }

    [RelayCommand]
    private void Loaded()
    {
        // TODO: is not called when "Clear" button is pressed
        _ignoreUpdates = false;
    }

    [RelayCommand]
    protected override void DataChanged()
    {
        DisableStartTime();
    }

    private void DisableStartTime()
    {
        if (_ignoreUpdates)
        {
            return;
        }

        ShouldUpdateStartTime = false;
        _dataChanged = true;
    }

    private void DisableEndTime()
    {
        if (_ignoreUpdates)
        {
            return;
        }

        ShouldUpdateEndTime = false;
        _dataChanged = true;
    }

    partial void OnStartDateChanged(DateTimeOffset value) => DataChanged();
    partial void OnStartTimeChanged(TimeSpan value) => DataChanged();
    partial void OnEndDateChanged(DateTimeOffset value) => DisableEndTime();
    partial void OnEndTimeChanged(TimeSpan value) => DisableEndTime();

    [RelayCommand]
    private void ShouldUpdateStartTimeChanged(bool update)
    {
        _ignoreUpdates = true;

        if (update)
        {
            UpdateStartTime();
        }

        _ignoreUpdates = false;
    }

    [RelayCommand]
    private void ShouldUpdateEndTimeChanged(bool update)
    {
        if (update)
        {
            UpdateEndTime();
        }
    }

    private void UpdateTime(object? state)
    {
        DispatcherQueue.TryEnqueue(() =>
        {
            if (ShouldUpdateStartTime)
            {
                UpdateStartTime();
            }

            if (ShouldUpdateEndTime)
            {
                UpdateEndTime();
            }
        });
    }

    private void UpdateStartTime()
    {
        _ignoreUpdates = true;
        var now = DateTime.Now;
        StartDate = now.ToDate();
        StartTime = now.ToTime();
        _ignoreUpdates = false;
    }

    private void UpdateEndTime()
    {
        _ignoreUpdates = true;
        var now = DateTime.Now;
        EndDate = now.ToDate();
        EndTime = now.ToTime();
        _ignoreUpdates = false;
    }
}
