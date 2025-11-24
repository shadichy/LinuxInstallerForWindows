using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace LinuxInstaller.UserControls;

public partial class SummaryCardControl : UserControl
{
    public static readonly DirectProperty<SummaryCardControl, PathGeometry> IconDataProperty =
        AvaloniaProperty.RegisterDirect<SummaryCardControl, PathGeometry>(
            nameof(IconData),
            o => o.IconData,
            (o, v) => o.IconData = v);

    public static readonly DirectProperty<SummaryCardControl, string> TitleProperty =
        AvaloniaProperty.RegisterDirect<SummaryCardControl, string>(
            nameof(Title),
            o => o.Title,
            (o, v) => o.Title = v);

    public static readonly DirectProperty<SummaryCardControl, ObservableCollection<DetailItem>> DetailsProperty =
        AvaloniaProperty.RegisterDirect<SummaryCardControl, ObservableCollection<DetailItem>>(
            nameof(Details),
            o => o.Details,
            (o, v) => o.Details = v);

    private PathGeometry _iconData;
    public PathGeometry IconData
    {
        get => _iconData;
        set => SetAndRaise(IconDataProperty, ref _iconData, value);
    }

    private string _title;
    public string Title
    {
        get => _title;
        set => SetAndRaise(TitleProperty, ref _title, value);
    }

    private ObservableCollection<DetailItem> _details;
    public ObservableCollection<DetailItem> Details
    {
        get => _details;
        set => SetAndRaise(DetailsProperty, ref _details, value);
    }

    public SummaryCardControl()
    {
        InitializeComponent();
        _details = new ObservableCollection<DetailItem>(); // Initialize to avoid null reference
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}

public partial class DetailItem : ObservableObject
{
    [ObservableProperty]
    private string _label;

    [ObservableProperty]
    private string _value;

    public DetailItem(string label, string value)
    {
        Label = label;
        Value = value;
    }
}
