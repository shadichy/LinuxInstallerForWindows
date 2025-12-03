using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinuxInstaller.Services;

public class NavigationService
{
    public NavigationService()
    {
    }

    private int _pageIndex = 0;
    public int PageIndex => _pageIndex;

    public void Next() => _pageIndex++;
    public void Previous() => _pageIndex--;
}
