using LinuxInstaller.Models;
using CommunityToolkit.Mvvm.ComponentModel; // For ObservableObject if needed, or just INotifyPropertyChanged

namespace LinuxInstaller.Services
{
    // This service will hold shared state/configuration for the installation process.
    public partial class InstallationConfigService : ObservableObject
    {
        private InstallWorkflowType _selectedInstallWorkflow = InstallWorkflowType.None;
        public InstallWorkflowType SelectedInstallWorkflow
        {
            get => _selectedInstallWorkflow;
            set => SetProperty(ref _selectedInstallWorkflow, value);
        }

        private Distro? _selectedDistro;
        public Distro? SelectedDistro
        {
            get => _selectedDistro;
            set => SetProperty(ref _selectedDistro, value);
        }

        private PartitionWorkflowType _selectedPartitionWorkflow = PartitionWorkflowType.None;
        public PartitionWorkflowType SelectedPartitionWorkflow
        {
            get => _selectedPartitionWorkflow;
            set => SetProperty(ref _selectedPartitionWorkflow, value);
        }

        private UserInfo _userInfo = new();
        public UserInfo UserInfo
        {
            get => _userInfo;
            set => SetProperty(ref _userInfo, value);
        }

        private PartitionPlan _partitionPlan = new();
        public PartitionPlan PartitionPlan
        {
            get => _partitionPlan;
            set => SetProperty(ref _partitionPlan, value);
        }
    }
}
