using ASLET.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;

namespace ASLET.Views;

public partial class TeachersView : ReactiveUserControl<TeachersViewModel>
{
    private static TeachersView? _instance;

    public static TeachersView? GetInstance(TeachersViewModel viewModel)
    {
        if (_instance == null)
        {
            _instance = new TeachersView
            {
                DataContext = viewModel
            };
        }

        return _instance;
    }

    public TeachersView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}