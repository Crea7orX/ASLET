using ReactiveUI;

namespace ASLET.ViewModels;

public class HoursViewModel : ReactiveObject, IRoutableViewModel
{
    private static HoursViewModel? _instance;

    public static HoursViewModel? GetInstance(IScreen hostScreen)
    {
        if (_instance == null)
        {
            _instance = new HoursViewModel(hostScreen);
        }

        return _instance;
    }

    public string? UrlPathSegment => "Hours";
    public IScreen HostScreen { get; }

    public HoursViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;
    }
}