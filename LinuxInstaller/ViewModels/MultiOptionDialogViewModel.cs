using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Avalonia.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LinuxInstaller.Models;

namespace LinuxInstaller.ViewModels;

public partial class MultiOptionDialogViewModel<T> : ObservableObject where T : notnull
{
    private readonly Window _dialogWindow;

    [ObservableProperty]
    private string _title = "Select an Option";

    [ObservableProperty]
    private string? _contentText;

    public ObservableCollection<DialogOption<T>> Options { get; }

    public MultiOptionDialogViewModel(string title, string? contentText, IEnumerable<DialogOption<T>> options, Window dialogWindow)
    {
        _title = title;
        _contentText = contentText;
        Options = new ObservableCollection<DialogOption<T>>(options);
        _dialogWindow = dialogWindow;
    }

    [RelayCommand]
    private void SelectOption(DialogOption<T> option)
    {
        _dialogWindow.Close(option.Value);
    }
}
