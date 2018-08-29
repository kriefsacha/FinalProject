using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Client.Controls
{
    public sealed partial class StationsDetails : UserControl
    {
        public StationsDetails()
        {
            this.InitializeComponent();
        }

        private void UserControl_DataContextChanged(Windows.UI.Xaml.FrameworkElement sender, Windows.UI.Xaml.DataContextChangedEventArgs args)
        {
            var t = this;
        }

        private void UserControl_IsEnabledChanged(object sender, Windows.UI.Xaml.DependencyPropertyChangedEventArgs e)
        {
            var t = this;
        }

        private void UserControl_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var t = this;
        }
    }
}
