using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ASLET.Views;

public partial class ClassesDialogWindow : Window
{
    public ClassesDialogWindow()
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