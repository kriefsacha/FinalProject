
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using System;
using Client.ViewModels;

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






