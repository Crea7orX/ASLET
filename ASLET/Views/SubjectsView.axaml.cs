using ASLET.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;

namespace ASLET.Views;

public partial class SubjectsView : ReactiveUserControl<SubjectsViewModel>
{
    private static SubjectsView? _instance;

    public static SubjectsView? GetInstance(SubjectsViewModel viewModel)
    {
        if (_instance == null)
        {
            _instance = new SubjectsView
            {
                DataContext = viewModel
            };
        }

        return _instance;
    }

    public SubjectsView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}