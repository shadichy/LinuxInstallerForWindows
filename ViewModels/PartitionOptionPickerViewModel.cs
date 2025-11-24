using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LinuxInstaller.ViewModels.Interfaces; // Add this using directive
using LinuxInstaller.Services; // Add this using directive
using LinuxInstaller.Models; // Add this using directive

namespace LinuxInstaller.ViewModels;

public partial class PartitionOptionPickerViewModel : ObservableObject, INavigatableViewModel
{
    private readonly InstallationConfigService _installationConfigService; // Injected service

    [ObservableProperty]
    private WorkflowType _selectedWorkflow = WorkflowType.None; // Default to None

    public PartitionOptionPickerViewModel(InstallationConfigService installationConfigService)
    {
        _installationConfigService = installationConfigService;
    }

    [RelayCommand]
    private void SelectWorkflow(WorkflowType workflowType)
    {
        SelectedWorkflow = workflowType;
        _installationConfigService.SelectedWorkflow = workflowType;
        // The MainViewModel's CanGoNext property will re-evaluate automatically
        // because it observes changes on the current page's INavigatableViewModel properties.
    }

    // INavigatableViewModel Implementation
    public bool CanProceed => SelectedWorkflow != WorkflowType.None; // Can proceed only if a workflow is selected
    public bool CanGoBack => true; // Assume always can go back
}
