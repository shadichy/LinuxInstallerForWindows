using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Windows.Input;

namespace LinuxInstaller.UserControls
{
    public partial class PageHeaderControl : UserControl
    {
        public PageHeaderControl()
        {
            InitializeComponent();
        }

        public static readonly StyledProperty<string?> TitleProperty =
            AvaloniaProperty.Register<PageHeaderControl, string?>(nameof(Title));

        public string? Title
        {
            get => GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly StyledProperty<string?> SubtitleProperty =
            AvaloniaProperty.Register<PageHeaderControl, string?>(nameof(Subtitle));

        public string? Subtitle
        {
            get => GetValue(SubtitleProperty);
            set => SetValue(SubtitleProperty, value);
        }

        public static readonly StyledProperty<ICommand?> BackCommandProperty =
            AvaloniaProperty.Register<PageHeaderControl, ICommand?>(nameof(BackCommand));

        public ICommand? BackCommand
        {
            get => GetValue(BackCommandProperty);
            set => SetValue(BackCommandProperty, value);
        }

        public static readonly StyledProperty<object?> RightContentProperty =
            AvaloniaProperty.Register<PageHeaderControl, object?>(nameof(RightContent));

        public object? RightContent
        {
            get => GetValue(RightContentProperty);
            set => SetValue(RightContentProperty, value);
        }
    }
}
