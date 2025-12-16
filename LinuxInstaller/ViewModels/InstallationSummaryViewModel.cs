using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LinuxInstaller.ViewModels.Interfaces;
using LinuxInstaller.Services; // Add this using directive
using LinuxInstaller.Models; // Add this using directive
using System.Collections.Generic;
using LinuxInstaller.Views;
using Avalonia.Controls.ApplicationLifetimes;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Linq;
using LinuxInstaller.Converters;

namespace LinuxInstaller.ViewModels;

public partial class InstallationSummaryViewModel : NavigatableViewModelBase
{
    private readonly InstallationConfigService _installationConfigService;

    public InstallationSummaryViewModel(NavigationService navigationService, InstallationConfigService installationConfigService) : base(navigationService)
    {
        _installationConfigService = installationConfigService;
    }

    public string Title => "Installation Summary";
    public string Subtitle => "Review your selections before proceeding.";

    public Distro? SelectedDistro => _installationConfigService.SelectedDistro;
    public PartitionWorkflowType SelectedWorkflow => _installationConfigService.SelectedPartitionWorkflow;
    public UserInfo UserInfo => _installationConfigService.UserInfo;
    public PartitionPlan PartitionPlan => _installationConfigService.PartitionPlan;

    public bool IsDistroSelected => SelectedDistro != null;
    public bool IsWorkflowSelected => SelectedWorkflow != default;
    public bool IsUserInfoAvailable => UserInfo != null;
    public bool IsPartitionPlanAvailable => PartitionPlan != null;

    public List<KeyValuePair<string, string>> PartitionSummaryContent
    {
        get
        {
            var content = new List<KeyValuePair<string, string>>();
            if (PartitionPlan != null)
            {
                content.Add(new("Target Disk", PartitionPlan.TargetDisk!.Name));
                foreach (var part in PartitionPlan.PartitionHistory.Last().Where(p => !string.IsNullOrWhiteSpace(p.MountPoint)))
                {
                    content.Add(new($"Mountpoint {part.MountPoint}", $"{part.Name} - {FS.ToString(part.FileSystem)} - {FileSizeConverter.ToUnit(part.Size)}"));
                }
            }
            return content;
        }
    }

    public ObservableCollection<SummaryItem> SummaryItems
    {
        get
        {
            if (_installationConfigService.SelectedInstallWorkflow == InstallWorkflowType.Iso)
            {
                return [
                    new SummaryItem
                    {
                        Title = "Selected ISO Image",
                        Icon = "\uE019",
                        Content = [
                            new KeyValuePair<string, string>("Path", _installationConfigService.SelectedIsoPath!)
                        ],
                        Action = new() {
                            Label = "Edit",
                            Icon = "\uE3C9",
                            Callback = BackToStartCommand,
                        }
                    }
                ];
            }
            return [
                new SummaryItem
                {
                    Title = "Distro",
                    Icon = "\uE019",
                    Content = [
                        new KeyValuePair<string, string>("Name", SelectedDistro!.DistroName),
                        new KeyValuePair<string, string>("Description", SelectedDistro!.Description),
                        new KeyValuePair<string, string>("Size", $"{FileSizeConverter.ToUnit(SelectedDistro!.Size)}")
                    ],
                    Action = new() {
                        Label = "Edit",
                        Icon = "\uE3C9",
                        Callback = GoToDistroPickerCommand,
                    }
                },
                new SummaryItem
                {
                    Title = "User Account",
                    Icon = "\uE31E",
                    Content = [
                        new KeyValuePair<string, string>("Full Name", UserInfo.FullName ?? UserInfo.Username),
                        new KeyValuePair<string, string>("Username", UserInfo.Username),
                        new KeyValuePair<string, string>("Auto Login", UserInfo.AutoLogin.ToString()),
                    ],
                    Action = new() {
                        Label = "Edit",
                        Icon = "\uE3C9",
                        Callback = GoToUserEditCommand,
                    }
                },
                new SummaryItem
                {
                    Title = "Partitions",
                    Icon = "\uE161",
                    Content = PartitionSummaryContent,
                    Action = new() {
                        Label = "Edit",
                        Icon = "\uE3C9",
                        Callback = GoToDistroPickerCommand,
                    }
                }
            ];
        }
        set { }
    }

    [RelayCommand]
    private void BackToStart()
    {
        Navigation.Reset();
    }

    [RelayCommand]
    private void GoToDistroPicker()
    {
        Navigation.Goto("distroPicker");
    }

    [RelayCommand]
    private void GoToUserEdit()
    {
        Navigation.Goto("userCreation");
    }

    [RelayCommand]
    private async Task Install()
    {
        var dialog = new ConfirmationDialogView();
        dialog.DataContext = new ConfirmationDialogViewModel("This will start the installation process.\nAre you sure you want to continue?", dialog);

        if (Avalonia.Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            bool result = await dialog.ShowDialog<bool>(owner: desktop.MainWindow!);
            if (result)
            {
                Navigation.Next();
            }
        }
    }

    [RelayCommand]
    private void Back()
    {
        Navigation.Previous();
    }

    // INavigatableViewModel Implementation
    public override bool CanProceed => true; // Assume always can proceed to start installation
    public override bool CanGoBack => true; // Assume always can go back to review/edit
}
