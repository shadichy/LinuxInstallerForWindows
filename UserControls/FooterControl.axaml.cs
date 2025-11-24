using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace LinuxInstaller.UserControls;

public partial class FooterControl : UserControl
{
    public FooterControl()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
