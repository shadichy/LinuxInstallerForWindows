using CommunityToolkit.Mvvm.ComponentModel;
using LinuxInstaller.Services;

namespace LinuxInstaller.ViewModels.Interfaces;

public interface INavigatableViewModel
{
    bool CanProceed { get; }
    bool CanGoBack { get; }
}

public abstract class NavigatableViewModelBase : ObservableObject, INavigatableViewModel
{
    private readonly NavigationService _navigationService;

    public abstract bool CanProceed { get; }

    public abstract bool CanGoBack { get; }

    public NavigatableViewModelBase(NavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public NavigationService Navigation => _navigationService;
}