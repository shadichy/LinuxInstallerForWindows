using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace LinuxInstaller.Converters;

public class PercentageToAngleConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double progress)
        {
            // 0-100 -> 0-360
            return (progress / 100.0) * 360.0;
        }
        return 0.0;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}