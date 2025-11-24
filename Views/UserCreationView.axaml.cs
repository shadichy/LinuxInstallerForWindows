using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace LinuxInstaller.Views;

public partial class UserCreationView : UserControl
{
    public UserCreationView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
