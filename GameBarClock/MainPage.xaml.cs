using GameBarClock.Widgets;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GameBarClock
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Window.Current.SetTitleBar(TouchTitleBar);
            MainFrame.Navigate(typeof(TipsWidget));
        }

        bool settingView = false;

        private void Settings_Click(object s, RoutedEventArgs e)
        {
            if (!settingView)
            {
                settingView = true;
                if (MainFrame.CanGoForward)
                {
                    MainFrame.GoForward();
                }
                else
                {
                    MainFrame.Navigate(typeof(SettingsWidget));
                }
                HomeBackIn.Begin();
                HomeSettingsOut.Begin();
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (settingView && MainFrame.CanGoBack)
            {
                settingView = false;
                MainFrame.GoBack();
                HomeBackOut.Begin();
                HomeSettingsIn.Begin();
            }
        }
    }
}
