using System;
using System.Reactive;
using System.Windows.Input;
using ReactiveUI;

namespace ASLET.ViewModels.Shared
{
    public class AddButtonViewModel: ViewModelBase
    {
        private string _textContent = "Hello";
        // public ReactiveCommand<Unit, Unit> OpenInputDialog { get; }

        public string TextContent
        {
            set => _textContent = value;
            get => _textContent;
        }

        public void OpenNewDialogWindow()
        {
            
        }

        // public AddButtonViewModel()
        // {
        //     OpenInputDialog =  ReactiveCommand.Create(() =>
        //     {
        //         OpenNewDialogWindow();
        //     }))
        // }
    }
}

