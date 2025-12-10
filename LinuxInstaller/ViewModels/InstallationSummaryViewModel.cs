using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LinuxInstaller.ViewModels.Interfaces;
using LinuxInstaller.Services; // Add this using directive
using LinuxInstaller.Models; // Add this using directive
using System.Collections.Generic;
using LinuxInstaller.Views;
using Avalonia.Controls.ApplicationLifetimes;
using System.Threading.Tasks;

namespace LinuxInstaller.ViewModels;

public partial class InstallationSummaryViewModel : NavigatableViewModelBase
{
    private readonly InstallationConfigService _installationConfigService;

    public InstallationSummaryViewModel(NavigationService navigationService, InstallationConfigService installationConfigService) : base(navigationService)
    {
        _installationConfigService = installationConfigService;
    }

    public Distro? SelectedDistro => _installationConfigService.SelectedDistro;
    public PartitionWorkflowType SelectedWorkflow => _installationConfigService.SelectedPartitionWorkflow;
    public UserInfo UserInfo => _installationConfigService.UserInfo;
    public PartitionPlan PartitionPlan => _installationConfigService.PartitionPlan;

    public bool IsDistroSelected => SelectedDistro != null;
    public bool IsWorkflowSelected => SelectedWorkflow != default;
    public bool IsUserInfoAvailable => UserInfo != null;
    public bool IsPartitionPlanAvailable => PartitionPlan != null;

    public List<KeyValuePair<string, string>> DistroSummaryContent
    {
        get
        {
            var content = new List<KeyValuePair<string, string>>();
            if (SelectedDistro != null)
            {
                content.Add(new("Name", SelectedDistro.DistroName));
                content.Add(new("Description", SelectedDistro.Description));
                content.Add(new("Size", $"{SelectedDistro.Size} bytes"));
            }
            return content;
        }
    }

    public List<KeyValuePair<string, string>> WorkflowSummaryContent
    {
        get
        {
            var content = new List<KeyValuePair<string, string>>();
            if (IsWorkflowSelected)
            {
                content.Add(new("Type", SelectedWorkflow.ToString()));
            }
            return content;
        }
    }

    public List<KeyValuePair<string, string>> UserSummaryContent
    {
        get
        {
            var content = new List<KeyValuePair<string, string>>();
            if (UserInfo != null)
            {
                content.Add(new("Full Name", UserInfo.FullName));
                content.Add(new("Username", UserInfo.Username));
                content.Add(new("Auto Login", UserInfo.AutoLogin.ToString()));
                content.Add(new("Require Password", UserInfo.RequirePasswordOnLogin.ToString()));
            }
            return content;
        }
    }

    public List<KeyValuePair<string, string>> PartitionSummaryContent
    {
        get
        {
            var content = new List<KeyValuePair<string, string>>();
            if (PartitionPlan != null)
            {
                content.Add(new("Target Disk", PartitionPlan.TargetDisk.Name));
                content.Add(new("Shrink Size", $"{PartitionPlan.ShrinkSizeInMB} MB"));
                // TODO: Add planned Linux partitions to the summary
            }
            return content;
        }
    }

    [RelayCommand]
    private async Task Install()
    {
        var dialog = new ConfirmationDialogView();
        dialog.DataContext = new ConfirmationDialogViewModel("This will start the installation process.\nAre you sure you want to continue?", dialog);
        
        if (Avalonia.Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            bool result = await dialog.ShowDialog<bool>(owner: desktop.MainWindow);
            if (result)
            {
                Navigation.Next();
            }
        }
    }

    [RelayCommand]
    private void GoBack()
    {
        Navigation.Previous();
    }
    
    // INavigatableViewModel Implementation
    public override bool CanProceed => true; // Assume always can proceed to start installation
    public override bool CanGoBack => true; // Assume always can go back to review/edit
}
