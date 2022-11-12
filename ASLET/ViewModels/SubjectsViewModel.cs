using System;
using System.Reactive.Linq;
using System.Windows.Input;
using ASLET.Models;
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

    public SubjectsViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;
        
        AddSubject = new Interaction<SubjectsDialogViewModel, SubjectModel?>();
        AddSubjectCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            SubjectModel? result = await AddSubject.Handle(new SubjectsDialogViewModel());

            // TODO ADD TEACHER
            Console.WriteLine(result);
        });
    }
}