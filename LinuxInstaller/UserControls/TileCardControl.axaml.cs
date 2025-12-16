using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Windows.Input;

namespace LinuxInstaller.UserControls;

public partial class TileCardControl : UserControl
{
    public TileCardControl()
    {
        InitializeComponent();
    }

    public static readonly StyledProperty<string?> IconProperty =
        AvaloniaProperty.Register<TileCardControl, string?>(nameof(Icon));

    public static readonly StyledProperty<string?> TitleProperty =
        AvaloniaProperty.Register<TileCardControl, string?>(nameof(Title));

    public static readonly StyledProperty<IEnumerable<KeyValuePair<string, string>>?> DescriptionsProperty =
        AvaloniaProperty.Register<TileCardControl, IEnumerable<KeyValuePair<string, string>>?>(nameof(Descriptions));

    public static readonly StyledProperty<string?> ActionIconProperty =
        AvaloniaProperty.Register<TileCardControl, string?>(nameof(ActionIcon));

    public static readonly StyledProperty<string?> ActionLabelProperty =
        AvaloniaProperty.Register<TileCardControl, string?>(nameof(ActionLabel));

    public static readonly StyledProperty<ICommand?> ActionCallbackProperty =
        AvaloniaProperty.Register<TileCardControl, ICommand?>(nameof(ActionCallback));

    public static readonly StyledProperty<object?> ExtraContentProperty =
        AvaloniaProperty.Register<PageHeaderControl, object?>(nameof(ExtraContent));

    public string? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public string? Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public IEnumerable<KeyValuePair<string, string>>? Descriptions
    {
        get => GetValue(DescriptionsProperty);
        set => SetValue(DescriptionsProperty, value);
    }

    public string? ActionIcon
    {
        get => GetValue(ActionIconProperty);
        set => SetValue(ActionIconProperty, value);
    }

    public string? ActionLabel
    {
        get => GetValue(ActionLabelProperty);
        set => SetValue(ActionLabelProperty, value);
    }

    public ICommand? ActionCallback
    {
        get => GetValue(ActionCallbackProperty);
        set => SetValue(ActionCallbackProperty, value);
    }

    public object? ExtraContent
    {
        get => GetValue(ExtraContentProperty);
        set => SetValue(ExtraContentProperty, value);
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
