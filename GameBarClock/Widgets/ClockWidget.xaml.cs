using CommunityToolkit.WinUI.Helpers;
using GameBarClock.Helpers;
using GameBarClock.Properties;
using Microsoft.Gaming.XboxGameBar;
using System;
using Windows.UI.Core;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace GameBarClock.Widgets
{
    public sealed partial class ClockWidget : Page
    {
        XboxGameBarWidget widget;

        public ClockWidget()
        {
            this.InitializeComponent();

            DateTimeText.Text = DateTime.Now.ToString(TextFormat);
            DispatcherTimer dispatcherTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(100)
            };
            dispatcherTimer.Tick += (s, e) =>
            {
                DateTimeText.Text = DateTime.Now.ToString(TextFormat);
                if (ShowWeek == Visibility.Visible)
                {
                    WeekText.Text = DateTime.Now.DayOfWeek.ToLocal();
                }
            };
            dispatcherTimer.Start();
            Settings.Default.PropertyChanged += async (s, e) =>
            {
                if (e.PropertyName == nameof(Settings.Default.TextAlignment))
                {
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => TextAlignment = (HorizontalAlignment)Settings.Default.TextAlignment);
                }
                if (e.PropertyName == nameof(Settings.Default.TextWeight))
                {
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => TextWeight = new FontWeight() { Weight = Settings.Default.TextWeight });
                }
                if (e.PropertyName == nameof(Settings.Default.TextColor))
                {
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => TextColor = new SolidColorBrush(Settings.Default.TextColor.ToColor()));
                }
                if (e.PropertyName == nameof(Settings.Default.TextSize))
                {
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => TextSize = Settings.Default.TextSize);
                }
                if (e.PropertyName == nameof(Settings.Default.TextFormat))
                {
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => TextFormat = Settings.Default.TextFormat);
                }
                if (e.PropertyName == nameof(Settings.Default.ShowWeek))
                {
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => ShowWeek = Settings.Default.ShowWeek ? Visibility.Visible : Visibility.Collapsed);
                }
                if (e.PropertyName == nameof(Settings.Default.TextFamily))
                {
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => TextFamily = Settings.Default.TextFamily);
                }
            };
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is XboxGameBarWidget)
            {
                widget = e.Parameter as XboxGameBarWidget;
                widget.GameBarDisplayModeChanged += async (sender, args) =>
                {
                    if (widget.Pinned && widget.ClickThroughEnabled && widget.GameBarDisplayMode == XboxGameBarDisplayMode.PinnedOnly)
                    {
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Opacity = widget.RequestedOpacity);
                    }
                    else
                    {
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Opacity = 1);
                    }
                };
            }
        }


        public SolidColorBrush TextColor
        {
            get { return (SolidColorBrush)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }
        public static readonly DependencyProperty TextColorProperty =
            DependencyProperty.Register(nameof(TextColor), typeof(SolidColorBrush), typeof(ClockWidget), new PropertyMetadata(new SolidColorBrush(Settings.Default.TextColor.ToColor())));

        public HorizontalAlignment TextAlignment
        {
            get { return (HorizontalAlignment)GetValue(TextAlignmentProperty); }
            set { SetValue(TextAlignmentProperty, value); }
        }
        public static readonly DependencyProperty TextAlignmentProperty =
            DependencyProperty.Register(nameof(TextAlignment), typeof(HorizontalAlignment), typeof(ClockWidget), new PropertyMetadata((HorizontalAlignment)Settings.Default.TextAlignment));

        public FontWeight TextWeight
        {
            get { return (FontWeight)GetValue(TextWeightProperty); }
            set { SetValue(TextWeightProperty, value); }
        }
        public static readonly DependencyProperty TextWeightProperty =
            DependencyProperty.Register(nameof(TextWeight), typeof(FontWeight), typeof(ClockWidget), new PropertyMetadata(new FontWeight() { Weight = Settings.Default.TextWeight }));

        public int TextSize
        {
            get { return (int)GetValue(TextSizeProperty); }
            set { SetValue(TextSizeProperty, value); }
        }
        public static readonly DependencyProperty TextSizeProperty =
            DependencyProperty.Register(nameof(TextSize), typeof(int), typeof(ClockWidget), new PropertyMetadata(Settings.Default.TextSize));

        public string TextFormat
        {
            get { return (string)GetValue(TextFormatProperty); }
            set { SetValue(TextFormatProperty, value); }
        }
        public static readonly DependencyProperty TextFormatProperty =
            DependencyProperty.Register(nameof(TextFormat), typeof(string), typeof(ClockWidget), new PropertyMetadata(Settings.Default.TextFormat));

        public Visibility ShowWeek
        {
            get { return (Visibility)GetValue(ShowWeekProperty); }
            set { SetValue(ShowWeekProperty, value); }
        }
        public static readonly DependencyProperty ShowWeekProperty =
            DependencyProperty.Register(nameof(ShowWeek), typeof(Visibility), typeof(ClockWidget), new PropertyMetadata(Settings.Default.ShowWeek ? Visibility.Visible : Visibility.Collapsed));

        public string TextFamily
        {
            get { return (string)GetValue(TextFamilyProperty); }
            set { SetValue(TextFamilyProperty, value); }
        }
        public static readonly DependencyProperty TextFamilyProperty =
            DependencyProperty.Register(nameof(TextFamily), typeof(string), typeof(ClockWidget), new PropertyMetadata(Settings.Default.TextFamily));




    }
}
