using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace Adressverwaltung.Converters
{
    public class PhotoSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Wenn PhotoPath gesetzt -> 100, sonst 0
            return !string.IsNullOrWhiteSpace(value as string) ? 100 : 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
