using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace Client.Controls
{
    public sealed partial class FlightsDetails : UserControl
    {
        public static readonly DependencyProperty FutureFlightsProperty = DependencyProperty.Register("FutureFlights", typeof(ObservableCollection<Models.Plane>), typeof(FlightsDetails),
                                            new PropertyMetadata(null));
        public FlightsDetails()
        {
            this.InitializeComponent();
        }

        public ObservableCollection<Models.Plane> FutureFlights
        {
            get { return (ObservableCollection<Models.Plane>)GetValue(FutureFlightsProperty); }

            set { SetValue(FutureFlightsProperty, value); }
        }
    }
}