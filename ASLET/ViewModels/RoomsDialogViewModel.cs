using System.Reactive;
using System.Threading.Tasks;
using ASLET.Models;
using ReactiveUI;

namespace ASLET.ViewModels;

public class RoomsDialogViewModel : ViewModelBase
{
    public ReactiveCommand<Unit, RoomModel> AddRoomCommand { get; }
    public ReactiveCommand<Unit, RoomModel?> CancelCommand { get; }

    private string _roomName;

    public string RoomName
    {
        get => _roomName;
        private set => this.RaiseAndSetIfChanged(ref _roomName, value);
    }
    
    private bool _isLaboratory;

    public bool IsLaboratory
    {
        get => _isLaboratory;
        private set => this.RaiseAndSetIfChanged(ref _isLaboratory, value);
    }

    private int _roomSize;

    public int RoomSize
    {
        get => _roomSize;
        private set => this.RaiseAndSetIfChanged(ref _roomSize, value);
    }

    #region DarkMode

    private bool _darkMode;

    public bool DarkMode
    {
        get => _darkMode;
        set => this.RaiseAndSetIfChanged(ref _darkMode, value);
    }

    #endregion

    public RoomsDialogViewModel(bool darkMode)
    {
        // TODO CHECKERS FOR VALID INPUT
        AddRoomCommand = ReactiveCommand.CreateFromTask(() =>
            Task.FromResult(new RoomModel(_roomName, _isLaboratory, _roomSize)));

        CancelCommand = ReactiveCommand.CreateFromTask(() => Task.FromResult<RoomModel?>(null));

        DarkMode = darkMode;

        RoomSize = 26;
    }
}