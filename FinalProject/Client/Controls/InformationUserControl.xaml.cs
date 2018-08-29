using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Client.Controls
{
    public sealed partial class InformationUserControl : UserControl
    {
        ObservableCollection<Flights> FlightInfo = new ObservableCollection<Flights>();
        
        public InformationUserControl()
        {
            this.InitializeComponent();
            //add get http call
            //proxy.hub on.
            Flights f1 = new Flights { FlightId = "fhdsj112", FlightHour = DateTime.Now };
            Flights f2 = new Flights { FlightId = "fhdfjd12", FlightHour = DateTime.Now };
            Flights f3 = new Flights { FlightId = "fsddsd12", FlightHour = DateTime.Now };
            FlightInfo.Add(f1);
            FlightInfo.Add(f2);
            FlightInfo.Add(f3);
            InformationLV.ItemsSource = FlightInfo;
            //InformationLV.ItemsSource = DAl.int;
        }
    }

    internal class Flights
    {
        public string FlightId { get; set; }
        public DateTime FlightHour { get; set; }
    }
}
