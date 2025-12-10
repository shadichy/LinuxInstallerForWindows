using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LinuxInstaller.ViewModels.Interfaces; // Add this using directive
using LinuxInstaller.Services; // Add this using directive
using LinuxInstaller.Models; // Add this using directive

namespace LinuxInstaller.ViewModels;

public partial class PartitionOptionPickerViewModel : NavigatableViewModelBase
{
    private readonly InstallationConfigService _installationConfigService; // Injected service

    [ObservableProperty]
    private PartitionWorkflowType _selectedWorkflow = PartitionWorkflowType.None; // Default to None

    public PartitionOptionPickerViewModel(NavigationService navigationService, InstallationConfigService installationConfigService) : base(navigationService)
    {
        _installationConfigService = installationConfigService;
    }

    [RelayCommand]
    private void SelectWorkflow(PartitionWorkflowType workflowType)
    {
        SelectedWorkflow = workflowType;
        _installationConfigService.SelectedPartitionWorkflow = workflowType;

        if (workflowType == PartitionWorkflowType.Automatic)
        {
            Navigation.Next(2);
        } 
        else if (workflowType == PartitionWorkflowType.Manual)
        {
            Navigation.Next();
        }
    }

    public override bool CanProceed => SelectedWorkflow != PartitionWorkflowType.None;
    public override bool CanGoBack => true;

    [RelayCommand]
    public void Back()
    {
        Navigation.Previous();
    }
}
