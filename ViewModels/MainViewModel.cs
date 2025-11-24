using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace LinuxInstaller.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private int _currentPageIndex;

    public ObservableCollection<ObservableObject> Pages { get; }

    public MainViewModel()
    {
        Pages = new ObservableCollection<ObservableObject>
        {
            // TODO: The order and inclusion of pages should be dynamic based on user choices
            // (e.g., skip partition editor if automatic partitioning is chosen).
            new WorkflowSelectionViewModel(),
            new PreFlightCheckViewModel(),
            new DistroPickerViewModel(),
            new PartitionOptionPickerViewModel(),
            new PartitionEditorViewModel(),
            new UserCreationViewModel(),
            new InstallationSummaryViewModel(),
            new InstallationProgressViewModel(),
            new LoadingViewModel() // This might be better as a separate, overlay view rather than a page in the carousel.
        };
    }

    // TODO: The CanGoNext and CanGoBack logic should be more sophisticated.
    // For example, it should check if the current page's tasks are complete before allowing the user to proceed.
    public bool CanGoBack => CurrentPageIndex > 0;
    public bool CanGoNext => CurrentPageIndex < Pages.Count - 1;
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
        // 2. Place bootloaders (shim, grub) and configs on the appropriate partitions (ESP and staging area) using AssetManager and BootManagerService.
        // 3. Create the BCD entry using BootManagerService.
        // 4. Prompt the user to reboot.
    }
}

