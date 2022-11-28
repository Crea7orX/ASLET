using ASLET.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;

namespace ASLET.Views;

public partial class TimetablesView : ReactiveUserControl<TimetablesViewModel>
{
    private static TimetablesView? _instance;

    public static TimetablesView? GetInstance(TimetablesViewModel viewModel)
    {
        if (_instance == null)
        {
            _instance = new TimetablesView
            {
                DataContext = viewModel
            };
        }

        return _instance;
    }

    public TimetablesView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}