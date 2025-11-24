using Avalonia.Controls;
using LinuxInstaller.ViewModels;

namespace LinuxInstaller.Views;

public partial class MainView : Window
{
    public MainView()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    }
}