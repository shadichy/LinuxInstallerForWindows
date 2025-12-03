using Avalonia.Data.Converters;
using Avalonia.Media;
using Avalonia;
using System;
using System.Globalization;

namespace LinuxInstaller.Converters
{
    public class BoolToBrushConverter : IValueConverter
    {
        public IBrush TrueBrush { get; set; } = Brushes.LightBlue; // Default brush when true
        public IBrush FalseBrush { get; set; } = Brushes.Transparent; // Default brush when false

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? TrueBrush : FalseBrush;
            }
            return FalseBrush; // Default if not a boolean
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
