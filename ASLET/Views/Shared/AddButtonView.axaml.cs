using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace ASLET.Views.Shared
{
    public partial class AddButtonView : UserControl
    {
        public AddButtonView()
        {
            InitializeComponent();
        }
        
        public string Test { get; set; } = string.Empty;
        

        public void OpenNewDialogWindow(object? sender, RoutedEventArgs routedEventArgs)
        {
            
        }
    }
}