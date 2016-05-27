using System;
using DcTranslate.ViewModel.ViewModels;

namespace DcTranslate.View.Helpers
{
    public class ViewModelLocator
    {
        private readonly Lazy<MainWindowViewModel> _mainWindowViewModel = new Lazy<MainWindowViewModel>();
        public MainWindowViewModel MainWindowViewModel => _mainWindowViewModel.Value;
    }
}
