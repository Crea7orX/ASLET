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

    public Interaction<ClassesPopupWindowViewModel, ClassesPopupWindowViewModel?> AddClass { get; }

    public ClassesViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;
        
        AddClass = new Interaction<ClassesPopupWindowViewModel, ClassesPopupWindowViewModel?>();
        AddClassCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            ClassesPopupWindowViewModel? result = await AddClass.Handle(new ClassesPopupWindowViewModel());

            Console.WriteLine(result);
        });
    }
}