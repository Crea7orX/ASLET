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

public class ClassesViewModel : ViewModelBase, IRoutableViewModel
{
    #region Routing

    private static ClassesViewModel? _instance;

    public static ClassesViewModel GetInstance(IScreen? hostScreen)
    {
        if (_instance == null)
        {
            _instance = new ClassesViewModel(hostScreen!);
        }

        return _instance;
    }

    public string? UrlPathSegment => "Classes";
    public IScreen HostScreen { get; }

    #endregion

    #region Logic

    public ICommand AddClassCommand { get; }

    public Interaction<ClassesDialogViewModel, ClassModel?> AddClass { get; }

    private ClassModel _selectedClass;

    public ClassModel SelectedClass
    {
        get => _selectedClass;
        private set => this.RaiseAndSetIfChanged(ref _selectedClass, value);
    }

    private ObservableCollection<ClassModel> _classes = new();

    public ObservableCollection<ClassModel> Classes
    {
        get => _classes;
        private set => this.RaiseAndSetIfChanged(ref _classes, value);
    }

    public ICommand DeleteClassCommand { get; }

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

    public ClassesViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;

        AddClass = new Interaction<ClassesDialogViewModel, ClassModel?>();
        AddClassCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            ClassModel? result = await AddClass.Handle(new ClassesDialogViewModel());

            if (result != null)
            {
                TimetableService.AddClass(result);
            }
        });

        DeleteClassCommand = ReactiveCommand.CreateFromTask((ClassModel selectedClass) =>
        {
            TimetableService.RemoveClass(selectedClass);
            return Task.CompletedTask;
        });
    }

    public void UpdateClasses(ref ObservableCollection<ClassModel> classes)
    {
        Classes = classes;
    }
}