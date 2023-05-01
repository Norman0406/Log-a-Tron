using Logatron.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace Logatron.Views;

public sealed partial class LogbookView : UserControl
{
    public LogbookViewModel? ViewModel
    {
        get; set;
    }

    public LogbookView()
    {
        InitializeComponent();
    }
}
