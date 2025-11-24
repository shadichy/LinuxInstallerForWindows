using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LinuxInstaller.ViewModels.Interfaces;
using LinuxInstaller.Services; // Add this using directive
using LinuxInstaller.Models; // Add this using directive

namespace LinuxInstaller.ViewModels;

public partial class InstallationSummaryViewModel : ObservableObject, INavigatableViewModel
{
    private readonly InstallationConfigService _installationConfigService;

    public InstallationSummaryViewModel(InstallationConfigService installationConfigService)
    {
        _installationConfigService = installationConfigService;
    }

    public Distro? SelectedDistro => _installationConfigService.SelectedDistro;
    public WorkflowType SelectedWorkflow => _installationConfigService.SelectedWorkflow;
    public UserInfo UserInfo => _installationConfigService.UserInfo;
    public PartitionPlan PartitionPlan => _installationConfigService.PartitionPlan;

    // INavigatableViewModel Implementation
    public bool CanProceed => true; // Assume always can proceed to start installation
    public bool CanGoBack => true; // Assume always can go back to review/edit
}
