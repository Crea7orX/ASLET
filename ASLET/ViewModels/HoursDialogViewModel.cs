using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using ASLET.Models;
using ReactiveUI;

namespace ASLET.ViewModels;

public class HoursDialogViewModel : ViewModelBase
{
    public ReactiveCommand<Unit, HourModel> AddHourCommand { get; }
    public ReactiveCommand<Unit, HourModel?> CancelCommand { get; }

    public ObservableCollection<ClassModel> Classes = new();
    public ObservableCollection<TeacherModel> Teachers = new();
    public ObservableCollection<SubjectModel> Subjects = new();

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
        AddHourCommand = ReactiveCommand.CreateFromTask(() => Task.FromResult(new HourModel(_selectedClass.Grade, _selectedClass.Letter, _selectedTeacher.Name, _selectedSubject.Name, _hoursAWeek)));

        CancelCommand = ReactiveCommand.CreateFromTask(() => Task.FromResult<HourModel?>(null));

        FillClasses();
        FillTeachers();
        FillSubjects();
        HoursAWeek = 1;
    }

    private void FillClasses()
    {
        Classes.Add(new ClassModel(8, 'А'));
        Classes.Add(new ClassModel(8, 'Б'));
        SelectedClass = Classes[0];
    }

    private void FillTeachers()
    {
        Teachers.Add(new TeacherModel("Учител 1"));
        Teachers.Add(new TeacherModel("Учител 2"));
        SelectedTeacher = Teachers[0];
    }

    private void FillSubjects()
    {
        Subjects.Add(new SubjectModel("Предмет 1"));
        Subjects.Add(new SubjectModel("Предмет 2"));
        SelectedSubject = Subjects[0];
    }
}