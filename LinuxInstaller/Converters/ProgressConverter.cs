using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace LinuxInstaller.Converters
{
    public class ProgressConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is double progressValue && parameter is double maxValue)
            {
                // Convert progress to a percentage or a fraction for UI binding (e.g., width)
                // This example converts to a fraction between 0 and 1
                return progressValue / maxValue;
            }
            return 0.0; // Default to 0 if conversion fails
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
