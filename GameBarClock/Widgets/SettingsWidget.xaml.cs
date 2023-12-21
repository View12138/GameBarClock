using GameBarClock.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GameBarClock.Widgets
{
    public sealed partial class SettingsWidget : Page
    {
        public SettingsWidget()
        {
            this.InitializeComponent();
            this.DataContext = new SettingsViewModel();
        }

        private void DateTimeFormat_GotFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(DateTimeFormat.Text))
            {
                DateTimeFormat.Text = App.DefaultDateTimeFormatString;
                DateTimeFormat.SelectAll();
            }
        }

        private void DateTimeFormat_LostFocus(object sender, RoutedEventArgs e)
        {
            if (DateTimeFormat.Text == App.DefaultDateTimeFormatString)
            {
                DateTimeFormat.Text = string.Empty;
            }
        }
    }
}
