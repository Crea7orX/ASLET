using System.Reactive;
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

            GoToClasses.Execute();
        }
    }
}
