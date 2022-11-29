using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ASLET.Models;
using ASLET.Services;
using ASLET.Services.GeneticAlgorithm;
using ASLET.Views;
using Avalonia;
using ReactiveUI;

namespace ASLET.ViewModels;

public class TimetablesViewModel : ViewModelBase, IRoutableViewModel
{
    #region Routing

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

    #endregion

    #region Logic

    public ICommand GenerateTimetableCommand { get; }

    public ObservableCollection<TimetableSelectorModel> Timetables { get; } = new();

    private TimetableSelectorModel _selectedTimetable;

    public TimetableSelectorModel SelectedTimetable
    {
        get => _selectedTimetable;
        private set
        {
            this.RaiseAndSetIfChanged(ref _selectedTimetable, value);
            UpdateTimetable(_selectedTimetable);
        }
    }

    private ObservableCollection<TimetableModel> _timetable = new();

    public ObservableCollection<TimetableModel> Timetable
    {
        get => _timetable;
        private set => this.RaiseAndSetIfChanged(ref _timetable, value);
    }

    private readonly Dictionary<int, List<TimetableSlotModel>> _roomsTimetable = new();
    
    private void GenerateTimetable()
    {
        if (ConfigurationService.Instance.GetHours().Count < 3)
        {
            NotificationService.ShowWarning(MainWindow.Instance, "Моля въведете поне 3 часа, за да се генерира учебна програма!");
            return;
        }
        if (ConfigurationService.Instance.GetRooms().Count < 3)
        {
            NotificationService.ShowWarning(MainWindow.Instance, "Моля въведете поне 3 стаи, за да се генерира учебна програма!");
            return;
        }
        
        Timetables.Clear();
        Timetable.Clear();
        _roomsTimetable.Clear();
        SelectedTimetable = TimetableSelectorModel.Empty;
        
        Hgasso<Schedule> algorithm = new Hgasso<Schedule>(new Schedule(ConfigurationService.Instance));
        int generatedStatus = algorithm.Run(10000);
        List<TimetableSlotModel>? result = TimetableVisualizationService.GetResult(algorithm.Result);

        if (generatedStatus == -1) NotificationService.ShowError(MainWindow.Instance, "Невъзможно генериране на учебна програма!");
        else if (generatedStatus == 0) NotificationService.ShowWarning(MainWindow.Instance, "Учебната програма ИМА застъпвания на учители или не изпълнение на условия за стаите!");
        else if (generatedStatus == 1) NotificationService.ShowSuccess(MainWindow.Instance, "Учебната програма Е ГЕНЕРИРАНА УСПЕШНО, без застъпвания на учители и с изпълнени всички условия за стаите!");
        if (result == null) return;
        
        ObservableCollection<StudentsGroupModel> classes = ConfigurationService.Instance.GetGroups();
        ObservableCollection<ProfessorModel> teachers = ConfigurationService.Instance.GetTeachers();
        ObservableCollection<RoomModel> rooms = ConfigurationService.Instance.GetRooms();
        
        foreach (StudentsGroupModel @class in classes)
        {
            Timetables.Add(new TimetableSelectorModel(@class, @class.Name + " - " + @class.NumberOfStudents + " (" + (@class.Id + 1) + ")"));
        }

        foreach (ProfessorModel teacher in teachers)
        {
            Timetables.Add(new TimetableSelectorModel(teacher, teacher.Name + " (" + (teacher.Id + 1) + ")"));
        }
        
        foreach (RoomModel room in rooms)
        {
            _roomsTimetable.Add(room.Id, result.Where(slot => slot.Room.Id == room.Id).ToList());
            Timetables.Add(new TimetableSelectorModel(room, room.Name + " - " + room.NumberOfSeats + " (" + (room.Id + 1) + ")"));
        }

        SelectedTimetable = Timetables[0];
    }

    private void UpdateTimetable(TimetableSelectorModel selector)
    {
        Timetable.Clear();
        Dictionary<int, Dictionary<int, string>> hoursDayLink = new Dictionary<int, Dictionary<int, string>>();
        for (int i = 1; i <= Constants.DAY_HOURS; i++)
        {
            hoursDayLink[i] = new Dictionary<int, string>();
            for (int j = 1; j <= 5; j++)
            {
                hoursDayLink[i].Add(j, "");
            }
        }
        if (selector.Model is StudentsGroupModel)
        {
            StudentsGroupModel selectedClass = (StudentsGroupModel)selector.Model;
            foreach (List<TimetableSlotModel> slotModels in _roomsTimetable.Values)
            {
                foreach (TimetableSlotModel timetableSlotModel in slotModels.Where(slot => Equals(slot.Subject.Groups[0], selectedClass)))
                {
                    hoursDayLink[timetableSlotModel.Hour][timetableSlotModel.Day] = timetableSlotModel.ClassToString();
                }
            }
        } else if (selector.Model is ProfessorModel)
        {
            ProfessorModel selectedTeacher = (ProfessorModel)selector.Model;
            foreach (List<TimetableSlotModel> slotModels in _roomsTimetable.Values)
            {
                foreach (TimetableSlotModel timetableSlotModel in slotModels.Where(slot => Equals(slot.Teacher, selectedTeacher)))
                {
                    hoursDayLink[timetableSlotModel.Hour][timetableSlotModel.Day] = timetableSlotModel.TeacherToString();
                }
            }
        } else if (selector.Model is RoomModel)
        {
            RoomModel selectedRoom = (RoomModel)selector.Model;
            foreach (TimetableSlotModel timetableSlotModel in _roomsTimetable[selectedRoom.Id])
            {
                hoursDayLink[timetableSlotModel.Hour][timetableSlotModel.Day] = timetableSlotModel.RoomToString();
            }
        }

        foreach (KeyValuePair<int,Dictionary<int,string>> keyValuePair in hoursDayLink)
        {
            Timetable.Add(new TimetableModel(keyValuePair.Key.ToString(), keyValuePair.Value[1],
                keyValuePair.Value[2], keyValuePair.Value[3], keyValuePair.Value[4], keyValuePair.Value[5]));
        }
    }

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

    public TimetablesViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;
        
        GenerateTimetableCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            GenerateTimetable();
        });
        
        if (Timetables.Count > 0) SelectedTimetable = Timetables[0];
        
        SelectedTimetable = TimetableSelectorModel.Empty;
    }
}