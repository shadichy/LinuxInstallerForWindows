using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using LinuxInstaller.ViewModels.Components;

namespace LinuxInstaller.UserControls;

public partial class CircularProgressControl : UserControl
{
    public CircularProgressControl()
    {
        InitializeComponent();
        DataContext = new CircularProgressControlViewModel();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}