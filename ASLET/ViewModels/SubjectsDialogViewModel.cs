using System.Reactive;
using System.Threading.Tasks;
using ASLET.Models;
using Microsoft.IdentityModel.Tokens;
using ReactiveUI;

namespace ASLET.ViewModels;

public class SubjectsDialogViewModel : ViewModelBase
{
    public ReactiveCommand<Unit, SubjectModel> AddSubjectCommand { get; }
            
    private bool _addSubjectEnabled;
    public bool AddSubjectEnabled
    {
        get => _addSubjectEnabled;
        private set => this.RaiseAndSetIfChanged(ref _addSubjectEnabled, value);
    }

    public ReactiveCommand<Unit, SubjectModel?> CancelCommand { get; }

    private string _subjectName;

    public string SubjectName
    {
        get => _subjectName;
        set
        {
            this.RaiseAndSetIfChanged(ref _subjectName, value);
            ValidateInput();
        }
    }

    #region DarkMode

    private bool _darkMode;
    public bool DarkMode
    {
        get => _darkMode;
        set => this.RaiseAndSetIfChanged(ref _darkMode, value);
    }
    #endregion

    public SubjectsDialogViewModel(bool darkMode)
    {
        AddSubjectCommand = ReactiveCommand.CreateFromTask(() => Task.FromResult(new SubjectModel(_subjectName)));

        CancelCommand = ReactiveCommand.CreateFromTask(() => Task.FromResult<SubjectModel?>(null));
        
        DarkMode = darkMode;
        
        SubjectName = "";
    }

    private void ValidateInput()
    {
        AddSubjectEnabled = !SubjectName.IsNullOrEmpty();
    }
}