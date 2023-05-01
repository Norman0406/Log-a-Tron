using Logatron.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace Logatron.Views;

public sealed partial class RadioView : UserControl
{
    public RadioViewModel? ViewModel
    {
        get; set;
    }

    public RadioView()
    {
        InitializeComponent();
    }
}
