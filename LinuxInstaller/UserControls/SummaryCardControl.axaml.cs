using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using System.Collections.ObjectModel;

namespace LinuxInstaller.UserControls;

public partial class SummaryCardControl : UserControl
{
    public static readonly StyledProperty<Geometry> IconDataProperty =
        AvaloniaProperty.Register<SummaryCardControl, Geometry>(nameof(IconData));

    public static readonly StyledProperty<string> TitleProperty =
        AvaloniaProperty.Register<SummaryCardControl, string>(nameof(Title));

    public Geometry IconData
    {
        get => GetValue(IconDataProperty);
        set => SetValue(IconDataProperty, value);
    }

    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public SummaryCardControl()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
