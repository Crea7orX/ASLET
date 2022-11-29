using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ASLET.Models;
using ASLET.Services;
using ASLET.Views;
using ReactiveUI;

namespace ASLET.ViewModels;

public class HoursViewModel : ViewModelBase, IRoutableViewModel
{
    #region Routing

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

    #endregion

    #region Logic

    public ICommand AddHourCommand { get; }

    public Interaction<HoursDialogViewModel, SubjectClassModel?> AddHour { get; }

    private SubjectClassModel _selectedHour;

    public SubjectClassModel SelectedHour
    {
        get => _selectedHour;
        private set => this.RaiseAndSetIfChanged(ref _selectedHour, value);
    }

    private ObservableCollection<SubjectClassModel> _hours = new();

    public ObservableCollection<SubjectClassModel> Hours
    {
        get => _hours;
        private set => this.RaiseAndSetIfChanged(ref _hours, value);
    }

    public ICommand DeleteHourCommand { get; }

    #endregion
    
    #region Parent-child relations

    private static MainWindowViewModel? _parent;
    public static void SetParent(MainWindowViewModel? parent) => _parent = parent;

    #endregion
    
    #region DarkMode

    private bool _darkMode;
    public bool DarkMode
    {
        get => _darkMode;
        set => this.RaiseAndSetIfChanged(ref _darkMode, value);
    }
    #endregion
    
    public HoursViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;
        
        AddHour = new Interaction<HoursDialogViewModel, SubjectClassModel?>();
        AddHourCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            if (ConfigurationService.Instance.GetGroups().Count == 0)
            {
                NotificationService.ShowError(MainWindow.Instance, "Въведете поне един клас!");
                return;
            }
            if (ConfigurationService.Instance.GetTeachers().Count == 0)
            {
                NotificationService.ShowError(MainWindow.Instance, "Въведете поне един учител!");
                return;
            }
            if (ConfigurationService.Instance.GetSubjects().Count == 0)
            {
                NotificationService.ShowError(MainWindow.Instance, "Въведете поне един предмет!");
                return;
            }

            SubjectClassModel? result = await AddHour.Handle(new HoursDialogViewModel(DarkMode));

            if (result != null)
            {
                ConfigurationService.Instance.AddHour(result);
            }
        });

        DeleteHourCommand = ReactiveCommand.CreateFromTask((SubjectClassModel selectedHour) =>
        {
            ConfigurationService.Instance.RemoveHour(selectedHour);
            return Task.CompletedTask;
        });
    }

    public void UpdateHours(ref ObservableCollection<SubjectClassModel> hours)
    {
        Hours = hours;
    }
}