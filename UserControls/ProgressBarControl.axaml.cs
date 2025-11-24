using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml;
using System;
using System.Globalization;

namespace LinuxInstaller.UserControls;

public class ProgressConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double progressValue && targetType == typeof(double))
        {
            // Assuming the progress value is between 0 and 100
            // and the control's actual width can be obtained from its parent or itself.
            // For simplicity, let's assume a fixed width for now or bind to ActualWidth
            // In a real scenario, you'd bind to the parent's ActualWidth or a specific target width.
            // This converter would then return (progressValue / 100) * ActualWidth.
            // For now, returning the raw value as a placeholder.
            return progressValue;
        }
        return 0.0;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}


public partial class ProgressBarControl : UserControl
{
    public static readonly DirectProperty<ProgressBarControl, double> ValueProperty =
        AvaloniaProperty.RegisterDirect<ProgressBarControl, double>(
            nameof(Value),
            o => o.Value,
            (o, v) => o.Value = v);

    private double _value;
    public double Value
    {
        get => _value;
        set => SetAndRaise(ValueProperty, ref _value, value);
    }

    public ProgressBarControl()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
