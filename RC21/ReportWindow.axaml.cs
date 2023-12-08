using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace RC21;

public partial class ReportWindow : Window
{
    public ReportWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}