using System;

namespace ASLET.ViewModels.Shared
{
    public class AddButtonViewModel: ViewModelBase
    {
        private string _textContent = "Hello";
        
        public string TextContent
        {
            set => _textContent = value;
            get => _textContent;
        }
    }
}

