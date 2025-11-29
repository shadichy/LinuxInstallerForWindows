using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using LinuxInstaller.Views;
using LinuxInstaller.Themes;
using System;
using Avalonia.Media;
using Microsoft.Extensions.DependencyInjection;
using Splat;
using Splat.Microsoft.Extensions.DependencyInjection;
using LinuxInstaller.ViewModels;
using Avalonia.Styling;

namespace LinuxInstaller;

public partial class App : Application
{
    public static IServiceProvider? ServiceProvider { get; private set; }

    public App()
    {
        var services = new ServiceCollection();
        SplatRegistrations.Register(services);
        services.UseMicrosoftDependencyResolver();
        ServiceProvider = services.BuildServiceProvider();
    }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var mainViewModel = ServiceProvider!.GetRequiredService<MainViewModel>();
            desktop.MainWindow = new MainView
            {
                DataContext = mainViewModel
            };
        }

        CreateAndApplyTheme();

        base.OnFrameworkInitializationCompleted();
    }

    private void CreateAndApplyTheme()
    {
        var random = new Random();
        var seedColor = System.Drawing.Color.FromArgb(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));
        var colorScheme = new MaterialColorScheme(seedColor);

        var resources = new Avalonia.Controls.ResourceDictionary();
        foreach (var prop in typeof(MaterialColorScheme).GetProperties())
        {
            var propValue = prop.GetValue(colorScheme);
            if (propValue is System.Drawing.Color color)
            {
                var avaloniaColor = Avalonia.Media.Color.FromArgb(color.A, color.R, color.G, color.B);
                resources.Add(prop.Name + "Brush", new SolidColorBrush(avaloniaColor));
            }
        }
        
        this.Resources.MergedDictionaries.Add(resources);
    }
}