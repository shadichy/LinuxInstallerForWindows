using Avalonia.Data.Converters;
using LinuxInstaller.Models;
using System;
using System.Globalization;

namespace LinuxInstaller.Converters;

public class FileSystemValueConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is FileSystem fs) return FS.ToString(fs);
        return null;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string str) return FS.ToFileSystem(str);
        return null;
    }
}
