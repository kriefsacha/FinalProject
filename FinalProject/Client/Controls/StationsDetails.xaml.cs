using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Client.Controls
{
    public sealed partial class StationsDetails : UserControl
    {
        public StationsDetails()
        {
            this.InitializeComponent();
        }

        public static readonly DependencyProperty StationsProperty = DependencyProperty.Register("Stations",typeof(ObservableCollection<Models.Station>) ,typeof(StationsDetails) ,
                                                    new PropertyMetadata(null));

        public ObservableCollection<Models.Station> Stations
        {
            get { return (ObservableCollection<Models.Station>)GetValue(StationsProperty); }

            set { SetValue(StationsProperty, value); }
        }
    }
}
