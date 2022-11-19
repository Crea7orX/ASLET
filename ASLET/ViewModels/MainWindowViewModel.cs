using System;
using System.Reactive;
using ReactiveUI;

namespace ASLET.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IScreen
    {
        private string _lastView = String.Empty;
        
        public RoutingState Router { get; } = new RoutingState();
        private ReactiveCommand<Unit, IRoutableViewModel> _goToClasses { get; }
        private ReactiveCommand<Unit, IRoutableViewModel> _goToTeachers { get; }
        private ReactiveCommand<Unit, IRoutableViewModel> _goToSubjects { get; }
        private ReactiveCommand<Unit, IRoutableViewModel> _goToHours { get; }

        public MainWindowViewModel()
        {
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

            GoToClasses();
        }
        
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
    }
}
