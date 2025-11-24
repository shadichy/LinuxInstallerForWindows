using LinuxInstaller.Models;
using CommunityToolkit.Mvvm.ComponentModel; // For ObservableObject if needed, or just INotifyPropertyChanged

namespace LinuxInstaller.Services
{
    // This service will hold shared state/configuration for the installation process.
    public partial class InstallationConfigService : ObservableObject
    {
        private Distro? _selectedDistro;
        public Distro? SelectedDistro
        {
            get => _selectedDistro;
            set => SetProperty(ref _selectedDistro, value);
        }

        private WorkflowType _selectedWorkflow = WorkflowType.None;
        public WorkflowType SelectedWorkflow
        {
            get => _selectedWorkflow;
            set => SetProperty(ref _selectedWorkflow, value);
        }

        private UserInfo _userInfo = new UserInfo();
        public UserInfo UserInfo
        {
            get => _userInfo;
            set => SetProperty(ref _userInfo, value);
        }

        private PartitionPlan _partitionPlan = new PartitionPlan();
        public PartitionPlan PartitionPlan
        {
            get => _partitionPlan;
            set => SetProperty(ref _partitionPlan, value);
        }
    }
}
