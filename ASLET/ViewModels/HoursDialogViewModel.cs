using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using ASLET.Models;
using ASLET.Services;
using ASLET.Services.Handlers;
using ReactiveUI;

namespace ASLET.ViewModels;

public class HoursDialogViewModel : ViewModelBase
{
    public ReactiveCommand<Unit, HourModel> AddHourCommand { get; }
    public ReactiveCommand<Unit, HourModel?> CancelCommand { get; }

    public ObservableCollection<ClassModel> Classes { get; } = new();
    public ObservableCollection<TeacherModel> Teachers { get; } = new();
    public ObservableCollection<SubjectModel> Subjects { get; } = new();

    private ClassModel _selectedClass;

    public ClassModel SelectedClass
    {
        get => _selectedClass;
        private set => this.RaiseAndSetIfChanged(ref _selectedClass, value);
    }

    private TeacherModel _selectedTeacher;

    public TeacherModel SelectedTeacher
    {
        get => _selectedTeacher;
        private set => this.RaiseAndSetIfChanged(ref _selectedTeacher, value);
    }

    private SubjectModel _selectedSubject;

    public SubjectModel SelectedSubject
    {
        get => _selectedSubject;
        private set => this.RaiseAndSetIfChanged(ref _selectedSubject, value);
    }

    private byte _hoursAWeek;

    public byte HoursAWeek
    {
        get => _hoursAWeek;
        private set => this.RaiseAndSetIfChanged(ref _hoursAWeek, value);
    }

    public HoursDialogViewModel()
    {
        // TODO CHECKERS FOR VALID INPUT
        AddHourCommand = ReactiveCommand.CreateFromTask(() => Task.FromResult(new HourModel(_selectedClass.ClassId, _selectedClass.Grade, _selectedClass.Letter, _selectedTeacher.TeacherId, _selectedTeacher.Name, _selectedSubject.SubjectId, _selectedSubject.Name, _hoursAWeek)));

        CancelCommand = ReactiveCommand.CreateFromTask(() => Task.FromResult<HourModel?>(null));

        FillClasses();
        FillTeachers();
        FillSubjects();
        HoursAWeek = 1;
    }

    private void FillClasses()
    {
        foreach (ClassModel @class in TimetableService.GetClasses())
        {
            Classes.Add(@class);
        }
        // foreach (string className in GlobalSpace.ClassController.Classes.Keys)
        // {
        //     if (className.Length == 2)
        //     {
        //         Classes.Add(new ClassModel(Byte.Parse(className.Substring(0, 1)), Char.Parse(className.Substring(1, 1))));
        //     }
        //     else if (className.Length == 3)
        //     {
        //         Classes.Add(new ClassModel(Byte.Parse(className.Substring(0, 2)), Char.Parse(className.Substring(2, 1))));
        //     }
        // }
        SelectedClass = Classes[0];
    }

    private void FillTeachers()
    {
        foreach (TeacherModel teacher in TimetableService.GetTeachers())
        {
            Teachers.Add(teacher);
        }
        // foreach (string teacherName in GlobalSpace.TeacherController.Teachers)
        // {
        //     Teachers.Add(new TeacherModel(teacherName));
        // }
        SelectedTeacher = Teachers[0];
    }

    private void FillSubjects()
    {
        foreach (SubjectModel subject in TimetableService.GetSubjects())
        {
            Subjects.Add(subject);
        }
        // foreach (string subjectName in GlobalSpace.SubjectController.SubjectsDictionary.Keys)
        // {
        //     Subjects.Add(new SubjectModel(subjectName));
        // }
        SelectedSubject = Subjects[0];
    }
}