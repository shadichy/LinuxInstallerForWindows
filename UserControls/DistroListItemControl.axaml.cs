using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using System;
using System.Globalization;

namespace LinuxInstaller.UserControls;

public class BoolToBrushConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool b && targetType == typeof(IBrush))
        {
            return b ? Brushes.LightBlue : Brushes.Transparent; // Placeholder brushes
        }
        return Brushes.Transparent;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public partial class DistroListItemControl : UserControl
{
    public static readonly DirectProperty<DistroListItemControl, ImageSource> IconSourceProperty =
        AvaloniaProperty.RegisterDirect<DistroListItemControl, ImageSource>(
            nameof(IconSource),
            o => o.IconSource,
            (o, v) => o.IconSource = v);

    public static readonly DirectProperty<DistroListItemControl, string> DistroNameProperty =
        AvaloniaProperty.RegisterDirect<DistroListItemControl, string>(
            nameof(DistroName),
            o => o.DistroName,
            (o, v) => o.DistroName = v);

    public static readonly DirectProperty<DistroListItemControl, bool> IsSelectedProperty =
        AvaloniaProperty.RegisterDirect<DistroListItemControl, bool>(
            nameof(IsSelected),
            o => o.IsSelected,
            (o, v) => o.IsSelected = v);

    private ImageSource _iconSource;
    public ImageSource IconSource
    {
        get => _iconSource;
        set => SetAndRaise(IconSourceProperty, ref _iconSource, value);
    }

    private string _distroName;
    public string DistroName
    {
        get => _distroName;
        set => SetAndRaise(DistroNameProperty, ref _distroName, value);
    }

    private bool _isSelected;
    public bool IsSelected
    {
        get => _isSelected;
        set => SetAndRaise(IsSelectedProperty, ref _isSelected, value);
    }

    public DistroListItemControl()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
