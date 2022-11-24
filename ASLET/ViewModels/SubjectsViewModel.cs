using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ASLET.Models;
using ReactiveUI;

namespace ASLET.ViewModels;

public class SubjectsViewModel : ViewModelBase, IRoutableViewModel
{
    #region Routing

    private static SubjectsViewModel? _instance;

    public static SubjectsViewModel GetInstance(IScreen? hostScreen)
    {
        if (_instance == null)
        {
            _instance = new SubjectsViewModel(hostScreen!);
        }

        return _instance;
    }

    public string? UrlPathSegment => "Subjects";
    public IScreen HostScreen { get; }

    #endregion

    #region Logic

    public ICommand AddSubjectCommand { get; }

    public Interaction<SubjectsDialogViewModel, SubjectModel?> AddSubject { get; }

    private SubjectModel _selectedSubject;

    public SubjectModel SelectedSubject
    {
        get => _selectedSubject;
        private set => this.RaiseAndSetIfChanged(ref _selectedSubject, value);
    }

    private ObservableCollection<SubjectModel> _subjects = new();

    public ObservableCollection<SubjectModel> Subjects
    {
        get => _subjects;
        private set => this.RaiseAndSetIfChanged(ref _subjects, value);
    }

    public ICommand DeleteSubjectCommand { get; }


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
    
    public SubjectsViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;
        
        AddSubject = new Interaction<SubjectsDialogViewModel, SubjectModel?>();
        AddSubjectCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            SubjectModel? result = await AddSubject.Handle(new SubjectsDialogViewModel());

            // TODO ADD SUBJECT
            Console.WriteLine(result);
            if (result != null) Subjects.Add(result);
        });

        DeleteSubjectCommand = ReactiveCommand.CreateFromTask((SubjectModel selectedSubject) =>
        {
            Subjects.Remove(selectedSubject);
            return Task.CompletedTask;
        });
    }
}