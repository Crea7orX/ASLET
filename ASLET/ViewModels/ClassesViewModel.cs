using ReactiveUI;

namespace ASLET.ViewModels;

public class ClassesViewModel : ReactiveObject, IRoutableViewModel
{
    private static ClassesViewModel? _instance;

    public static ClassesViewModel? GetInstance(IScreen hostScreen)
    {
        if (_instance == null)
        {
            _instance = new ClassesViewModel(hostScreen);
        }

        return _instance;
    }

    public string? UrlPathSegment => "Classes";
    public IScreen HostScreen { get; }

    public ClassesViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;
    }
}