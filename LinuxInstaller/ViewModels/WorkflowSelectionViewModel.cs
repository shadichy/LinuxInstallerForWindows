using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LinuxInstaller.Models;
using LinuxInstaller.Services;
using LinuxInstaller.ViewModels.Interfaces;

namespace LinuxInstaller.ViewModels;

public partial class WorkflowSelectionViewModel : NavigatableViewModelBase
{
    private readonly InstallationConfigService _installationConfigService; // Injected service

    [ObservableProperty]
    private InstallWorkflowType _selectedWorkflow = InstallWorkflowType.None; // Default to None

    public WorkflowSelectionViewModel(NavigationService navigationService, InstallationConfigService installationConfigService) : base(navigationService)
    {
        _installationConfigService = installationConfigService;
    }

    private string _hoverText = "Choose a distribution to download.";

    public string HoverText
    {
        get => _hoverText;
        set => SetProperty(ref _hoverText, value);
    }

    [RelayCommand]
    private void SelectWorkflow(InstallWorkflowType workflowType)
    {
        SelectedWorkflow = workflowType;
        _installationConfigService.SelectedInstallWorkflow = workflowType;

        if (workflowType == InstallWorkflowType.Distro)
        {
            Navigation.Next();
        }
        else if (workflowType == InstallWorkflowType.Iso)
        {
            // TODO: Opens file dialog to select ISO and then navigates to ISO handling page
        }
    }

    [RelayCommand]
    private void SelectDistro()
    {
        Navigation.Goto(2);
    }

    [RelayCommand]
    private void SelectDistroHover()
    {
        HoverText = "Choose a distribution to download.";
    }

    [RelayCommand]
    private void SelectIsoHover()
    {
        HoverText = "Pick your own ISO to boot.";
    }
    
    public override bool CanProceed => true;
    public override bool CanGoBack => true;
}



