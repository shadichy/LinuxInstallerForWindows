using Avalonia;
using Avalonia.Media;

namespace LinuxInstaller;

public static class AppFontBuilderExtension
{
    public static AppBuilder WithAppFonts(this AppBuilder builder)
    {
        return builder.ConfigureFonts(fontManager =>
        {
            fontManager.AddFontCollection(new AppFontCollection());
        });
    }
}