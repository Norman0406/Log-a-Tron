using Logatron.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Logatron.Views;

public sealed partial class LogbookEntry : UserControl
{
    public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty.Register("ViewModel", typeof(LogbookEntryEditViewModel), typeof(LogbookEntry), new PropertyMetadata(null));

    public LogbookEntryEditViewModel ViewModel
    {
        get => (LogbookEntryEditViewModel)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }

    public LogbookEntry()
    {
        // We cannot use {x:Bind} for the commands, since it cannot handle a change in view model during runtime, which is what
        // happens when we double click on a list item. Therefore, we need {Binding}, which requires a DataContext.
        DataContext = this;

        InitializeComponent();
    }
}
