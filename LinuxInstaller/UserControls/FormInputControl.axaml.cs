using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace LinuxInstaller.UserControls;

public partial class FormInputControl : UserControl
{
    public static readonly DirectProperty<FormInputControl, string> LabelProperty =
        AvaloniaProperty.RegisterDirect<FormInputControl, string>(
            nameof(Label),
            o => o.Label,
            (o, v) => o.Label = v);

    public static readonly DirectProperty<FormInputControl, string> TextProperty =
        AvaloniaProperty.RegisterDirect<FormInputControl, string>(
            nameof(Text),
            o => o.Text,
            (o, v) => o.Text = v);

    public static readonly DirectProperty<FormInputControl, string> WatermarkProperty =
        AvaloniaProperty.RegisterDirect<FormInputControl, string>(
            nameof(Watermark),
            o => o.Watermark,
            (o, v) => o.Watermark = v);

    private string _label;
    public string Label
    {
        get => _label;
        set => SetAndRaise(LabelProperty, ref _label, value);
    }

    private string _text;
    public string Text
    {
        get => _text;
        set => SetAndRaise(TextProperty, ref _text, value);
    }

    private string _watermark;
    public string Watermark
    {
        get => _watermark;
        set => SetAndRaise(WatermarkProperty, ref _watermark, value);
    }

    public FormInputControl()
    {
        InitializeComponent();
        _label = string.Empty;
        _text = string.Empty;
        _watermark = string.Empty;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
