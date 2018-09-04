using System;
using Windows.UI.Xaml.Data;

namespace Client.Converters
{
    public class DateTimeToDateFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTime date) return date.ToString("dd MMMM yyyy HH:mm");
            else return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
