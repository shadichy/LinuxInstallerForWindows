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
    public static IServiceProvider ServiceProvider { get; private set; } = null!;

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
        CreateAndApplyTheme();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var mainViewModel = ServiceProvider.GetRequiredService<MainViewModel>();
            desktop.MainWindow = new MainView
            {
                DataContext = mainViewModel,
                Width = 800, // Set the width here
                Height = 520 // Set the height here
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void CreateAndApplyTheme()
    {
        var seedColor = System.Drawing.Color.LimeGreen;
        var colorScheme = new MaterialColorScheme(seedColor);

        var resources = new ResourceDictionary();
        foreach (var keypair in colorScheme.SchemeColors)
        {
            var color = keypair.Value;
            var avaloniaColor = Color.FromArgb(color.A, color.R, color.G, color.B);
            resources.Add(keypair.Key + "Brush", new SolidColorBrush(avaloniaColor));
            this.Resources[keypair.Key + "Brush"] = new SolidColorBrush(avaloniaColor);
        }

        this.Resources.MergedDictionaries.Add(resources);
    }
}