using System;
using System.Reactive.Linq;
using System.Windows.Input;
using ASLET.Models;
using ReactiveUI;

namespace ASLET.ViewModels;

public class HoursViewModel : ViewModelBase, IRoutableViewModel
{
    private static HoursViewModel? _instance;

    public static HoursViewModel GetInstance(IScreen? hostScreen)
    {
        if (_instance == null)
        {
            _instance = new HoursViewModel(hostScreen!);
        }

        return _instance;
    }

    public string? UrlPathSegment => "Hours";
    public IScreen HostScreen { get; }

    public ICommand AddHourCommand { get; }

    public Interaction<HoursDialogViewModel, HourModel?> AddHour { get; }

    public HoursViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;
        
        AddHour = new Interaction<HoursDialogViewModel, HourModel?>();
        AddHourCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            HourModel? result = await AddHour.Handle(new HoursDialogViewModel());

            // TODO ADD HOUR
            Console.WriteLine(result);
        });
    }
}