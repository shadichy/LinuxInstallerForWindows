using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml;
using System;
using System.Globalization;

namespace LinuxInstaller.UserControls;

// TODO: This converter is a placeholder. A real implementation should properly calculate the width of the progress bar
// based on the parent container's width and the provided percentage value.
// It should be registered as a static resource in App.axaml to be used application-wide.
public class ProgressConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double progressValue && targetType == typeof(double))
        {
            // This is incorrect for a real implementation.
            // It should be bound to the parent's ActualWidth and the value should be a percentage.
            // For example: `return (progressValue / 100.0) * parentWidth;`
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
