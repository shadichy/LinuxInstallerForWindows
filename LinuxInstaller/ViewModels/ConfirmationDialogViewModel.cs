using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Avalonia.Controls;

namespace LinuxInstaller.ViewModels;

public partial class ConfirmationDialogViewModel : ObservableObject
{
    private readonly Window _dialogWindow;

    [ObservableProperty]
    private string _message;

    public ConfirmationDialogViewModel(string message, Window dialogWindow)
    {
        _message = message;
        _dialogWindow = dialogWindow;
    }

    [RelayCommand]
    private void Confirm()
    {
        _dialogWindow.Close(true);
    }

    [RelayCommand]
    private void Cancel()
    {
        _dialogWindow.Close(false);
    }
}
