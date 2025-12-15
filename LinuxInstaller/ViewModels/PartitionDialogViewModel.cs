using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LinuxInstaller.Models;
using Avalonia.Controls;
using System.Collections.Generic;
using System;
using System.Linq;

namespace LinuxInstaller.ViewModels;

public partial class PartitionDialogViewModel : ObservableObject
{
    private readonly Window _dialogWindow;

    [ObservableProperty]
    private PlannedPartition _targetPartition;

    [ObservableProperty]
    private decimal _size;

    [ObservableProperty]
    private string _selectedSizeUnit = AvailableSizeUnits[0];

    [ObservableProperty]
    private decimal _freeSpaceBefore = 0; // Decimal value for free space before

    [ObservableProperty]
    private decimal _freeSpaceAfter = 0; // Decimal value for free space after

    [ObservableProperty]
    private decimal _maxSize;

    [ObservableProperty]
    private bool _isNew = false;

    [ObservableProperty]
    private string _title;

    public static List<string> AvailableFileSystems { get; } = [.. Enum.GetValues<FileSystem>().Select(FS.ToString)];
    public static List<string> AvailableSizeUnits { get; } = ["MB", "GB"];

    public PartitionDialogViewModel(Window dialogWindow, ChartSpace space, int index = 0, bool hasRoot = false)
    {
        _dialogWindow = dialogWindow;
        SelectedSizeUnit = "MB"; // Default unit for all size-related fields

        if (space is ChartPartition ps)
        {
            TargetPartition = ps.Partition.Clone();
            Title = "Edit Partition";
        }
        else
        {
            IsNew = true;
            TargetPartition = new PlannedPartition()
            {
                Id = index == 0 ? $"part{(new Random()).Next(99999, 10000000)}" : $"part{index}",
                Name = "New Partition " + (index == 0 ? "" : $"{index}"),
                StartOffset = space.Start,
                Size = space.Size, // Initial size is the whole free space
                FileSystem = FileSystem.LINUX, // Default to ext4
                IsSystem = false,
                MountPoint = hasRoot ? "" : "/" // Default mount point
            };
            Title = "Add New Partition";
        }

        // Initialize display values based on TargetPartition and initial space
        var max = BytesToUnit(TargetPartition.Size, SelectedSizeUnit);
        _fsaLock = true;
        _size = max;
        MaxSize = max;
    }

    private bool _sizeLock = false;
    private bool _fsaLock = false;

    partial void OnSelectedSizeUnitChanged(string? oldValue, string newValue)
    {
        if (oldValue == null) oldValue = AvailableSizeUnits[0];
        MaxSize = BytesToUnit(UnitToBytes(MaxSize, oldValue), newValue);
        // Update Size, FreeSpaceBefore, and FreeSpaceAfter to new unit
        _sizeLock = true;
        FreeSpaceBefore = BytesToUnit(UnitToBytes(FreeSpaceBefore, oldValue), newValue);
        Size = BytesToUnit(UnitToBytes(Size, oldValue), newValue);
        FreeSpaceAfter = BytesToUnit(UnitToBytes(FreeSpaceAfter, oldValue), newValue);
    }

    partial void OnFreeSpaceBeforeChanged(decimal oldValue, decimal newValue)
    {
        _fsaLock = true;
        Size -= (newValue - oldValue);
    }

    partial void OnFreeSpaceAfterChanged(decimal oldValue, decimal newValue)
    {
        if (_sizeLock)
        {
            _sizeLock = false;
            return;
        }
        _fsaLock = true;
        Size = MaxSize - FreeSpaceBefore - FreeSpaceAfter;
    }

    partial void OnSizeChanged(decimal oldValue, decimal newValue)
    {
        if (_fsaLock)
        {
            _fsaLock = false;
            return;
        }
        _sizeLock = true;
        FreeSpaceAfter = MaxSize - FreeSpaceBefore - Size;
    }


    private static decimal BytesToUnit(ulong bytes, string unit) => unit switch
    {
        "MB" => bytes / (decimal)(1024 * 1024),
        "GB" => bytes / (decimal)(1024 * 1024 * 1024),
        _ => throw new ArgumentOutOfRangeException(nameof(unit), $"Unknown unit: {unit}")
    };

    private static ulong UnitToBytes(decimal value, string unit) => unit switch
    {
        "MB" => (ulong)(value * 1024 * 1024),
        "GB" => (ulong)(value * 1024 * 1024 * 1024),
        _ => throw new ArgumentOutOfRangeException(nameof(unit), $"Unknown unit: {unit}")
    };

    [RelayCommand]
    private void Ok()
    {
        TargetPartition.StartOffset = TargetPartition.StartOffset + UnitToBytes(FreeSpaceBefore, SelectedSizeUnit);
        TargetPartition.Size = UnitToBytes(Size, SelectedSizeUnit);
        _dialogWindow.Close(TargetPartition);
    }

    [RelayCommand]
    private void Cancel()
    {
        _dialogWindow.Close(null);
    }
}