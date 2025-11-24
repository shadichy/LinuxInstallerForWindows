using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using LinuxInstaller.Views;
using LinuxInstaller.Themes;
using System;
using System.Drawing;
using Avalonia.Media;

namespace LinuxInstaller;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainView();
        }

        CreateAndApplyTheme();

        base.OnFrameworkInitializationCompleted();
    }

    private void CreateAndApplyTheme()
    {
        var random = new Random();
        var seedColor = System.Drawing.Color.FromArgb(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));
        var colorScheme = new MaterialColorScheme(seedColor);

        var resources = new ResourceDictionary();
        foreach (var prop in typeof(MaterialColorScheme).GetProperties())
        {
            var color = (System.Drawing.Color)prop.GetValue(colorScheme);
            var avaloniaColor = Avalonia.Media.Color.FromArgb(color.A, color.R, color.G, color.B);
            resources.Add(prop.Name + "Brush", new SolidColorBrush(avaloniaColor));
        }
        
        this.Resources.MergedDictionaries.Add(resources);
    }
}