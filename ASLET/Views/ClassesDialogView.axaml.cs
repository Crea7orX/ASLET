using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ASLET.Views;

public partial class ClassesDialogView : UserControl
{
    public ClassesDialogView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}