using Avalonia;
using Avalonia.Controls;
using LinuxInstaller.Models;
using System.Windows.Input;
using System.ComponentModel; // Required for INotifyPropertyChanged
using System.Runtime.CompilerServices; // Required for CallerMemberName

namespace LinuxInstaller.UserControls;

public partial class ButtonControl : UserControl, INotifyPropertyChanged
{
    public ButtonControl()
    {
        InitializeComponent();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    // Content, Command, and CommandParameter properties
    public static readonly StyledProperty<object?> ContentProperty =
        AvaloniaProperty.Register<ButtonControl, object?>(nameof(Content));

    public object? Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    public static readonly StyledProperty<ICommand?> CommandProperty =
        AvaloniaProperty.Register<ButtonControl, ICommand?>(nameof(Command));

    public ICommand? Command
    {
        get => GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public static readonly StyledProperty<object?> CommandParameterProperty =
        AvaloniaProperty.Register<ButtonControl, object?>(nameof(CommandParameter));

    public object? CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    // Variant Property
    public static readonly StyledProperty<ButtonVariant> VariantProperty =
        AvaloniaProperty.Register<ButtonControl, ButtonVariant>(nameof(Variant), ButtonVariant.Text);

    public ButtonVariant Variant
    {
        get => GetValue(VariantProperty);
        set => SetValue(VariantProperty, value);
    }

    // Size Property
    public static readonly StyledProperty<ButtonSize> SizeProperty =
        AvaloniaProperty.Register<ButtonControl, ButtonSize>(nameof(Size), ButtonSize.Medium);

    public ButtonSize Size
    {
        get => GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    // Shape Property
    public static readonly StyledProperty<ButtonShape> ShapeProperty =
        AvaloniaProperty.Register<ButtonControl, ButtonShape>(nameof(Shape), ButtonShape.Pill);

    public ButtonShape Shape
    {
        get => GetValue(ShapeProperty);
        set => SetValue(ShapeProperty, value);
    }

    // Combined ButtonStyles Property
    public static readonly StyledProperty<ButtonStyles> ButtonStylesProperty =
        AvaloniaProperty.Register<ButtonControl, ButtonStyles>(nameof(ButtonStyles), new ButtonStyles());

    public ButtonStyles ButtonStyles
    {
        get => GetValue(ButtonStylesProperty);
        set => SetValue(ButtonStylesProperty, value);
    }

    // Is<spec> Getters
    public bool IsText => Variant == ButtonVariant.Text;
    public bool IsOutlined => Variant == ButtonVariant.Outlined;
    public bool IsFilled => Variant == ButtonVariant.Filled;
    public bool IsTonal => Variant == ButtonVariant.Tonal;
    public bool IsElevated => Variant == ButtonVariant.Elevated;

    public bool IsExtraSmall => Size == ButtonSize.ExtraSmall;
    public bool IsSmall => Size == ButtonSize.Small;
    public bool IsMedium => Size == ButtonSize.Medium;
    public bool IsLarge => Size == ButtonSize.Large;
    public bool IsExtraLarge => Size == ButtonSize.ExtraLarge;

    public bool IsPill => Shape == ButtonShape.Pill;
    public bool IsSquare => Shape == ButtonShape.Square;

    static ButtonControl()
    {
        // Register change handlers
        VariantProperty.Changed.AddClassHandler<ButtonControl>((x, e) => x.OnVariantChanged(e));
        SizeProperty.Changed.AddClassHandler<ButtonControl>((x, e) => x.OnSizeChanged(e));
        ShapeProperty.Changed.AddClassHandler<ButtonControl>((x, e) => x.OnShapeChanged(e));
        ButtonStylesProperty.Changed.AddClassHandler<ButtonControl>((x, e) => x.OnButtonStylesChanged(e));
    }

    private void OnButtonStylesChanged(AvaloniaPropertyChangedEventArgs e)
    {
        if (e.NewValue is not ButtonStyles newStyles) return;
        Variant = newStyles.Variant;
        Size = newStyles.Size;
        Shape = newStyles.Shape;
    }

    private void OnVariantChanged(AvaloniaPropertyChangedEventArgs e)
    {
        OnPropertyChanged(nameof(IsText));
        OnPropertyChanged(nameof(IsOutlined));
        OnPropertyChanged(nameof(IsFilled));
        OnPropertyChanged(nameof(IsTonal));
        OnPropertyChanged(nameof(IsElevated));
    }

    private void OnSizeChanged(AvaloniaPropertyChangedEventArgs e)
    {
        OnPropertyChanged(nameof(IsExtraSmall));
        OnPropertyChanged(nameof(IsSmall));
        OnPropertyChanged(nameof(IsMedium));
        OnPropertyChanged(nameof(IsLarge));
        OnPropertyChanged(nameof(IsExtraLarge));
    }

    private void OnShapeChanged(AvaloniaPropertyChangedEventArgs e)
    {
        OnPropertyChanged(nameof(IsPill));
        OnPropertyChanged(nameof(IsSquare));
    }
}
