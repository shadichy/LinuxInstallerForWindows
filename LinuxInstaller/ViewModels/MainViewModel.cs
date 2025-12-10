using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System;
using Microsoft.Extensions.DependencyInjection;
using LinuxInstaller.ViewModels.Interfaces;
using LinuxInstaller.Services;
using System.Reactive.Linq;
using System.Collections.Generic;

namespace LinuxInstaller.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly ConfigGeneratorService _configGeneratorService;
        private readonly AssetManagerService _assetManagerService;
        private readonly BootManagerService _bootManagerService;
        private readonly InstallationConfigService _installationConfigService;
        private readonly NavigationService _navigationService;


        private NavigatableViewModelBase _currentContent;
        public NavigatableViewModelBase CurrentContent
        {
            get => _currentContent;
            set => SetProperty(ref _currentContent, value);
        }

        public MainViewModel(
            WorkflowSelectionViewModel IWorkflowSelectionViewModel,
            PreFlightCheckViewModel IPreFlightCheckViewModel,
            DistroPickerViewModel IDistroPickerViewModel,
            PartitionOptionPickerViewModel IPartitionOptionPickerViewModel,
            PartitionEditorViewModel IPartitionEditorViewModel,
            UserCreationViewModel IUserCreationViewModel,
            InstallationSummaryViewModel IInstallationSummaryViewModel,
            InstallationProgressViewModel IInstallationProgressViewModel,
            LoadingViewModel ILoadingViewModel,
            ConfigGeneratorService configGeneratorService,
            AssetManagerService assetManagerService,
            BootManagerService bootManagerService,
            InstallationConfigService installationConfigService,
            NavigationService navigationService
        )
        {
            _configGeneratorService = configGeneratorService;
            _assetManagerService = assetManagerService;
            _bootManagerService = bootManagerService;
            _installationConfigService = installationConfigService;
            _navigationService = navigationService;

            List<KeyValuePair<string, NavigatableViewModelBase>> routes = [];
            routes.Add(new("loading", ILoadingViewModel));
            routes.Add(new("workflowSelection", IWorkflowSelectionViewModel));
            routes.Add(new("distroPicker", IDistroPickerViewModel));
            routes.Add(new("partitionEditor", IPartitionEditorViewModel));
            routes.Add(new("userCreation", IUserCreationViewModel));
            routes.Add(new("installationSummary", IInstallationSummaryViewModel));
            routes.Add(new("installationProgress", IInstallationProgressViewModel));
            routes.Add(new("preFlightCheck", IPreFlightCheckViewModel));

            _navigationService.SetupRoutes(routes);

            _currentContent = _navigationService.CurrentPage;

            _navigationService.CurrentPageIndexObservable
                .Subscribe(viewModel => CurrentContent = _navigationService.CurrentPage);
        }
        
        private int CurrentPageIndex => _navigationService.CurrentPageIndex;

    private void Finish()
    {
        // TODO: Implement the finalization logic.
        // This should trigger the core installation process:
        // 1. Generate final configs (grub.cfg, install.conf) using ConfigGeneratorService.
        // Example: Accessing selected distro for config generation
        Console.WriteLine($"Generating config for: {_installationConfigService.SelectedDistro?.DistroName}");

        var grubConfig = _configGeneratorService.GenerateGrubStage1Config("/.myinstaller/stage2.cfg");
        // TODO: Save grubConfig to a file.

        if (_installationConfigService.PartitionPlan.TargetDisk != null)
        {
            var installConfig = _configGeneratorService.GenerateInstallConf(
                _installationConfigService.PartitionPlan.TargetDisk.Id,
                new System.Collections.Generic.List<Models.PartitionPlan> { _installationConfigService.PartitionPlan });
            // TODO: Save installConfig to a file.
        }

        // 2. Place bootloaders (shim, grub) and configs on the appropriate partitions (ESP and staging area) using AssetManager and BootManagerService.
        var espPath = _bootManagerService.MountEsp();
        if (!string.IsNullOrEmpty(espPath))
        {
            _assetManagerService.CopyBundledBootloader(espPath);

            // 3. Create the BCD entry using BootManagerService.
            _bootManagerService.CreateBcdEntry(espPath, "EFI\\MyCustomInstaller\\shimx64.efi");

            _bootManagerService.UnmountEsp();
        }

        // 4. Prompt the user to reboot. (This would involve UI interaction, not directly here)
        Console.WriteLine("Installation Finished. Please reboot."); // Placeholder for now
    }
    }
}


