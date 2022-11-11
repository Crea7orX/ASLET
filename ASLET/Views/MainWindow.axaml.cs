using System.Threading.Tasks;
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
            this.WhenActivated(d => d(ClassesViewModel.GetInstance(null).AddClass.RegisterHandler(DoShowDialogAsync)));
        }
        
        private async Task DoShowDialogAsync(InteractionContext<ClassesDialogWindowViewModel, ClassesDialogWindowViewModel?> interaction)
        {
            ClassesDialogWindow dialog = new ClassesDialogWindow();
            dialog.DataContext = interaction.Input;

            ClassesDialogWindowViewModel? result = await dialog.ShowDialog<ClassesDialogWindowViewModel?>(this);
            interaction.SetOutput(result);
        }
    }
}