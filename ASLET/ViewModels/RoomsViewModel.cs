using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ASLET.Models;
using ReactiveUI;

namespace ASLET.ViewModels;

public class RoomsViewModel : ViewModelBase, IRoutableViewModel
{
    #region Routing

    private static RoomsViewModel? _instance;

    public static RoomsViewModel GetInstance(IScreen? hostScreen)
    {
        if (_instance == null)
        {
            _instance = new RoomsViewModel(hostScreen!);
        }

        return _instance;
    }

    public string? UrlPathSegment => "Rooms";
    public IScreen HostScreen { get; }

    #endregion

    #region Logic

    public ICommand AddRoomCommand { get; }

    public Interaction<RoomsDialogViewModel, RoomModel?> AddRoom { get; }

    private RoomModel _selectedRoom;

    public RoomModel SelectedRoom
    {
        get => _selectedRoom;
        private set => this.RaiseAndSetIfChanged(ref _selectedRoom, value);
    }

    private ObservableCollection<RoomModel> _rooms = new();

    public ObservableCollection<RoomModel> Rooms
    {
        get => _rooms;
        private set => this.RaiseAndSetIfChanged(ref _rooms, value);
    }

    public ICommand DeleteRoomCommand { get; }


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
    
    public RoomsViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;
        
        AddRoom = new Interaction<RoomsDialogViewModel, RoomModel?>();
        AddRoomCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            RoomModel? result = await AddRoom.Handle(new RoomsDialogViewModel(_darkMode));

            if (result != null)
            {
                ConfigurationService.Instance.AddRoom(result);
            }
        });

        DeleteRoomCommand = ReactiveCommand.CreateFromTask((RoomModel selectedRoom) =>
        {
            ConfigurationService.Instance.RemoveRoom(selectedRoom);
            return Task.CompletedTask;
        });
    }

    public void UpdateRooms(ref ObservableCollection<RoomModel> rooms)
    {
        Rooms = rooms;
    }
}