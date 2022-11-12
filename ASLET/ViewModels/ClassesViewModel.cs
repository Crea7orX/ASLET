using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ASLET.Models;
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

    public Interaction<ClassesDialogViewModel, ClassModel?> AddClass { get; }

    private ClassModel _selectedClass;

    public ClassModel SelectedClass
    {
        get => _selectedClass;
        private set => this.RaiseAndSetIfChanged(ref _selectedClass, value);
    }

    private ObservableCollection<ClassModel> _classes = new();

    public ObservableCollection<ClassModel> Classes
    {
        get => _classes;
        private set => this.RaiseAndSetIfChanged(ref _classes, value);
    }

    public ICommand DeleteClassCommand { get; }

    public ClassesViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;

        AddClass = new Interaction<ClassesDialogViewModel, ClassModel?>();
        AddClassCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            ClassModel? result = await AddClass.Handle(new ClassesDialogViewModel());

            // TODO ADD CLASS
            Console.WriteLine(result);
            if (result != null) Classes.Add(result);
        });

        DeleteClassCommand = ReactiveCommand.CreateFromTask((ClassModel selectedClass) =>
        {
            Classes.Remove(selectedClass);
            return Task.CompletedTask;
        });
    }
}