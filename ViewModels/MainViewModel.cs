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
            new WorkflowSelectionViewModel(),
            new PreFlightCheckViewModel(),
            new DistroPickerViewModel(),
            new PartitionOptionPickerViewModel(),
            new PartitionEditorViewModel(),
            new UserCreationViewModel(),
            new InstallationSummaryViewModel(),
            new InstallationProgressViewModel(),
            new LoadingViewModel()
        };
    }

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
        // Finish logic here
    }
}

