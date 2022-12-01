using System;
using System.Threading.Tasks;
using ASLET.ViewModels;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace ASLET.Views;

public partial class HomeView : ReactiveUserControl<HomeViewModel>
{
    private static HomeView? _instance;

    public static HomeView? GetInstance(HomeViewModel viewModel)
    {
        if (_instance == null)
        {
            _instance = new HomeView
            {
                DataContext = viewModel
            };
        }

        return _instance;
    }

    public HomeView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}