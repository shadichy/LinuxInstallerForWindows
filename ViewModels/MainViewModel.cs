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
    private readonly InstallationConfigService _installationConfigService; // Injected service

    [ObservableProperty]
    private int _currentPageIndex;

    public ObservableCollection<ObservableObject> Pages { get; }

    public MainViewModel(
        Func<WorkflowSelectionViewModel> workflowSelectionViewModelFactory,
        Func<PreFlightCheckViewModel> preFlightCheckViewModelFactory,
        Func<DistroPickerViewModel> distroPickerViewModelFactory,
        Func<PartitionOptionPickerViewModel> partitionOptionPickerViewModelFactory,
        Func<PartitionEditorViewModel> partitionEditorViewModelFactory,
        Func<UserCreationViewModel> userCreationViewModelFactory,
        Func<InstallationSummaryViewModel> installationSummaryViewModelFactory,
        Func<InstallationProgressViewModel> installationProgressViewModelFactory,
        Func<LoadingViewModel> loadingViewModelFactory,
        ConfigGeneratorService configGeneratorService, // Injected service
        AssetManagerService assetManagerService,       // Injected service
        BootManagerService bootManagerService,          // Injected service
        InstallationConfigService installationConfigService // Injected service
    )
    {
        _configGeneratorService = configGeneratorService;
        _assetManagerService = assetManagerService;
        _bootManagerService = bootManagerService;
        _installationConfigService = installationConfigService;

        Pages = new ObservableCollection<ObservableObject>
        {
            // TODO: The order and inclusion of pages should be dynamic based on user choices
            // (e.g., skip partition editor if automatic partitioning is chosen).
            workflowSelectionViewModelFactory(),
            preFlightCheckViewModelFactory(),
            distroPickerViewModelFactory(),
            partitionOptionPickerViewModelFactory(),
            partitionEditorViewModelFactory(),
            userCreationViewModelFactory(),
            installationSummaryViewModelFactory(),
            installationProgressViewModelFactory(),
            loadingViewModelFactory() // This might be better as a separate, overlay view rather than a page in the carousel.
        };
    }

    public bool CanGoBack => CurrentPageIndex > 0 && ((Pages[CurrentPageIndex] as INavigatableViewModel)?.CanGoBack ?? true);
    public bool CanGoNext => CurrentPageIndex < Pages.Count - 1 && ((Pages[CurrentPageIndex] as INavigatableViewModel)?.CanProceed ?? true);
    public bool IsFinishVisible => CurrentPageIndex == Pages.Count - 1;

    [RelayCommand]
    private void NextPage()
    {
        if (CanGoNext)
        {
            CurrentPageIndex++;
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
            CurrentPageIndex--;
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
        _configGeneratorService.GenerateGrubConfig(); // Placeholder call
        _configGeneratorService.GenerateInstallConfig(); // Placeholder call

        // 2. Place bootloaders (shim, grub) and configs on the appropriate partitions (ESP and staging area) using AssetManager and BootManagerService.
        _assetManagerService.ExtractBootloaders(); // Placeholder call
        _bootManagerService.PlaceBootloaders(); // Placeholder call

        // 3. Create the BCD entry using BootManagerService.
        _bootManagerService.CreateBcdEntry(); // Placeholder call

        // 4. Prompt the user to reboot. (This would involve UI interaction, not directly here)
        Console.WriteLine("Installation Finished. Please reboot."); // Placeholder for now
    }
}

