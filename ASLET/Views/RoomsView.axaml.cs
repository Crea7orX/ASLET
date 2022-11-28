using ASLET.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;

namespace ASLET.Views;

public partial class RoomsView : ReactiveUserControl<RoomsViewModel>
{
    private static RoomsView? _instance;

    public static RoomsView? GetInstance(RoomsViewModel viewModel)
    {
        if (_instance == null)
        {
            _instance = new RoomsView
            {
                DataContext = viewModel
            };
        }

        return _instance;
    }

    public RoomsView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}