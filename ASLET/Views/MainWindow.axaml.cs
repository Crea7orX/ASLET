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
        }
        
        private async Task AddClassDialogAsync(InteractionContext<ClassesDialogViewModel, ClassModel?> interaction)
        {
            ClassesDialogWindow dialog = new ClassesDialogWindow
            {
                DataContext = interaction.Input
            };

            ClassModel? result = await dialog.ShowDialog<ClassModel?>(this);
            interaction.SetOutput(result);
        }
        
        private async Task AddTeacherDialogAsync(InteractionContext<TeachersDialogViewModel, TeacherModel?> interaction)
        {
            TeachersDialogWindow dialog = new TeachersDialogWindow()
            {
                DataContext = interaction.Input
            };

            TeacherModel? result = await dialog.ShowDialog<TeacherModel?>(this);
            interaction.SetOutput(result);
        }
    }
}