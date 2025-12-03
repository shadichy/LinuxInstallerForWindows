using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using LinuxInstaller.Converters;

namespace LinuxInstaller.UserControls;

public partial class DistroListItemControl : UserControl
{
    // TODO: The 'IsSelected' property should be properly bound and updated when the item is clicked.
    // This typically involves handling input events (e.g., PointerPressed) on the control.
    // Also, the IconSource should be bound to an actual image path or resource.
    public static readonly StyledProperty<IImage> IconSourceProperty =
        AvaloniaProperty.Register<DistroListItemControl, IImage>(nameof(IconSource));

    public static readonly StyledProperty<string> DistroNameProperty =
        AvaloniaProperty.Register<DistroListItemControl, string>(nameof(DistroName));

    public static readonly StyledProperty<bool> IsSelectedProperty =
        AvaloniaProperty.Register<DistroListItemControl, bool>(nameof(IsSelected));

    public IImage IconSource
    {
        get => GetValue(IconSourceProperty);
        set => SetValue(IconSourceProperty, value);
    }

    public string DistroName
    {
        get => GetValue(DistroNameProperty);
        set => SetValue(DistroNameProperty, value);
    }

    public bool IsSelected
    {
        get => GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
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
