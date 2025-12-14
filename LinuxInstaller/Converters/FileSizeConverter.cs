using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;

namespace LinuxInstaller.Converters;

public class FileSizeConverter : IValueConverter
{
    static readonly uint C = 1024;

    public static string ToUnit(ulong bytes)
    {
        List<string> scaleUnits = ["Bytes", "KB", "MB", "GB", "TB", "PB"];

        int i = 0;
        ulong scale = 1;
        while (i < scaleUnits.Count)
        {
            ulong nextScale = scale * C;
            if (bytes < nextScale) break;
            scale = nextScale;
            i++;
        }

        return $"{bytes / (double)(scale):F1} {scaleUnits[i]}";
    }

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is ulong sizeInBytes) return ToUnit(sizeInBytes);
        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
