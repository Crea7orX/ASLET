using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ASLET.Models;
using ASLET.Services;
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

    private HourModel _selectedHour;

    public HourModel SelectedHour
    {
        get => _selectedHour;
        private set => this.RaiseAndSetIfChanged(ref _selectedHour, value);
    }

    private ObservableCollection<HourModel> _hours = new();

    public ObservableCollection<HourModel> Hours
    {
        get => _hours;
        private set => this.RaiseAndSetIfChanged(ref _hours, value);
    }

    public ICommand DeleteHourCommand { get; }

    public HoursViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;
        
        AddHour = new Interaction<HoursDialogViewModel, HourModel?>();
        AddHourCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            HourModel? result = await AddHour.Handle(new HoursDialogViewModel());

            if (result != null)
            {
                TimetableService.AddHour(result);
            }
        });

        DeleteHourCommand = ReactiveCommand.CreateFromTask((HourModel selectedHour) =>
        {
            TimetableService.RemoveHour(selectedHour);
            return Task.CompletedTask;
        });
    }

    public void UpdateHours(ref ObservableCollection<HourModel> hours)
    {
        Hours = hours;
    }
}