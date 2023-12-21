using System;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace GameBarClock.Helpers
{
    public class DefaultDateTimeFormatStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string format && format == App.DefaultDateTimeFormatString)
            {
                return string.Empty;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is string format && format == string.Empty)
            {
                return App.DefaultDateTimeFormatString;
            }
            return value;
        }
    }

    public class GridSizeToRectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Rect rect = new Rect(0, 0, 0, 0);
            if (value is Grid grid)
            {
                rect = new Rect(0, 0, grid.ActualWidth, grid.ActualHeight);
            }
            return rect;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}
