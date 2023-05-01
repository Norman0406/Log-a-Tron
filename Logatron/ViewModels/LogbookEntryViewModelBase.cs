using System.ComponentModel;
using System.Reflection;
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
    protected string _name = string.Empty;

    [ObservableProperty]
    private string _comments = string.Empty;

    protected virtual void DataChanged()
    {
        // NOP
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        var propertiesOfThisClass = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;
        if (typeof(LogbookEntryViewModelBase).GetProperties(propertiesOfThisClass).Any(p => p.Name == e.PropertyName))
        {
            DataChanged();
        }
    }
}
