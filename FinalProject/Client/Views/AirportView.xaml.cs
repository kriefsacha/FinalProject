using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;

namespace Client
{
    public sealed partial class AirportView : Page
    {
        public AirportView()
        {
            this.InitializeComponent();

            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen;
        }
    }
}






