using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace Logatron.ViewModels
{
    public abstract partial class LogbookEntryViewModelBase : ObservableObject
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
