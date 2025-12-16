using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;

namespace LinuxInstaller.UserControls;

public partial class TextFieldControl : UserControl, INotifyPropertyChanged
{
    public TextFieldControl()
    {
        InitializeComponent();
        IsPassword = false;
    }

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
    public static readonly StyledProperty<string> TextProperty =
        AvaloniaProperty.Register<TextFieldControl, string>(nameof(Text));
    public static readonly StyledProperty<char> PasswordCharProperty =
        AvaloniaProperty.Register<TextFieldControl, char>(nameof(PasswordChar));
    public static readonly StyledProperty<string> ErrorProperty =
        AvaloniaProperty.Register<TextFieldControl, string>(nameof(Error));
    public static readonly StyledProperty<bool> IsPasswordProperty =
        AvaloniaProperty.Register<TextFieldControl, bool>(nameof(IsPassword));
    public static readonly StyledProperty<string> LeadingIconProperty =
        AvaloniaProperty.Register<TextFieldControl, string>(nameof(LeadingIcon));
    public static readonly StyledProperty<string> TrailingIconProperty =
        AvaloniaProperty.Register<TextFieldControl, string>(nameof(TrailingIcon));
    public static readonly StyledProperty<string> VariantProperty =
        AvaloniaProperty.Register<TextFieldControl, string>(nameof(Variant), defaultValue: "Default");
    public static readonly StyledProperty<CornerRadius> BorderRadiusProperty =
        AvaloniaProperty.Register<TextFieldControl, CornerRadius>(nameof(BorderRadius), defaultValue: new(4));
    public static readonly StyledProperty<bool> IsEnabledProperty =
        AvaloniaProperty.Register<TextFieldControl, bool>(nameof(IsEnabled), defaultValue: true);
    // Numeric properties
    public static readonly StyledProperty<bool> IsNumericProperty =
        AvaloniaProperty.Register<TextFieldControl, bool>(nameof(IsNumeric), defaultValue: false);
    public static readonly StyledProperty<decimal> ValueDecimalProperty =
        AvaloniaProperty.Register<TextFieldControl, decimal>(nameof(ValueDecimal), defaultBindingMode: Avalonia.Data.BindingMode.TwoWay);
    public static readonly StyledProperty<decimal> MinValueProperty =
        AvaloniaProperty.Register<TextFieldControl, decimal>(nameof(MinValue), defaultValue: decimal.MinValue);
    public static readonly StyledProperty<decimal> MaxValueProperty =
        AvaloniaProperty.Register<TextFieldControl, decimal>(nameof(MaxValue), defaultValue: decimal.MaxValue);

    public string Label
    {
        get => GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }
    public string Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    public char PasswordChar
    {
        get => GetValue(PasswordCharProperty);
        set => SetValue(PasswordCharProperty, value);
    }
    public string Error
    {
        get => GetValue(ErrorProperty);
        set => SetValue(ErrorProperty, value);
    }
    public bool IsPassword
    {
        get => GetValue(IsPasswordProperty);
        set
        {
            SetValue(IsPasswordProperty, value);
            PasswordChar = value ? '*' : '\0';
        }
    }
    public string LeadingIcon
    {
        get => GetValue(LeadingIconProperty);
        set => SetValue(LeadingIconProperty, value);
    }
    public string TrailingIcon
    {
        get => GetValue(TrailingIconProperty);
        set => SetValue(TrailingIconProperty, value);
    }
    private string _variant = "Default";
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
    public bool IsEnabled
    {
        get => GetValue(IsEnabledProperty);
        set => SetValue(IsEnabledProperty, value);
    }
    public bool IsNumeric
    {
        get => GetValue(IsNumericProperty);
        set => SetValue(IsNumericProperty, value);
    }
    public decimal ValueDecimal
    {
        get => GetValue(ValueDecimalProperty);
        set => SetValue(ValueDecimalProperty, value);
    }
    public decimal MinValue
    {
        get => GetValue(MinValueProperty);
        set => SetValue(MinValueProperty, value);
    }
    public decimal MaxValue
    {
        get => GetValue(MaxValueProperty);
        set => SetValue(MaxValueProperty, value);
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
