using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace LinuxInstaller.Converters;

public class DiskSpaceToWidthConverter : IMultiValueConverter
{
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values.Count == 3 &&
            values[0] is ulong blockSize &&
            values[1] is ulong totalDiskSize &&
            values[2] is double containerWidth)
        {
            if (totalDiskSize == 0 || containerWidth == 0) return 0.0;

            return (blockSize / (double)totalDiskSize) * containerWidth;
        }
        return 0.0;
    }

    public object?[]? ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
