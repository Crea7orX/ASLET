using System.Windows.Input;
using ASLET.Services;
using ReactiveUI;

namespace ASLET.ViewModels;

public class TimetablesViewModel : ViewModelBase, IRoutableViewModel
{
    private static TimetablesViewModel? _instance;

    public static TimetablesViewModel GetInstance(IScreen? hostScreen)
    {
        if (_instance == null)
        {
            _instance = new TimetablesViewModel(hostScreen!);
        }

        return _instance;
    }

    public string? UrlPathSegment => "Timetables";
    
    public IScreen HostScreen { get; }

    public ICommand GenerateTimetableCommand { get; }

    public TimetablesViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;
        
        GenerateTimetableCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            TimetableService.GenerateTimetable();
        });
    }
}