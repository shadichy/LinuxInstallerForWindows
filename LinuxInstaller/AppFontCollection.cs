using System;
using Avalonia.Media.Fonts;

namespace LinuxInstaller;

public class AppFontCollection : EmbeddedFontCollection
{
    public AppFontCollection() : base(
            new Uri("fonts:SpaceGrotesk", UriKind.Absolute),
            new Uri("avares://LinuxInstaller/Assets/Fonts/SpaceGrotesk", UriKind.Absolute))
    { }
}
