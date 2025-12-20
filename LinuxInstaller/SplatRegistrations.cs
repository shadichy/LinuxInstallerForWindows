using System;
using Microsoft.Extensions.DependencyInjection;
using Splat;
using Splat.Microsoft.Extensions.DependencyInjection;
using LinuxInstaller.ViewModels;
using LinuxInstaller.Services;
using LinuxInstaller.Models;

namespace LinuxInstaller
{
    public static class SplatRegistrations
    {
        public static void Register(IServiceCollection services)
        {
            // Register Models (if they have dependencies or complex construction)
            // For now, Models are simple DTOs, so no explicit registration needed unless they get more complex.

            // Register Services
            services.AddSingleton<AssetManagerService>();
            services.AddSingleton<BootManagerService>();
            services.AddSingleton<ConfigGeneratorService>();
            services.AddSingleton<DiskpartService>();
            services.AddSingleton<DistroService>();
            services.AddSingleton<PartitionService>();
            services.AddSingleton<SystemAnalysisService>();
            services.AddSingleton<InstallationConfigService>(); // Register the new service
            services.AddSingleton<NavigationService>();

            // Register ViewModels (Transient means a new instance every time they are requested)
            // MainViewModel is a singleton as it manages the application's main state and navigation.
            services.AddSingleton<MainViewModel>();
            services.AddTransient<DistroPickerViewModel>();
            services.AddTransient<InstallationProgressViewModel>();
            services.AddTransient<InstallationSummaryViewModel>();
            services.AddTransient<LoadingViewModel>();
            services.AddTransient<PartitionEditorViewModel>();
            services.AddTransient<UserCreationViewModel>();
            services.AddTransient<WorkflowSelectionViewModel>();
            services.AddTransient<InstallationFinishViewModel>();
        }

    }
}
