using System;
using System.Reactive;
using ASLET.Models;
using ASLET.Services;
using System;
using ReactiveUI;


namespace ASLET.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IScreen
    {

        #region Routing 

        private string _lastView = String.Empty;
        public RoutingState Router { get; } = new RoutingState();
        private ReactiveCommand<Unit, IRoutableViewModel> _goToClasses { get; }
        private ReactiveCommand<Unit, IRoutableViewModel> _goToTeachers { get; }
        private ReactiveCommand<Unit, IRoutableViewModel> _goToSubjects { get; }
        private ReactiveCommand<Unit, IRoutableViewModel> _goToHours { get; }

        private ReactiveCommand<Unit, IRoutableViewModel> _goToTimetables { get; }

        public void GoToClasses()
        {
            if (_lastView == "ClassesViewModel") return;
            
            _lastView = "ClassesViewModel";
            _goToClasses.Execute();
        }
        
        public void GoToTeachers()
        {
            if (_lastView == "TeachersViewModel") return;
            
            _lastView = "TeachersViewModel";
            _goToTeachers.Execute();
        }
        
        public void GoToSubjects()
        {
            if (_lastView == "SubjectsViewModel") return;
            
            _lastView = "SubjectsViewModel";
            _goToSubjects.Execute();
        }
        
        public void GoToHours()
        {
            if (_lastView == "HoursDialogViewModel") return;
            
            _lastView = "HoursDialogViewModel";
            _goToHours.Execute();
        }
        
        public void GoToTimetables()
        {
            if (_lastView == "TimetablesViewModel") return;
            
            _lastView = "TimetablesViewModel";
            _goToTimetables.Execute();
        }

        #endregion

        #region Dark Mode

        private bool _darkMode;
        
        public bool DarkMode
        {
            get => _darkMode;
            set => this.RaiseAndSetIfChanged(ref _darkMode, value);
        }

        public void ToggleDarkMode(bool isDarkMode)
        {
            DarkMode = isDarkMode;
            ClassesViewModelChild!.DarkMode = DarkMode;
            HoursViewModelChild!.DarkMode = DarkMode;
            SubjectsViewModelChild!.DarkMode = DarkMode;
            TeachersViewModelChild!.DarkMode = DarkMode;
            TimetablesViewModelChild!.DarkMode = DarkMode;
        }
        
        #endregion
        
        
        public MainWindowViewModel()
        {
            SetParents();
            SetChildren();
            
            _goToClasses = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(ClassesViewModel.GetInstance(this))
            );
            _goToTeachers = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(TeachersViewModel.GetInstance(this))
            );
            _goToSubjects = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(SubjectsViewModel.GetInstance(this))
            );
            _goToHours = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(HoursViewModel.GetInstance(this))
            );
            _goToTimetables = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(TimetablesViewModel.GetInstance(this))
            );

            GoToClasses();


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

        #region Parent-child relations

        private ClassesViewModel? _classesViewModelChild;
        private HoursViewModel? _hoursViewModelChild;
        private SubjectsViewModel? _subjectsViewModelChild;
        private TeachersViewModel? _teachersViewModelChild;
        private TimetablesViewModel? _timetablesViewModelChild;

        
        public void SetParents()
        {
            MenuViewModel.SetParent(this);
            ClassesViewModel.SetParent(this);
            HoursViewModel.SetParent(this);
            SubjectsViewModel.SetParent(this);
            TeachersViewModel.SetParent(this);
            TimetablesViewModel.SetParent(this);
        }

        private void SetChildren()
        {
            ClassesViewModelChild = ClassesViewModel.GetInstance(this);
            HoursViewModelChild = HoursViewModel.GetInstance(this);
            SubjectsViewModelChild = SubjectsViewModel.GetInstance(this);
            TeachersViewModelChild = TeachersViewModel.GetInstance(this);
            TimetablesViewModelChild = TimetablesViewModel.GetInstance(this);
        }

        public ClassesViewModel? ClassesViewModelChild
        {
            get => _classesViewModelChild;
            set => this.RaiseAndSetIfChanged(ref _classesViewModelChild, value);
        } 
        
        public HoursViewModel? HoursViewModelChild
        {
            get => _hoursViewModelChild;
            set => this.RaiseAndSetIfChanged(ref _hoursViewModelChild, value);
        }

        public SubjectsViewModel? SubjectsViewModelChild
        {
            get => _subjectsViewModelChild;
            set => this.RaiseAndSetIfChanged(ref _subjectsViewModelChild, value);
        }
        
        public TeachersViewModel? TeachersViewModelChild
        {
            get => _teachersViewModelChild;
            set => this.RaiseAndSetIfChanged(ref _teachersViewModelChild, value);
        }
        
        public TimetablesViewModel? TimetablesViewModelChild
        {
            get => _timetablesViewModelChild;
            set => this.RaiseAndSetIfChanged(ref _timetablesViewModelChild, value);
        }

        #endregion
        
    }
}
