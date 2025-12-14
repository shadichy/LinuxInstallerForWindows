using Avalonia;
using Avalonia.Data.Converters;
using LinuxInstaller.Models;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace LinuxInstaller.Converters;

public class ChartBlockBorderThicknessConverter : IMultiValueConverter
{
    public object Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values.Count == 2 && values[0] is ChartSpace currentItem && values[1] is ChartSpace selectedItem)
        {
            return currentItem == selectedItem ? new Thickness(4) : new Thickness(1);
        }
        return new Thickness(1);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
