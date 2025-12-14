using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media;
using LinuxInstaller.Models;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace LinuxInstaller.Converters;

public class ChartBlockBackgroundConverter : IMultiValueConverter
{
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        Application.Current!.TryFindResource("PrimaryBrush", out var primaryBrush);
        SolidColorBrush? primary = primaryBrush as SolidColorBrush;
        Color color = primary!.Color;

        if (values[0] is not ChartSpace currentItem) return new SolidColorBrush(color, 0.36);

        if (values.Count == 2 && values[1] is ChartSpace selectedItem && currentItem == selectedItem)
        {
            Application.Current!.TryFindResource("StripesBrush", out var brush);
            return brush;
        }

        if (currentItem is ChartFreeSpace) return new SolidColorBrush(color, 0.12);

        double opacity = 0.36; // Default for unknown/partition

        if (currentItem is ChartPartition chartPartition && chartPartition.Partition != null)
        {
            switch (chartPartition.Partition.FileSystem.ToUpper())
            {
                case "FAT32":
                    opacity = 0.64;
                    break;
                case "EXFAT":
                    opacity = 0.72;
                    break;
                case "NTFS":
                    opacity = 0.96;
                    break;
                default:
                    opacity = 0.36;
                    break;
            }
        }
        // Create a new SolidColorBrush with the calculated opacity                                                                                                                     
        return new SolidColorBrush(color, opacity);
    }


    public object[]? ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
