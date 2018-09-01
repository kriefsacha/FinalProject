using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace Client.Converters
{
    /// <summary>
    /// Converter that gives us an Random plane image
    /// </summary>
    public class IntToImageConverter: IValueConverter
    {       
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value is int number ? new BitmapImage(new Uri(string.Format("ms-appx:///Assets/Planes/{0}.png", number))) : null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
