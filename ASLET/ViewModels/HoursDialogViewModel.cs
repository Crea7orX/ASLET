using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using ASLET.Models;
using ASLET.Services;
using ReactiveUI;

namespace ASLET.ViewModels;

public class HoursDialogViewModel : ViewModelBase
{
    public ReactiveCommand<Unit, SubjectClassModel> AddHourCommand { get; }
    
    private bool _addHourEnabled;
    public bool AddHourEnabled
    {
        get => _addHourEnabled;
        private set => this.RaiseAndSetIfChanged(ref _addHourEnabled, value);
    }
    
    public ReactiveCommand<Unit, SubjectClassModel?> CancelCommand { get; }

    public ObservableCollection<StudentsGroupModel> Classes { get; } = new();
    public ObservableCollection<TeacherModel> Teachers { get; } = new();
    public ObservableCollection<SubjectModel> Subjects { get; } = new();

    private StudentsGroupModel _selectedClass;

    public StudentsGroupModel SelectedClass
    {
        get => _selectedClass;
        private set
        {
            this.RaiseAndSetIfChanged(ref _selectedClass, value);
            ValidateInput();
        }
    }

    private TeacherModel _selectedTeacher;

    public TeacherModel SelectedTeacher
    {
        get => _selectedTeacher;
        private set
        {
            this.RaiseAndSetIfChanged(ref _selectedTeacher, value);
            ValidateInput();
        }
    }

    private SubjectModel _selectedSubject;

    public SubjectModel SelectedSubject
    {
        get => _selectedSubject;
        private set
        {
            this.RaiseAndSetIfChanged(ref _selectedSubject, value);
            ValidateInput();
        }
    }

    private byte _hoursAWeek;

    public byte HoursAWeek
    {
        get => _hoursAWeek;
        private set => this.RaiseAndSetIfChanged(ref _hoursAWeek, value);
    }
    
    private bool _requireLaboratory;

    public bool RequireLaboratory
    {
        get => _requireLaboratory;
        private set => this.RaiseAndSetIfChanged(ref _requireLaboratory, value);
    }

    #region DarkMode

    private bool _darkMode;

    public bool DarkMode
    {
        get => _darkMode;
        set => this.RaiseAndSetIfChanged(ref _darkMode, value);
    }

    #endregion
    
    public HoursDialogViewModel(bool darkMode)
    {
        AddHourCommand = ReactiveCommand.CreateFromTask(() =>
            Task.FromResult(new SubjectClassModel(_selectedTeacher, _selectedSubject, _requireLaboratory, _hoursAWeek, _selectedClass)));

        CancelCommand = ReactiveCommand.CreateFromTask(() => Task.FromResult<SubjectClassModel?>(null));

        DarkMode = darkMode;

        FillClasses();
        FillTeachers();
        FillSubjects();
        
        HoursAWeek = 1;
    }

    private void ValidateInput()
    {
        if (_selectedClass != null && _selectedTeacher != null && _selectedSubject != null) AddHourEnabled = true;
        else AddHourEnabled = false;
    }

    private void FillClasses()
    {
        foreach (StudentsGroupModel @class in ConfigurationService.Instance.GetGroups())
        {
            Classes.Add(@class);
        }
        
        if (Classes[0] != null) SelectedClass = Classes[0];
    }

    private void FillTeachers()
    {
        foreach (TeacherModel teacher in ConfigurationService.Instance.GetTeachers())
        {
            Teachers.Add(teacher);
        }
        
        if (Teachers[0] != null) SelectedTeacher = Teachers[0];
    }

    private void FillSubjects()
    {
        foreach (SubjectModel subject in ConfigurationService.Instance.GetSubjects())
        {
            Subjects.Add(subject);
        }

        if (Subjects[0] != null) SelectedSubject = Subjects[0];
    }
}