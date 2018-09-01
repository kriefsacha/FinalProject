using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Animation;

namespace Client.Controls
{
    public class CanvasItemsControl : ItemsControl
    {
        public static readonly DependencyProperty StationProperty =
            DependencyProperty.RegisterAttached("StationNumber", typeof(int), typeof(CanvasItemsControl), new PropertyMetadata(0, OnStationProperty));

        /// <summary>
        /// When the station number of a plane changes , fire a animation
        /// </summary>
        /// <param name="Plane"></param>
        /// <param name="stationNumber"></param>
        private static void OnStationProperty(DependencyObject Plane, DependencyPropertyChangedEventArgs stationNumber)
        {
            FrameworkElement contentControl = (FrameworkElement)Plane;

            //New value of the station number
            var to = (int)stationNumber.NewValue;

            var toX = GetStationLeft(to);
            var toY = GetStationTop(to);

            if (to != 0)
            {
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
                contentControl.Visibility = Visibility.Collapsed;
        }

        protected override void PrepareContainerForItemOverride(
                            DependencyObject element,
                            object item)
        {
            Binding binding = new Binding() { Path = new PropertyPath("StationNumber") };

            FrameworkElement contentControl = (FrameworkElement)element;
            contentControl.SetBinding(StationProperty, binding);

            base.PrepareContainerForItemOverride(element, item);
        }

        /// <summary>
        /// Returns the top of a station
        /// </summary>
        /// <param name="StationNumber">Number of the station</param>
        /// <returns>Top</returns>
        private static int GetStationTop(int StationNumber)
        {
            if (StationNumber >= 1 && StationNumber <= 4)
                return 0;
            else if (StationNumber == 5 || StationNumber == 8)
                return 220;
            else if (StationNumber == 6 || StationNumber == 7)
                return 380;
            else
                return 0;
        }

        /// <summary>
        /// Returns the left of a station
        /// </summary>
        /// <param name="StationNumber">Number of the station</param>
        /// <returns>Left</returns>
        private static int GetStationLeft(int StationNumber)
        {
            switch (StationNumber)
            {
                case 1: return 630;
                case 2: return 420;
                case 3: return 210;
                case 4: return 0;
                case 5: return 0;
                case 6: return 0;
                case 7: return 300;
                case 8: return 300;
                default: return 0;
            }
        }
    }
}
