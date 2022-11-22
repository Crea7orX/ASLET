using System;
using System.ComponentModel;
using System.Reactive;
using Avalonia.Controls;
using Avalonia.Threading;
using ReactiveUI;

namespace ASLET.ViewModels;

public class MenuViewModel : ViewModelBase, INotifyPropertyChanged
{

    private bool _userSettingsIsOpen = false;
    
    public bool UserSettingsIsOpen
    {
        get => _userSettingsIsOpen;
        set => this.RaiseAndSetIfChanged(ref _userSettingsIsOpen, value);
    }

    private static MainWindowViewModel _Parent;
    public static void Parent(MainWindowViewModel parent) => _Parent = parent;

    
    public ReactiveCommand<Unit, Unit> LocalTest { get; }


    public MenuViewModel()
    {
        UserSettingsIsOpen = false;
        LocalTest = ReactiveCommand.Create(() =>  ToggleDropupMenu());
    }


    public void GoToClasses() => _Parent.GoToClasses();
    public void GoToTeachers() => _Parent.GoToTeachers();
    public void GoToSubjects() => _Parent.GoToSubjects();
    public void GoToHours() => _Parent.GoToHours();

    private void ToggleDropupMenu()
    {
        UserSettingsIsOpen ^= true;
        Console.WriteLine(UserSettingsIsOpen);
    }
}