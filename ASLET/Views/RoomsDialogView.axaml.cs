using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ASLET.Views;

public partial class RoomsDialogView : UserControl
{
    public RoomsDialogView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}