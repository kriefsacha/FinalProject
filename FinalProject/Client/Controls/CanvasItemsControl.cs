using Client.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace Client.Controls
{
    public class CanvasItemsControl : ItemsControl
    {
       

        //public static int GetStation(DependencyObject obj)
        //{
        //    return (int)obj.GetValue(StationProperty);
        //}

        //public static void SetStation(DependencyObject obj, int value)
        //{
        //    obj.SetValue(StationProperty, value);
        //}

        public static readonly DependencyProperty StationProperty =
            DependencyProperty.RegisterAttached("StationNumber", typeof(int), typeof(CanvasItemsControl), new PropertyMetadata(0, OnStationProperty));

        //public static readonly DependencyProperty StationProperty1 =
        //   DependencyProperty.RegisterAttached("PreviousStationNumber", typeof(int), typeof(CanvasItemsControl), new PropertyMetadata(0, OnStationProperty));


        private static void OnStationProperty(DependencyObject Plane, DependencyPropertyChangedEventArgs stationNumber)
        {
            //d is the element
            //e is the station we have old and new
            FrameworkElement contentControl = (FrameworkElement)Plane;

            var from = (int)stationNumber.OldValue;
            var to = (int)stationNumber.NewValue;


            var toX = GetStationLeft(to);
            var toY = GetStationTop(to);



            if (to != 0)
            {
                //var toX = (to % 3) * 200 + 100;
                //var toY = (to / 3) * 100 + 100;
                Storyboard moveStoeyBoard = new Storyboard();
                DoubleAnimation leftDoubleAmnimation = new DoubleAnimation();
                DoubleAnimation topDoubleAmnimation = new DoubleAnimation();
                leftDoubleAmnimation.To = toX;
                leftDoubleAmnimation.Duration = new Duration(TimeSpan.FromMilliseconds(1000));
                topDoubleAmnimation.To = toY;
                topDoubleAmnimation.Duration = new Duration(TimeSpan.FromMilliseconds(1000));
                Storyboard.SetTarget(leftDoubleAmnimation, contentControl);
                Storyboard.SetTarget(topDoubleAmnimation, contentControl);
                Storyboard.SetTargetProperty(leftDoubleAmnimation, "(Canvas.Left)");
                Storyboard.SetTargetProperty(topDoubleAmnimation, "(Canvas.Top)");
                moveStoeyBoard.Children.Add(leftDoubleAmnimation);
                moveStoeyBoard.Children.Add(topDoubleAmnimation);
                moveStoeyBoard.Begin();
            }
            else
            {
                contentControl.Visibility = Visibility.Collapsed; // how to take off the list??
            }
        }

        protected override void PrepareContainerForItemOverride(
                            DependencyObject element,
                            object item)
        {
            //var converter = new StationToLocationConverter();
            Binding binding = new Binding() { Path = new PropertyPath("StationNumber") };

            //Binding binding1 = new Binding() { Path = new PropertyPath("PreviousStationNumber") };

            FrameworkElement contentControl = (FrameworkElement)element;
            contentControl.SetBinding(StationProperty, binding);
            //contentControl.SetBinding(StationProperty, binding1);

            base.PrepareContainerForItemOverride(element, item);
        }

        private static int GetStationTop(int StationNumber)
        {
            if (StationNumber >= 1 && StationNumber <= 4)
            {
                return 0;
            }
            else if (StationNumber == 5 || StationNumber == 8)
            {
                return 220;
            }
            if (StationNumber == 6 || StationNumber == 7)
            {
                return 380;
            }
            else { return 0; }
        }
        private static int GetStationLeft(int StationNumber)
        {
            if (StationNumber == 1)
            {
                return 630;
            }
            if (StationNumber == 2)
            {
                return 420;
            }
            if (StationNumber == 3)
            {
                return 210;
            }
            if (StationNumber == 4)
            {
                return 0;
            }
            if (StationNumber == 5)
            {
                return 0;
            }
            if (StationNumber == 6)
            {
                return 0;
            }
            if (StationNumber == 7)
            {
                return 300;
            }
            if (StationNumber == 8)
            {
                return 300;
            }
            else { return 0; }
        }

    }
}
