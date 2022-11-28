using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ASLET.Views;

public partial class SubjectsDialogView : UserControl
{
    public SubjectsDialogView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}