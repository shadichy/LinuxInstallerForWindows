using LinuxInstaller.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Subjects;

namespace LinuxInstaller.Services;

public class NavigationService
{
    private readonly BehaviorSubject<UInt16> _currentPageIndex = new(0);
    private readonly List<UInt16> _pageHistory = new() { 0 };
    private readonly ObservableCollection<KeyValuePair<string, NavigatableViewModelBase>> _routes = [];

    public IObservable<UInt16> CurrentPageIndexObservable => _currentPageIndex;
    public UInt16 CurrentPageIndex => _pageHistory.Last();

    public int PageCount => _routes.Count;

    public NavigatableViewModelBase CurrentPage => _routes[CurrentPageIndex].Value;

    public void SetupRoutes(IEnumerable<KeyValuePair<string, NavigatableViewModelBase>> pages)
    {
        _routes.Clear();
        foreach (var page in pages)
        {
            _routes.Add(page);
        }
    }

    public void Next(UInt16 count = 1)
    {
        var nextPageIndex = (UInt16)(CurrentPageIndex + count);
        if (nextPageIndex < PageCount)
        {
            _pageHistory.Add(nextPageIndex);
            _currentPageIndex.OnNext(nextPageIndex);
        }
    }

    public void Previous(UInt16 count = 1)
    {
        if (_pageHistory.Count > count)
        {
            _pageHistory.RemoveRange(_pageHistory.Count - count, count);
            _currentPageIndex.OnNext(CurrentPageIndex);
        }
    }

    public void Goto(UInt16 index)
    {
        if (index < PageCount)
        {
            _pageHistory.Add(index);
            _currentPageIndex.OnNext(index);
        }
    }

    public void Goto(string routeName)
    {
        var index = _routes.ToList().FindIndex(r => r.Key == routeName);
        if (index >= 0)
        {
            Goto((UInt16)index);
        }
    }

    public void Reset()
    {
        _routes.Clear();
        _pageHistory.Clear();
        _pageHistory.Add(0);
        _currentPageIndex.OnNext(0);
    }
}

