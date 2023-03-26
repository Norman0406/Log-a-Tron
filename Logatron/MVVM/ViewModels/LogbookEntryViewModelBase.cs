using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace Logatron.MVVM.ViewModels
{
    public abstract partial class LogbookEntryViewModelBase : ViewModelBase
    {
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
    }
}
