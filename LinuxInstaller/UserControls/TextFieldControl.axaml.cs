using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;

namespace LinuxInstaller.UserControls;

public partial class TextFieldControl : UserControl, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetAndRaise<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    public static readonly StyledProperty<string> LabelProperty =
        AvaloniaProperty.Register<TextFieldControl, string>(nameof(Label));

    public string Label
    {
        get => GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

    public static readonly StyledProperty<string> TextProperty =
        AvaloniaProperty.Register<TextFieldControl, string>(nameof(Text));

    public string Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public static readonly StyledProperty<char> PasswordCharProperty =
        AvaloniaProperty.Register<TextFieldControl, char>(nameof(PasswordChar));

    public char PasswordChar
    {
        get => GetValue(PasswordCharProperty);
        set => SetValue(PasswordCharProperty, value);
    }

    public static readonly StyledProperty<string> ErrorProperty =
        AvaloniaProperty.Register<TextFieldControl, string>(nameof(Error));

    public string Error
    {
        get => GetValue(ErrorProperty);
        set => SetValue(ErrorProperty, value);
    }

    public static readonly StyledProperty<bool> IsPasswordProperty =
        AvaloniaProperty.Register<TextFieldControl, bool>(nameof(IsPassword));

    public bool IsPassword
    {
        get => GetValue(IsPasswordProperty);
        set
        {
            SetValue(IsPasswordProperty, value);
            PasswordChar = value ? '*' : '\0';
        }
    }

    public static readonly StyledProperty<string> LeadingIconProperty =
        AvaloniaProperty.Register<TextFieldControl, string>(nameof(LeadingIcon));

    public string LeadingIcon
    {
        get => GetValue(LeadingIconProperty);
        set => SetValue(LeadingIconProperty, value);
    }

    public static readonly StyledProperty<string> TrailingIconProperty =
        AvaloniaProperty.Register<TextFieldControl, string>(nameof(TrailingIcon));

    public string TrailingIcon
    {
        get => GetValue(TrailingIconProperty);
        set => SetValue(TrailingIconProperty, value);
    }

    private string _variant = "Default";
    public static readonly StyledProperty<string> VariantProperty =
        AvaloniaProperty.Register<TextFieldControl, string>(nameof(Variant), defaultValue: "Default");

    public string Variant
    {
        get => _variant;
        set
        {
            if (SetAndRaise(ref _variant, value))
            {
                OnPropertyChanged(nameof(IsOutlined));
                OnPropertyChanged(nameof(ICornerRadius));
            }
        }
    }

    public bool IsOutlined => Variant.Equals("Outlined");

    private CornerRadius _borderRadius = new(4);
    public static readonly StyledProperty<CornerRadius> BorderRadiusProperty =
        AvaloniaProperty.Register<TextFieldControl, CornerRadius>(nameof(BorderRadius), defaultValue: new(4));

    public CornerRadius BorderRadius
    {
        get => _borderRadius;
        set
        {
            if (SetAndRaise(ref _borderRadius, value))
            {
                OnPropertyChanged(nameof(ICornerRadius));
            }
        }

    }

    public CornerRadius ICornerRadius => Variant.Equals("Outlined") ? BorderRadius : new(4, 4, 0, 0);

    public static readonly StyledProperty<bool> IsEnabledProperty =
        AvaloniaProperty.Register<TextFieldControl, bool>(nameof(IsEnabled), defaultValue: true);

    public bool IsEnabled
    {
        get => GetValue(IsEnabledProperty);
        set => SetValue(IsEnabledProperty, value);
    }

    // Numeric properties
    public static readonly StyledProperty<bool> IsNumericProperty =
        AvaloniaProperty.Register<TextFieldControl, bool>(nameof(IsNumeric), defaultValue: false);

    public bool IsNumeric
    {
        get => GetValue(IsNumericProperty);
        set => SetValue(IsNumericProperty, value);
    }

    public static readonly StyledProperty<decimal> ValueDecimalProperty =
        AvaloniaProperty.Register<TextFieldControl, decimal>(nameof(ValueDecimal), defaultBindingMode: Avalonia.Data.BindingMode.TwoWay);
    public decimal ValueDecimal
    {
        get => GetValue(ValueDecimalProperty);
        set => SetValue(ValueDecimalProperty, value);
    }

    public static readonly StyledProperty<decimal> MinValueProperty =
        AvaloniaProperty.Register<TextFieldControl, decimal>(nameof(MinValue), defaultValue: decimal.MinValue);

    public decimal MinValue
    {
        get => GetValue(MinValueProperty);
        set => SetValue(MinValueProperty, value);
    }

    public static readonly StyledProperty<decimal> MaxValueProperty =
        AvaloniaProperty.Register<TextFieldControl, decimal>(nameof(MaxValue), defaultValue: decimal.MaxValue);

    public decimal MaxValue
    {
        get => GetValue(MaxValueProperty);
        set => SetValue(MaxValueProperty, value);
    }

    public TextFieldControl()
    {
        InitializeComponent();
        IsPassword = false;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
