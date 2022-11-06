using ASLET.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;

namespace ASLET.Views;

public partial class HoursView : ReactiveUserControl<HoursViewModel>
{
    private static HoursView? _instance;

    public static HoursView? GetInstance(HoursViewModel viewModel)
    {
        if (_instance == null)
        {
            _instance = new HoursView
            {
                DataContext = viewModel
            };
        }

        return _instance;
    }

    public HoursView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}