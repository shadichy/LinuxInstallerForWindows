using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using CommunityToolkit.Mvvm.Input;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LinuxInstaller.UserControls;

public partial class SelectControl : UserControl, INotifyPropertyChanged
{
    public SelectControl()
    {
        InitializeComponent();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetAndRaise<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
            return false;

        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    public static readonly StyledProperty<IEnumerable?> ItemsSourceProperty =
        AvaloniaProperty.Register<SelectControl, IEnumerable?>(nameof(ItemsSource));
    public static readonly StyledProperty<object?> SelectedItemProperty =
        AvaloniaProperty.Register<SelectControl, object?>(nameof(SelectedItem), defaultBindingMode: Avalonia.Data.BindingMode.TwoWay);
    public static readonly StyledProperty<string?> LabelProperty =
        AvaloniaProperty.Register<SelectControl, string?>(nameof(Label));
    public static readonly StyledProperty<IDataTemplate?> ItemTemplateProperty =
        AvaloniaProperty.Register<SelectControl, IDataTemplate?>(nameof(ItemTemplate));
    public static readonly StyledProperty<IDataTemplate?> SelectedItemTemplateProperty =
        AvaloniaProperty.Register<SelectControl, IDataTemplate?>(nameof(SelectedItemTemplate));
    public static readonly StyledProperty<string> VariantProperty =
        AvaloniaProperty.Register<TextFieldControl, string>(nameof(Variant), defaultValue: "Default");
    public static readonly StyledProperty<CornerRadius> BorderRadiusProperty =
        AvaloniaProperty.Register<TextFieldControl, CornerRadius>(nameof(BorderRadius), defaultValue: new(4));

    public IEnumerable? ItemsSource
    {
        get => GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }
    public object? SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }
    public string? Label
    {
        get => GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }
    public IDataTemplate? ItemTemplate
    {
        get => GetValue(ItemTemplateProperty);
        set => SetValue(ItemTemplateProperty, value);
    }
    public IDataTemplate? SelectedItemTemplate
    {
        get => GetValue(SelectedItemTemplateProperty);
        set => SetValue(SelectedItemTemplateProperty, value);
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
    public bool IsOutlined => Variant == "Outlined";
    private CornerRadius _borderRadius = new(4);
    public CornerRadius BorderRadius
    {
        get => _borderRadius;
        set
        {
            if (SetAndRaise(ref _borderRadius, value)) OnPropertyChanged(nameof(ICornerRadius));
        }
    }
    public CornerRadius ICornerRadius => Variant.Equals("Outlined") ? BorderRadius : new(4, 4, 0, 0);

    [RelayCommand]
    private void SelectItem(object item)
    {
        SelectedItem = item;
        var toggleButton = this.FindDescendantOfType<ToggleButton>();
        if (toggleButton != null) toggleButton.IsChecked = false;
    }
}

