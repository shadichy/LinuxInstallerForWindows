using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LinuxInstaller.Models;

public class ActionButton
{
    public string? Icon { get; set; }
    public required string Label { get; set; }
    public ICommand? Callback { get; set; }
}

public class SummaryItem
{
    public string? Icon { get; set; }
    public required string Title { get; set; }
    public required List<KeyValuePair<string, string>> Content { get; set; }
    public ActionButton? Action { get; set; }
}
