using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System; // Required for Func<T>
using Microsoft.Extensions.DependencyInjection; // Not strictly needed for Func<T> but good practice for DI contexts
using LinuxInstaller.ViewModels.Interfaces; // Add this using directive
using LinuxInstaller.Services; // Add this using directive

namespace LinuxInstaller.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly ConfigGeneratorService _configGeneratorService;
    private readonly AssetManagerService _assetManagerService;
    private readonly BootManagerService _bootManagerService;
    private readonly InstallationConfigService _installationConfigService;
    private readonly NavigationService _navigationService;

    public ObservableCollection<ObservableObject> Pages { get; }

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

        Pages = new ObservableCollection<ObservableObject>
        {
            ILoadingViewModel,
            IWorkflowSelectionViewModel,
            IPreFlightCheckViewModel,
            IDistroPickerViewModel,
            IPartitionOptionPickerViewModel,
            IPartitionEditorViewModel,
            IUserCreationViewModel,
            IInstallationSummaryViewModel,
            IInstallationProgressViewModel,
        };

        //CurrentContent = Pages[CurrentPageIndex];
    }

    public ObservableObject CurrentContent => Pages[_navigationService.PageIndex];

    private int CurrentPageIndex => _navigationService.PageIndex;

    public bool CanGoBack => CurrentPageIndex > 0 && ((Pages[CurrentPageIndex] as INavigatableViewModel)?.CanGoBack ?? true);
    public bool CanGoNext => CurrentPageIndex < Pages.Count - 1 && ((Pages[CurrentPageIndex] as INavigatableViewModel)?.CanProceed ?? true);
    public bool IsFinishVisible => CurrentPageIndex == Pages.Count - 1;

    [RelayCommand]
    private void NextPage()
    {
        if (CanGoNext)
        {
            _navigationService.Next();
            OnPropertyChanged(nameof(CanGoBack));
            OnPropertyChanged(nameof(CanGoNext));
            OnPropertyChanged(nameof(IsFinishVisible));
        }
    }

    [RelayCommand]
    private void PreviousPage()
    {
        if (CanGoBack)
        {
            _navigationService.Previous();
            OnPropertyChanged(nameof(CanGoBack));
            OnPropertyChanged(nameof(CanGoNext));
            OnPropertyChanged(nameof(IsFinishVisible));
        }
    }

    [RelayCommand]
    private void Finish()
    {
        // TODO: Implement the finalization logic.
        // This should trigger the core installation process:
        // 1. Generate final configs (grub.cfg, install.conf) using ConfigGeneratorService.
        // Example: Accessing selected distro for config generation
        Console.WriteLine($"Generating config for: {_installationConfigService.SelectedDistro?.Name}");

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

