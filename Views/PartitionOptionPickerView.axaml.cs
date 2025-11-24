using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace LinuxInstaller.Views;

public partial class PartitionOptionPickerView : UserControl
{
    public PartitionOptionPickerView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
