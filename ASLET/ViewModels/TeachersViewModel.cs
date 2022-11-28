using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ASLET.Models;
using ASLET.Services;
using ReactiveUI;

namespace ASLET.ViewModels;

public class TeachersViewModel : ViewModelBase, IRoutableViewModel
{
    #region Routing

    private static TeachersViewModel? _instance;

    public static TeachersViewModel GetInstance(IScreen? hostScreen)
    {
        if (_instance == null)
        {
            _instance = new TeachersViewModel(hostScreen!);
        }
        
        return _instance;
    }

    public string? UrlPathSegment => "Teachers";
    public IScreen HostScreen { get; }

    #endregion

    #region Logic

    public ICommand AddTeacherCommand { get; }

    public Interaction<TeachersDialogViewModel, ProfessorModel?> AddTeacher { get; }

    private ProfessorModel _selectedTeacher;

    public ProfessorModel SelectedTeacher
    {
        get => _selectedTeacher;
        private set => this.RaiseAndSetIfChanged(ref _selectedTeacher, value);
    }

    private ObservableCollection<ProfessorModel> _teachers = new();

    public ObservableCollection<ProfessorModel> Teachers
    {
        get => _teachers;
        private set => this.RaiseAndSetIfChanged(ref _teachers, value);
    }

    public ICommand DeleteTeacherCommand { get; }

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

    public TeachersViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;
        
        AddTeacher = new Interaction<TeachersDialogViewModel, ProfessorModel?>();
        AddTeacherCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            ProfessorModel? result = await AddTeacher.Handle(new TeachersDialogViewModel(DarkMode));

            if (result != null)
            {
                ConfigurationService.Instance.AddTeacher(result);
            }
        });

        DeleteTeacherCommand = ReactiveCommand.CreateFromTask((ProfessorModel selectedTeacher) =>
        {
            ConfigurationService.Instance.RemoveTeacher(selectedTeacher);
            return Task.CompletedTask;
        });
    }

    public void UpdateTeachers(ref ObservableCollection<ProfessorModel> teachers)
    {
        Teachers = teachers;
    }
}