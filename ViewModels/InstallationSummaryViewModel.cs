using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LinuxInstaller.ViewModels.Interfaces;
using LinuxInstaller.Services; // Add this using directive
using LinuxInstaller.Models; // Add this using directive

namespace LinuxInstaller.ViewModels;

public partial class InstallationSummaryViewModel : ObservableObject, INavigatableViewModel
{
    private readonly InstallationConfigService _installationConfigService;

    // This constructor is used by the designer.
    public InstallationSummaryViewModel() : this(new())
    {
    }

    public InstallationSummaryViewModel(InstallationConfigService installationConfigService)
    {
        _installationConfigService = installationConfigService;
    }

    public Distro? SelectedDistro { get => _installationConfigService.SelectedDistro; }
    public WorkflowType SelectedWorkflow { get => _installationConfigService.SelectedWorkflow; }
    public UserInfo UserInfo { get => _installationConfigService.UserInfo; }
    public PartitionPlan PartitionPlan { get => _installationConfigService.PartitionPlan; }

    public bool IsDistroSelected => SelectedDistro != null;
    public bool IsWorkflowSelected => SelectedWorkflow != default;
    public bool IsUserInfoAvailable => UserInfo != null;
    public bool IsPartitionPlanAvailable => PartitionPlan != null;

    // INavigatableViewModel Implementation
    public bool CanProceed => true; // Assume always can proceed to start installation
    public bool CanGoBack => true; // Assume always can go back to review/edit
}
