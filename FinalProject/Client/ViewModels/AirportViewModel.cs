using Client.Enums;
using Client.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Client.ViewModels
{

    public class AirportViewModel
    {

        private ObservableCollection<Plane> planes;

        public ObservableCollection<Plane> Planes
        {
            get { return planes; }
            set { planes = value; }
        }

        public AirportViewModel()
        {

            Planes = new ObservableCollection<Plane>();

            Plane plane1 = new Plane();
            plane1.StationNumber = 1;
            //plane1.PreviousStationNumber = 1;

            Plane plane2 = new Plane();
            plane2.StationNumber = 2;

            Plane plane3 = new Plane();
            plane3.StationNumber = 3;

            Plane plane4 = new Plane();
            plane4.StationNumber = 4;

            Plane plane5 = new Plane();
            plane5.StationNumber = 5;

            Plane plane6 = new Plane();
            plane6.StationNumber = 6;

            Plane plane7 = new Plane();
            plane7.StationNumber = 7;

            Plane plane8 = new Plane();
            plane8.StationNumber = 8;

            Planes.Add(plane1);
            //Planes.Add(plane2);
            //Planes.Add(plane3);
            //Planes.Add(plane4);
            //Planes.Add(plane5);
            //Planes.Add(plane6);
            //Planes.Add(plane7);
            //Planes.Add(plane8);


            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            var task = Task.Delay(1000).ContinueWith(t =>
            {
                Planes[0].StationNumber = 1;
            }, scheduler);
            for (int i = 1; i < 10; i++)
            {
                var tmp = i;
                task = task.ContinueWith(_ => Task.Delay(2000).ContinueWith(__ =>
                {
                    Planes[0].StationNumber = tmp;
                }, scheduler), scheduler).Unwrap();
            }


        }
    }


}
