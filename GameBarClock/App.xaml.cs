using GameBarClock.Properties;
using GameBarClock.Widgets;
using Microsoft.Gaming.XboxGameBar;
using System;
using System.Collections.Generic;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.Resources.Core;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace GameBarClock
{
    sealed partial class App : Application
    {
        private XboxGameBarWidget clockWidget = null;
        private XboxGameBarWidget settingsWidget = null;

        public static string DefaultDateTimeFormatString = "yyyy-MM-dd HH:mm:ss";
        public static string DefaultFontFamily = "Segoe UI";

        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            if (Settings.Default.Language == "System")
            {
                ResourceContext.ResetGlobalQualifierValues(new List<string> { "Language" });
            }
            else
            {
                ResourceContext.SetGlobalQualifierValue("Language", Settings.Default.Language);
            }
        }

        protected override void OnActivated(IActivatedEventArgs args)
        {
            XboxGameBarWidgetActivatedEventArgs widgetArgs = null;
            if (args.Kind == ActivationKind.Protocol)
            {
                var protocolArgs = args as IProtocolActivatedEventArgs;
                string scheme = protocolArgs.Uri.Scheme;
                if (scheme.Equals("ms-gamebarwidget"))
                {
                    widgetArgs = args as XboxGameBarWidgetActivatedEventArgs;
                }
            }
            if (widgetArgs != null && widgetArgs.IsLaunchActivation)
            {
                var rootFrame = new Frame();
                rootFrame.NavigationFailed += OnNavigationFailed;
                Window.Current.Content = rootFrame;

                if (widgetArgs.AppExtensionId == "Clock")
                {
                    clockWidget = new XboxGameBarWidget(
                        widgetArgs,
                        Window.Current.CoreWindow,
                        rootFrame);
                    rootFrame.Navigate(typeof(ClockWidget), clockWidget);
                    clockWidget.SettingsClicked += async (s, e) => await s.ActivateSettingsAsync();

                    Window.Current.Closed += OnWidgetMainClosed;
                }
                else if (widgetArgs.AppExtensionId == "Settings")
                {
                    settingsWidget = new XboxGameBarWidget(
                        widgetArgs,
                        Window.Current.CoreWindow,
                        rootFrame);
                    rootFrame.Navigate(typeof(SettingsWidget), settingsWidget);

                    Window.Current.Closed += OnWidgetSettingsClosed;
                }
                else
                {
                    return;
                }

                Window.Current.Activate();
            }
        }

        private void OnWidgetMainClosed(object sender, Windows.UI.Core.CoreWindowEventArgs e)
        {
            clockWidget = null;
            Window.Current.Closed -= OnWidgetMainClosed;
        }

        private void OnWidgetSettingsClosed(object sender, Windows.UI.Core.CoreWindowEventArgs e)
        {
            settingsWidget = null;
            Window.Current.Closed -= OnWidgetSettingsClosed;
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                rootFrame = new Frame();
                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: 从之前挂起的应用程序加载状态
                }

                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    var view = ApplicationView.GetForCurrentView();
                    view.SetPreferredMinSize(new Size(384, 96));
                    view.TryResizeView(new Size(384, 296));
                    view.TitleBar.BackgroundColor = Colors.Transparent;
                    view.TitleBar.ButtonBackgroundColor = Colors.Transparent;
                    view.TitleBar.ButtonHoverBackgroundColor = Colors.Transparent;
                    view.TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
                    view.TitleBar.ButtonPressedBackgroundColor = Colors.Transparent;
                    view.TitleBar.InactiveBackgroundColor = Colors.Transparent;
                    CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }

                Window.Current.Activate();
            }
        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            clockWidget = null;
            settingsWidget = null;

            deferral.Complete();
        }
    }
}
