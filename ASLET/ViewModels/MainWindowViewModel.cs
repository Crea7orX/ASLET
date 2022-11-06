using System.Reactive;
using ReactiveUI;

namespace ASLET.ViewModels
{
    public class MainWindowViewModel : ReactiveObject, IScreen
    {
        public RoutingState Router { get; } = new RoutingState();
        public ReactiveCommand<Unit, IRoutableViewModel> GoToClasses { get; }
        public ReactiveCommand<Unit, IRoutableViewModel> GoToTeachers { get; }

        public MainWindowViewModel()
        {
            GoToClasses = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(ClassesViewModel.GetInstance(this))
            );
            GoToTeachers = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(TeachersViewModel.GetInstance(this))
            );
        }
    }
}
