using System;
using System.Reactive.Linq;
using System.Windows.Input;
using ReactiveUI;

namespace ASLET.ViewModels;

public class ClassesViewModel : ViewModelBase, IRoutableViewModel
{
    private static ClassesViewModel? _instance;

    public static ClassesViewModel GetInstance(IScreen? hostScreen)
    {
        if (_instance == null)
        {
            _instance = new ClassesViewModel(hostScreen!);
        }

        return _instance;
    }

    public string? UrlPathSegment => "Classes";
    public IScreen HostScreen { get; }

    public ICommand AddClassCommand { get; }

    public Interaction<ClassesDialogWindowViewModel, ClassesDialogWindowViewModel?> AddClass { get; }

    public ClassesViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;
        
        AddClass = new Interaction<ClassesDialogWindowViewModel, ClassesDialogWindowViewModel?>();
        AddClassCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            ClassesDialogWindowViewModel? result = await AddClass.Handle(new ClassesDialogWindowViewModel());

            Console.WriteLine(result);
        });
    }
}