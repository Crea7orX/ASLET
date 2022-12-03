using System.Reactive;
using System.Threading.Tasks;
using ASLET.Models;
using Microsoft.IdentityModel.Tokens;
using ReactiveUI;

namespace ASLET.ViewModels;

public class TeachersDialogViewModel : ViewModelBase
{
    public ReactiveCommand<Unit, TeacherModel> AddTeacherCommand { get; }
                
    private bool _addTeacherEnabled;
    public bool AddTeacherEnabled
    {
        get => _addTeacherEnabled;
        private set => this.RaiseAndSetIfChanged(ref _addTeacherEnabled, value);
    }

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
        set
        {
            this.RaiseAndSetIfChanged(ref _teacherName, value);
            ValidateInput();
        }
    }

    public TeachersDialogViewModel(bool darkMode)
    {
        AddTeacherCommand = ReactiveCommand.CreateFromTask(() => Task.FromResult(new TeacherModel(_teacherName, true)));

        CancelCommand = ReactiveCommand.CreateFromTask(() => Task.FromResult<TeacherModel?>(null));

        DarkMode = darkMode; 
        
        TeacherName = "";
    }

    private void ValidateInput()
    {
        AddTeacherEnabled = !TeacherName.IsNullOrEmpty();
    }
}