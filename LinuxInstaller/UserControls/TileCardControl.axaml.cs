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

    public static readonly StyledProperty<IEnumerable<KeyValuePair<string, string>>?> ContentProperty =
        AvaloniaProperty.Register<TileCardControl, IEnumerable<KeyValuePair<string, string>>?>(nameof(Content));

    public static readonly StyledProperty<string?> ActionLabelProperty =
        AvaloniaProperty.Register<TileCardControl, string?>(nameof(ActionLabel));

    public static readonly StyledProperty<ICommand?> ActionCallbackProperty =
        AvaloniaProperty.Register<TileCardControl, ICommand?>(nameof(ActionCallback));

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

    public IEnumerable<KeyValuePair<string, string>>? Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
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
    
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
