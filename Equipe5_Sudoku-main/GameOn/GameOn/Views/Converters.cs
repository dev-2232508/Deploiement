using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace GameOn
{
    public class ReadOnlyToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isReadOnly)
                return isReadOnly ? new SolidColorBrush(Color.FromRgb(230, 230, 230)) : Brushes.White;

            return Brushes.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => Binding.DoNothing;
    }

    public class NullableIntToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Null → empty string, number → string
            return value is int i ? i.ToString() : string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Empty string → null, number string → int
            string str = value as string;
            if (string.IsNullOrWhiteSpace(str)) return null;
            if (int.TryParse(str, out int result)) return result;
            return null;
        }
    }
}