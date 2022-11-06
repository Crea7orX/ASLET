using ReactiveUI;

namespace ASLET.ViewModels;

public class SubjectsViewModel : ReactiveObject, IRoutableViewModel
{
    private static SubjectsViewModel? _instance;

    public static SubjectsViewModel? GetInstance(IScreen hostScreen)
    {
        if (_instance == null)
        {
            _instance = new SubjectsViewModel(hostScreen);
        }

        return _instance;
    }

    public string? UrlPathSegment => "Subjects";
    public IScreen HostScreen { get; }

    public SubjectsViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;
    }
}