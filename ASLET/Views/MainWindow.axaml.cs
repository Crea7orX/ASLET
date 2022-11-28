using System.Threading.Tasks;
using ASLET.Models;
using ASLET.ViewModels;
using Avalonia.ReactiveUI;
using ReactiveUI;


namespace ASLET.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WhenActivated(d => d(ClassesViewModel.GetInstance(null).AddClass.RegisterHandler(AddClassDialogAsync)));
            this.WhenActivated(d => d(TeachersViewModel.GetInstance(null).AddTeacher.RegisterHandler(AddTeacherDialogAsync)));
            this.WhenActivated(d => d(SubjectsViewModel.GetInstance(null).AddSubject.RegisterHandler(AddSubjectDialogAsync)));
            this.WhenActivated(d => d(HoursViewModel.GetInstance(null).AddHour.RegisterHandler(AddHourDialogAsync)));
            this.WhenActivated(d => d(RoomsViewModel.GetInstance(null).AddRoom.RegisterHandler(AddRoomDialogAsync)));
        }
        
        private async Task AddClassDialogAsync(InteractionContext<ClassesDialogViewModel, StudentsGroupModel?> interaction)
        {
            ClassesDialogWindow dialog = new ClassesDialogWindow
            {
                DataContext = interaction.Input
            };

            StudentsGroupModel? result = await dialog.ShowDialog<StudentsGroupModel?>(this);
            interaction.SetOutput(result);
        }
        
        private async Task AddTeacherDialogAsync(InteractionContext<TeachersDialogViewModel, ProfessorModel?> interaction)
        {
            TeachersDialogWindow dialog = new TeachersDialogWindow()
            {
                DataContext = interaction.Input
            };

            ProfessorModel? result = await dialog.ShowDialog<ProfessorModel?>(this);
            interaction.SetOutput(result);
        }
        
        private async Task AddSubjectDialogAsync(InteractionContext<SubjectsDialogViewModel, SubjectModel?> interaction)
        {
            SubjectsDialogWindow dialog = new SubjectsDialogWindow()
            {
                DataContext = interaction.Input
            };

            SubjectModel? result = await dialog.ShowDialog<SubjectModel?>(this);
            interaction.SetOutput(result);
        }
        
        private async Task AddHourDialogAsync(InteractionContext<HoursDialogViewModel, SubjectClassModel?> interaction)
        {
            HoursDialogWindow dialog = new HoursDialogWindow()
            {
                DataContext = interaction.Input
            };

            SubjectClassModel? result = await dialog.ShowDialog<SubjectClassModel?>(this);
            interaction.SetOutput(result);
        }
        
        private async Task AddRoomDialogAsync(InteractionContext<RoomsDialogViewModel, RoomModel?> interaction)
        {
            RoomsDialogWindow dialog = new RoomsDialogWindow()
            {
                DataContext = interaction.Input
            };

            RoomModel? result = await dialog.ShowDialog<RoomModel?>(this);
            interaction.SetOutput(result);
        }
    }
}