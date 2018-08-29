using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Client.Converters
{
    class StationToLocationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var station = (int)value;
            var topOrLeft = (string)parameter;
            if (topOrLeft == "top")
            {
                if (station >= 1 && station <= 4)
                {
                    return 0;
                }
                if (station == 5 || station == 8)
                {
                    return 220;
                }
                if (station == 6 || station == 7)
                {
                    return 380;
                }
            }
            else
            {
                if (station == 1)
                {
                    return 630;
                }
                if (station == 2)
                {
                    return 420;
                }
                if (station == 3)
                {
                    return 210;
                }
                if (station == 4)
                {
                    return 0;
                }
                if (station == 5)
                {
                    return 0;
                }
                if (station == 6)
                {
                    return 0;
                }
                if (station == 7)
                {
                    return 300;
                }
                if (station == 8)
                {
                    return 300;
                }
            }
            return null;
        }
  
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
