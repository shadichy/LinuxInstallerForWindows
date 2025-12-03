namespace LinuxInstaller.ViewModels.Interfaces
{
    public interface INavigatableViewModel
    {
        bool CanProceed { get; }
        bool CanGoBack { get; }
    }
}
