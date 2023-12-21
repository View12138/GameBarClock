using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.WinUI.Helpers;
using GameBarClock.Models;
using GameBarClock.Properties;
using Microsoft.Graphics.Canvas.Text;
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.ApplicationModel.Resources;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Text;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace GameBarClock.ViewModels
{
    public class SettingsViewModel : ObservableObject
    {
        private UISettings UISettings { get; } = new UISettings();

        private ResourceLoader ResourceLoader => ResourceLoader.GetForViewIndependentUse();
        public CoreDispatcher Dispatcher { get; }

        public SettingsViewModel()
        {
            Dispatcher = Window.Current.Dispatcher;
            Languages.Add(new Property<string>(ResourceLoader.GetString("System"), "System"));
            Languages.Add(new Property<string>("简体中文", "zh-CN"));
            Languages.Add(new Property<string>("English", "en-US"));
            Language = Languages.FirstOrDefault(x => x.Value == Settings.Default.Language) ?? Languages.First();
            Init();
            UISettings.ColorValuesChanged += (s, e)
                => ThemeColors.First(x => x.Key == "ColorDefault").Value = new SolidColorBrush(UISettings.GetColorValue(UIColorType.Accent));
        }

        private void Init()
        {
            ThemeColors.Clear();
            ThemeColors.Add(new KeyProperty<SolidColorBrush>(ResourceLoader.GetString("ColorGreen"), new SolidColorBrush(Color.FromArgb(255, 0, 255, 0)), "ColorGreen"));
            ThemeColors.Add(new KeyProperty<SolidColorBrush>(ResourceLoader.GetString("ColorAmber"), new SolidColorBrush(Color.FromArgb(255, 255, 95, 0)), "ColorAmber"));
            ThemeColors.Add(new KeyProperty<SolidColorBrush>(ResourceLoader.GetString("ColorRed"), new SolidColorBrush(Color.FromArgb(255, 255, 0, 0)), "ColorRed"));
            ThemeColors.Add(new KeyProperty<SolidColorBrush>(ResourceLoader.GetString("ColorWhite"), new SolidColorBrush(Color.FromArgb(255, 222, 248, 255)), "ColorWhite"));
            ThemeColors.Add(new KeyProperty<SolidColorBrush>(ResourceLoader.GetString("ColorViolet"), new SolidColorBrush(Color.FromArgb(255, 184, 122, 245)), "ColorViolet"));
            ThemeColors.Add(new KeyProperty<SolidColorBrush>(ResourceLoader.GetString("ColorBlueGreen"), new SolidColorBrush(Color.FromArgb(255, 0, 255, 255)), "ColorBlueGreen"));
            ThemeColors.Add(new KeyProperty<SolidColorBrush>(ResourceLoader.GetString("ColorYellow"), new SolidColorBrush(Color.FromArgb(255, 255, 227, 0)), "ColorYellow"));
            ThemeColors.Add(new KeyProperty<SolidColorBrush>(ResourceLoader.GetString("ColorDefault"), new SolidColorBrush(UISettings.GetColorValue(UIColorType.Accent)), "ColorDefault"));

            TextAlignments.Clear();
            TextAlignments.Add(new IconProperty<HorizontalAlignment>(ResourceLoader.GetString("AlignmentLeft"), HorizontalAlignment.Left, "\uE8E4"));
            TextAlignments.Add(new IconProperty<HorizontalAlignment>(ResourceLoader.GetString("AlignmentRight"), HorizontalAlignment.Right, "\uE8E2"));
            TextAlignments.Add(new IconProperty<HorizontalAlignment>(ResourceLoader.GetString("AlignmentCenter"), HorizontalAlignment.Center, "\uE8E3"));

            TextWeights.Clear();
            TextWeights.Add(new Property<FontWeight>(nameof(FontWeights.Normal), FontWeights.Normal));
            TextWeights.Add(new Property<FontWeight>(nameof(FontWeights.Black), FontWeights.Black));
            TextWeights.Add(new Property<FontWeight>(nameof(FontWeights.Bold), FontWeights.Bold));
            TextWeights.Add(new Property<FontWeight>(nameof(FontWeights.ExtraBlack), FontWeights.ExtraBlack));
            TextWeights.Add(new Property<FontWeight>(nameof(FontWeights.ExtraBold), FontWeights.ExtraBold));
            TextWeights.Add(new Property<FontWeight>(nameof(FontWeights.ExtraLight), FontWeights.ExtraLight));
            TextWeights.Add(new Property<FontWeight>(nameof(FontWeights.Light), FontWeights.Light));
            TextWeights.Add(new Property<FontWeight>(nameof(FontWeights.Medium), FontWeights.Medium));
            TextWeights.Add(new Property<FontWeight>(nameof(FontWeights.SemiBold), FontWeights.SemiBold));
            TextWeights.Add(new Property<FontWeight>(nameof(FontWeights.SemiLight), FontWeights.SemiLight));
            TextWeights.Add(new Property<FontWeight>(nameof(FontWeights.Thin), FontWeights.Thin));

            TextFamilies.Clear();
            foreach (var textFamily in CanvasTextFormat.GetSystemFontFamilies())
            {
                TextFamilies.Add(new Property<string>(textFamily, textFamily));
            }

            ThemeColor = ThemeColors.FirstOrDefault(x => x.Value.Color.ToHex() == Settings.Default.TextColor) ?? ThemeColors.Last();
            TextAlignment = TextAlignments.FirstOrDefault(x => x.Value == (HorizontalAlignment)Settings.Default.TextAlignment) ?? TextAlignments.Last();
            TextWeight = TextWeights.FirstOrDefault(x => x.Value.Weight == Settings.Default.TextWeight) ?? TextWeights.First();
            TextSize = Settings.Default.TextSize;
            TextFormat = Settings.Default.TextFormat;
            ShowWeek = Settings.Default.ShowWeek;
            TextFamily = TextFamilies.FirstOrDefault(x => x.Value == Settings.Default.TextFamily) ?? new Property<string>(App.DefaultFontFamily, App.DefaultFontFamily);
        }

        private ObservableCollection<Property<string>> languages = new ObservableCollection<Property<string>>();
        private ObservableCollection<KeyProperty<SolidColorBrush>> themeColors = new ObservableCollection<KeyProperty<SolidColorBrush>>();
        private ObservableCollection<Property<HorizontalAlignment>> textAlignments = new ObservableCollection<Property<HorizontalAlignment>>();
        private ObservableCollection<Property<FontWeight>> textWeights = new ObservableCollection<Property<FontWeight>>();
        private ObservableCollection<Property<string>> textFamilies = new ObservableCollection<Property<string>>();

        private Property<string> language;
        private Property<SolidColorBrush> themeColor;
        private Property<HorizontalAlignment> textAlignment;
        private Property<FontWeight> textWeight;
        private int textSize;
        private string textFormat;
        private bool showWeek;
        private Property<string> textFamily;
        private bool showTips;
        private string tips;
        private InfoBarSeverity tipsSeverity = InfoBarSeverity.Informational;

        public ObservableCollection<Property<string>> Languages { get => languages; set => SetProperty(ref languages, value); }
        public ObservableCollection<KeyProperty<SolidColorBrush>> ThemeColors { get => themeColors; set => SetProperty(ref themeColors, value); }
        public ObservableCollection<Property<HorizontalAlignment>> TextAlignments { get => textAlignments; set => SetProperty(ref textAlignments, value); }
        public ObservableCollection<Property<FontWeight>> TextWeights { get => textWeights; set => SetProperty(ref textWeights, value); }
        public ObservableCollection<Property<string>> TextFamilies { get => textFamilies; set => SetProperty(ref textFamilies, value); }



        public Property<string> Language
        {
            get => language; set
            {
                if (language != null)
                {
                    ShowTips = true;
                    Tips = ResourceLoader.GetString("ChangeLanguageTips");
                }
                SetProperty(ref language, value);
                Settings.Default.Language = value.Value;
                Settings.Default.Save();
            }
        }

        public Property<SolidColorBrush> ThemeColor
        {
            get => themeColor; set
            {
                SetProperty(ref themeColor, value);
                Settings.Default.TextColor = value.Value.Color.ToHex();
                Settings.Default.Save();
            }
        }

        public Property<HorizontalAlignment> TextAlignment
        {
            get => textAlignment; set
            {
                SetProperty(ref textAlignment, value);
                Settings.Default.TextAlignment = (int)value.Value;
                Settings.Default.Save();
            }
        }

        public Property<FontWeight> TextWeight
        {
            get => textWeight; set
            {
                SetProperty(ref textWeight, value);
                Settings.Default.TextWeight = value.Value.Weight;
                Settings.Default.Save();
            }
        }

        public int TextSize
        {
            get => textSize; set
            {
                SetProperty(ref textSize, value);
                Settings.Default.TextSize = value;
                Settings.Default.Save();
            }
        }

        public string TextFormat
        {
            get => textFormat; set
            {
                SetProperty(ref textFormat, value);
                Settings.Default.TextFormat = value;
                Settings.Default.Save();
            }
        }

        public bool ShowWeek
        {
            get => showWeek; set
            {
                SetProperty(ref showWeek, value);
                Settings.Default.ShowWeek = value;
                Settings.Default.Save();
            }
        }

        public Property<string> TextFamily
        {
            get => textFamily; set
            {
                SetProperty(ref textFamily, value);
                Settings.Default.TextFamily = value.Value;
                Settings.Default.Save();
            }
        }

        public bool ShowTips { get => showTips; set => SetProperty(ref showTips, value); }
        public string Tips { get => tips; set => SetProperty(ref tips, value); }
        public InfoBarSeverity TipsSeverity { get => tipsSeverity; set => SetProperty(ref tipsSeverity, value); }
    }
}
