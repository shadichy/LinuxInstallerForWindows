using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media;
using LinuxInstaller.Models;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace LinuxInstaller.Converters;

public class ChartBlockBorderBrushConverter : IMultiValueConverter
{
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values.Count == 2 && values[0] is ChartSpace currentItem && values[1] is ChartSpace selectedItem)
        {
            if (currentItem == selectedItem)
            {
                Application.Current!.TryFindResource("SurfaceVariantBrush", out var brush);
                return brush;
            }
        }

        Application.Current!.TryFindResource("SurfaceContainerLowestBrush", out var defaultBrush);
        return defaultBrush;
    }

    public object[]? ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
