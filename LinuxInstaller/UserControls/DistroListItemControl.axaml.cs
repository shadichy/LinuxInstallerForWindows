using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input; // Added this using statement

namespace LinuxInstaller.UserControls;

public partial class DistroListItemControl : UserControl, INotifyPropertyChanged
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
    
    static DistroListItemControl()
    {
        DistroNameProperty.Changed.AddClassHandler<DistroListItemControl>((x, e) => x.OnPropertyChanged(nameof(Identifier)));
        VersionProperty.Changed.AddClassHandler<DistroListItemControl>((x, e) => x.OnPropertyChanged(nameof(Identifier)));
        IconUrlProperty.Changed.AddClassHandler<DistroListItemControl>(async (x, e) => await x.LoadIcon(e.NewValue as string));
    }

    // TODO: The 'IsSelected' property should be properly bound and updated when the item is clicked.
    // This typically involves handling input events (e.g., PointerPressed) on the control.
    // Also, the IconSource should be bound to an actual image path or resource.
    public static readonly StyledProperty<IImage?> IconSourceProperty =
        AvaloniaProperty.Register<DistroListItemControl, IImage?>(nameof(IconSource));

    public static readonly StyledProperty<string?> IconUrlProperty =
        AvaloniaProperty.Register<DistroListItemControl, string?>(nameof(IconUrl));

    public static readonly StyledProperty<string> DistroNameProperty =
        AvaloniaProperty.Register<DistroListItemControl, string>(nameof(DistroName));

    public static readonly StyledProperty<string> VersionProperty =
        AvaloniaProperty.Register<DistroListItemControl, string>(nameof(Version));

    public static readonly StyledProperty<string> DescriptionProperty =
        AvaloniaProperty.Register<DistroListItemControl, string>(nameof(Description));

    public static readonly StyledProperty<ulong> SizeProperty =
        AvaloniaProperty.Register<DistroListItemControl, ulong>(nameof(Size));

    public static readonly StyledProperty<bool> IsSelectedProperty =
        AvaloniaProperty.Register<DistroListItemControl, bool>(nameof(IsSelected));

    public static readonly StyledProperty<ICommand?> SelectCommandProperty =
        AvaloniaProperty.Register<DistroListItemControl, ICommand?>(nameof(SelectCommand));

    public IImage? IconSource
    {
        get => GetValue(IconSourceProperty);
        set => SetValue(IconSourceProperty, value);
    }

    public string? IconUrl
    {
        get => GetValue(IconUrlProperty);
        set => SetValue(IconUrlProperty, value);
    }

    public string DistroName
    {
        get => GetValue(DistroNameProperty);
        set => SetValue(DistroNameProperty, value);
    }

    public string Version
    {
        get => GetValue(VersionProperty);
        set => SetValue(VersionProperty, value);
    }

    public string Identifier => (DistroName ?? "") + " " + (Version ?? "");

    public string Description
    {
        get => GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    public ulong Size
    {
        get => GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    public bool IsSelected
    {
        get => GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }

    public ICommand? SelectCommand
    {
        get => GetValue(SelectCommandProperty);
        set => SetValue(SelectCommandProperty, value);
    }

    public DistroListItemControl()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private async Task LoadIcon(string? url)
    {
        Debug.WriteLine($"Attempting to load icon from URL: {url}");
        if (string.IsNullOrEmpty(url))
        {
            IconSource = null;
            return;
        }

        try
        {
            using var httpClient = new HttpClient();
            using var stream = await httpClient.GetStreamAsync(url);
            IconSource = new Bitmap(stream);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Failed to load icon from URL: {url} ({ex.Message}). Loading fallback icon.");
            try
            {
                var uri = new Uri("avares://LinuxInstaller/Assets/Icons/cloud_download.png");
                using (var stream = AssetLoader.Open(uri))
                {
                    IconSource = new Bitmap(stream);
                    Debug.WriteLine("Successfully loaded fallback icon.");
                }
            }
            catch (Exception fallbackEx)
            {
                Debug.WriteLine($"Failed to load fallback icon: {fallbackEx.Message}");
                IconSource = null;
            }
        }
    }
    private void Border_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        if (SelectCommand?.CanExecute(DataContext) == true)
        {
            SelectCommand.Execute(DataContext);
            e.Handled = true; // Mark event as handled to prevent further processing
        }
    }
}