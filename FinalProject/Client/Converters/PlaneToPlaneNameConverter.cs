﻿using System;
using Windows.UI.Xaml.Data;

namespace Client.Converters
{
    /// <summary>
    /// Converter that will shows a string from a plane
    /// </summary>
    public class PlaneToPlaneNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(value is string plane)
            {
                if (plane != null && plane != "") return " : " + plane;
                else return " : Available ";
            }

            return " : Available";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
