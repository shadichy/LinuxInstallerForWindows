namespace LinuxInstaller.Models;

public enum ButtonVariant
{
    Text,
    Outlined,
    Filled,
    Tonal,
    Elevated
}

public enum ButtonSize
{
    ExtraSmall,
    Small,
    Medium,
    Large,
    ExtraLarge
}

public enum ButtonShape
{
    Pill,
    Square
}

public class ButtonStyles
{
    public ButtonVariant Variant { get; set; } = ButtonVariant.Filled;
    public ButtonSize Size { get; set; } = ButtonSize.Medium;
    public ButtonShape Shape { get; set; } = ButtonShape.Pill;
}