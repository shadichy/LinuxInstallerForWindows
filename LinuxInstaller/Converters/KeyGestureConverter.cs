using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Input;

namespace LinuxInstaller.Converters;

public class KeyGestureConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is KeyGesture gesture)
        {
            return gesture.ToString();
        }
        return string.Empty;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
