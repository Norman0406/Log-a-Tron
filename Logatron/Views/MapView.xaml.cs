using Mapsui.UI;
using Mapsui;
using Mapsui.UI.Wpf;
using System.Windows.Controls;

namespace Logatron.Views
{
    /// <summary>
    /// Interaction logic for MapView.xaml
    /// </summary>
    public partial class MapView : UserControl
    {
        public MapView()
        {
            InitializeComponent();

            MapControl.Map?.Layers.Add(Mapsui.Utilities.OpenStreetMap.CreateTileLayer());
        }
    }
}
