using ReactiveUI;

namespace ASLET.ViewModels;

public class TeachersViewModel : ReactiveObject, IRoutableViewModel
{
    private static TeachersViewModel? _instance;

    public static TeachersViewModel? GetInstance(IScreen hostScreen)
    {
        if (_instance == null)
        {
            _instance = new TeachersViewModel(hostScreen);
        }

        return _instance;
    }

    public string? UrlPathSegment => "Teachers";
    public IScreen HostScreen { get; }

    public TeachersViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;
    }
}