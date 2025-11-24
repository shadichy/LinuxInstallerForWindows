using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace LinuxInstaller.UserControls;

public partial class HeaderControl : UserControl
{
    public HeaderControl()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
