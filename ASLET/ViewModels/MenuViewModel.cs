using System.Reactive;
using ReactiveUI;

namespace ASLET.ViewModels;

public class MenuViewModel : ViewModelBase
{
    
    #region Private fields
    
    private bool _userSettingsIsOpen;
    private static MainWindowViewModel? _parent;
    
    #endregion

    #region Dark Mode
    
    private bool _darkMode;

    public bool DarkMode
    {
        get => _darkMode;
        set
        {
            _parent?.ToggleDarkMode(value);
            this.RaiseAndSetIfChanged(ref _darkMode, value);
        } 
    }
    
    public void ToggleDarMode() => DarkMode ^= true;
    
    #endregion

    #region Public props

    public ReactiveCommand<Unit, Unit> ToggleDropup { get; }
    
    public bool UserSettingsIsOpen
    {
        get => _userSettingsIsOpen;
        set => this.RaiseAndSetIfChanged(ref _userSettingsIsOpen, value);
    }

    

    #endregion

    #region Constructors

    public MenuViewModel()
    {
        DarkMode = false;
        UserSettingsIsOpen = false;
        ToggleDropup = ReactiveCommand.Create(() =>  ToggleDropupMenu());
    }

    #endregion

    #region Functions

    #region Public Functions

    public static void SetParent(MainWindowViewModel? parent) => _parent = parent;

    public void GoToClasses() => _parent?.GoToClasses();
    public void GoToTeachers() => _parent?.GoToTeachers();
    public void GoToSubjects() => _parent?.GoToSubjects();
    public void GoToHours() => _parent?.GoToHours();

    public void GoToTimetables() => _parent?.GoToTimetables();
    

    #endregion
    
    #region Private functions

    private void ToggleDropupMenu() => UserSettingsIsOpen ^= true;

    #endregion

    #endregion
    
}