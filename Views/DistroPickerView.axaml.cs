using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace LinuxInstaller.Views;

public partial class DistroPickerView : UserControl
{
    public DistroPickerView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
