using System.Reactive;
using System.Threading.Tasks;
using ASLET.Models;
using ReactiveUI;

namespace ASLET.ViewModels;

public class TeachersDialogViewModel : ViewModelBase
{
    public ReactiveCommand<Unit, TeacherModel> AddTeacherCommand { get; }
    public ReactiveCommand<Unit, TeacherModel?> CancelCommand { get; }

    private string _teacherName;

    #region DarkMode

    private bool _darkMode;
    public bool DarkMode
    {
        get => _darkMode;
        set => this.RaiseAndSetIfChanged(ref _darkMode, value);
    }
    #endregion

    
    public string TeacherName
    {
        get => _teacherName;
        set => this.RaiseAndSetIfChanged(ref _teacherName, value);
    }

    public TeachersDialogViewModel(bool darkMode)
    {
        // TODO CHECKERS FOR VALID INPUT
        AddTeacherCommand = ReactiveCommand.CreateFromTask(() => Task.FromResult(new TeacherModel(_teacherName)));

        CancelCommand = ReactiveCommand.CreateFromTask(() => Task.FromResult<TeacherModel?>(null));

        DarkMode = darkMode; 
        
        TeacherName = "";
    }
}