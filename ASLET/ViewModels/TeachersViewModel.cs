using System;
using System.Reactive.Linq;
using System.Windows.Input;
using ASLET.Models;
using ReactiveUI;

namespace ASLET.ViewModels;

public class TeachersViewModel : ViewModelBase, IRoutableViewModel
{
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

    public ICommand AddTeacherCommand { get; }

    public Interaction<TeachersDialogViewModel, TeacherModel?> AddTeacher { get; }

    public TeachersViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;
        
        AddTeacher = new Interaction<TeachersDialogViewModel, TeacherModel?>();
        AddTeacherCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            TeacherModel? result = await AddTeacher.Handle(new TeachersDialogViewModel());

            // TODO ADD TEACHER
            Console.WriteLine(result);
        });
    }
}