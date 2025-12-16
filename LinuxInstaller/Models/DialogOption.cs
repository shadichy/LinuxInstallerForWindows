namespace LinuxInstaller.Models;

public class DialogOption<T>
{
    public required string Label { get; set; }
    public required T Value { get; set; }
    public ButtonStyles ButtonStyles { get; set; } = new ButtonStyles();
}

