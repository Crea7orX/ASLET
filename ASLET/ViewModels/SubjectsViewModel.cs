using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ASLET.Models;
using ASLET.Services;
using ReactiveUI;

namespace ASLET.ViewModels;

public class SubjectsViewModel : ViewModelBase, IRoutableViewModel
{
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

    public SubjectsViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;
        
        AddSubject = new Interaction<SubjectsDialogViewModel, SubjectModel?>();
        AddSubjectCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            SubjectModel? result = await AddSubject.Handle(new SubjectsDialogViewModel());

            if (result != null)
            {
                TimetableService.AddSubject(result);
            }
        });

        DeleteSubjectCommand = ReactiveCommand.CreateFromTask((SubjectModel selectedSubject) =>
        {
            TimetableService.RemoveSubject(selectedSubject);
            return Task.CompletedTask;
        });
    }

    public void UpdateSubjects(ref ObservableCollection<SubjectModel> subjects)
    {
        Subjects = subjects;
    }
}