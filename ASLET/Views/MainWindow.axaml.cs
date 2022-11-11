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
        
        private async Task DoShowDialogAsync(InteractionContext<ClassesPopupWindowViewModel, ClassesPopupWindowViewModel?> interaction)
        {
            ClassesPopupWindow dialog = new ClassesPopupWindow();
            dialog.DataContext = interaction.Input;

            ClassesPopupWindowViewModel? result = await dialog.ShowDialog<ClassesPopupWindowViewModel?>(this);
            interaction.SetOutput(result);
        }
    }
}