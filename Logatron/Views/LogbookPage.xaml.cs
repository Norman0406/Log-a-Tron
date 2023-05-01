using Logatron.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace Logatron.Views;

public sealed partial class LogbookPage : Page
{
    public LogbookPageViewModel ViewModel
    {
        get;
    }

    public LogbookPage()
    {
        ViewModel = App.GetService<LogbookPageViewModel>();
        InitializeComponent();
    }
}
