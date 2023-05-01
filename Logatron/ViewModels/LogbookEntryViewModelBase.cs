using CommunityToolkit.Mvvm.ComponentModel;

namespace Logatron.ViewModels;

public abstract partial class LogbookEntryViewModelBase : ViewModelBase
{
    [ObservableProperty]
    private DateTime _startTime = DateTime.Now;

    [ObservableProperty]
    private DateTime _endTime = DateTime.Now;

    [ObservableProperty]
    private string _callsign = string.Empty;

    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private string _comments = string.Empty;
}
