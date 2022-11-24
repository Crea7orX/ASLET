using System.Reactive;
using ASLET.Models;
using ASLET.Services;
using ReactiveUI;

namespace ASLET.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IScreen
    {
        public RoutingState Router { get; } = new RoutingState();
        public ReactiveCommand<Unit, IRoutableViewModel> GoToClasses { get; }
        public ReactiveCommand<Unit, IRoutableViewModel> GoToTeachers { get; }
        public ReactiveCommand<Unit, IRoutableViewModel> GoToSubjects { get; }
        public ReactiveCommand<Unit, IRoutableViewModel> GoToHours { get; }
        public ReactiveCommand<Unit, IRoutableViewModel> GoToTimetables { get; }

        public MainWindowViewModel()
        {
            GoToClasses = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(ClassesViewModel.GetInstance(this))
            );
            GoToTeachers = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(TeachersViewModel.GetInstance(this))
            );
            GoToSubjects = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(SubjectsViewModel.GetInstance(this))
            );
            GoToHours = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(HoursViewModel.GetInstance(this))
            );
            GoToTimetables = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(TimetablesViewModel.GetInstance(this))
            );

            GoToClasses.Execute();

            ClassModel class1 = new ClassModel(1, 'А');
            ClassModel class2 = new ClassModel(2, 'А');
            ClassModel class3 = new ClassModel(1, 'Б');
            TimetableService.AddClass(class1);
            TimetableService.AddClass(class2);
            TimetableService.AddClass(class3);

            TeacherModel teacher1 = new TeacherModel("Учител 1");
            TeacherModel teacher2 = new TeacherModel("Учител 2");
            TimetableService.AddTeacher(teacher1);
            TimetableService.AddTeacher(teacher2);

            SubjectModel subject1 = new SubjectModel("Предмет 1");
            SubjectModel subject2 = new SubjectModel("Предмет 2");
            TimetableService.AddSubject(subject1);
            TimetableService.AddSubject(subject2);

            HourModel hour1 = new HourModel(class1.ClassId, class1.Grade, class1.Letter, teacher1.TeacherId,
                teacher1.Name, subject1.SubjectId, subject1.Name, 2);
            HourModel hour2 = new HourModel(class1.ClassId, class1.Grade, class1.Letter, teacher2.TeacherId,
                teacher2.Name, subject2.SubjectId, subject2.Name, 4);
            HourModel hour3 = new HourModel(class2.ClassId, class2.Grade, class2.Letter, teacher1.TeacherId,
                teacher1.Name, subject1.SubjectId, subject1.Name, 2);
            HourModel hour4 = new HourModel(class2.ClassId, class2.Grade, class1.Letter, teacher2.TeacherId,
                teacher2.Name, subject2.SubjectId, subject2.Name, 4);
            HourModel hour5 = new HourModel(class3.ClassId, class3.Grade, class3.Letter, teacher1.TeacherId,
                teacher1.Name, subject2.SubjectId, subject2.Name, 5);
            HourModel hour6 = new HourModel(class1.ClassId, class1.Grade, class1.Letter, teacher2.TeacherId,
                teacher2.Name, subject2.SubjectId, subject2.Name, 5);
            TimetableService.AddHour(hour1);
            TimetableService.AddHour(hour2);
            TimetableService.AddHour(hour3);
            TimetableService.AddHour(hour4);
            TimetableService.AddHour(hour5);
            TimetableService.AddHour(hour6);
        }
    }
}
