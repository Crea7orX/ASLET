using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ASLET.Models;
using ASLET.Services;
using ReactiveUI;

namespace ASLET.ViewModels;

public class HomeViewModel : ViewModelBase, IRoutableViewModel
{
    #region Routing

    private static HomeViewModel? _instance;

    public static HomeViewModel GetInstance(IScreen? hostScreen)
    {
        if (_instance == null)
        {
            _instance = new HomeViewModel(hostScreen!);
        }

        return _instance;
    }

    public string? UrlPathSegment => "Home";
    public IScreen HostScreen { get; }

    #endregion

    #region Parent-child relations

    private static MainWindowViewModel? _parent;
    public static void SetParent(MainWindowViewModel? parent) => _parent = parent;

    #endregion
    
    #region DarkMode

    private bool _darkMode;
    public bool DarkMode
    {
        get => _darkMode;
        set => this.RaiseAndSetIfChanged(ref _darkMode, value);
    }
    #endregion

    public HomeViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;
    }
}