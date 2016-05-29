using System.Windows;
using DcTranslate.Model;
using DcTranslate.View.Windows;
using DcTranslate.ViewModel.ViewModels;

namespace DcTranslate.View.Helpers
{
    public class ViewFunctions : IViewFunctions
    {
        public bool EditNumberTranslation(NumberTranslation numberTranslation)
        {
            var addEditWindow = new AddEditWindow();
            var addEditWindowViewModel = new AddEditWindowViewModel(numberTranslation);
            addEditWindow.DataContext = addEditWindowViewModel;

            if (addEditWindow.ShowDialog() == false) return false;

            numberTranslation.Id = addEditWindowViewModel.NumberTranslation.Id;
            numberTranslation.FromNumber = addEditWindowViewModel.NumberTranslation.FromNumber;
            numberTranslation.ToNumber = addEditWindowViewModel.NumberTranslation.ToNumber;
            numberTranslation.Description = addEditWindowViewModel.NumberTranslation.Description;
            return true;
        }

        public NumberTranslation AddNumberTranslation()
        {
            var addEditWindow = new AddEditWindow();
            var addEditWindowViewModel = new AddEditWindowViewModel();
            addEditWindow.DataContext = addEditWindowViewModel;

            if (addEditWindow.ShowDialog() == false) return null;

            var numberTranslation = new NumberTranslation();
            numberTranslation.Id = addEditWindowViewModel.NumberTranslation.Id;
            numberTranslation.FromNumber = addEditWindowViewModel.NumberTranslation.FromNumber;
            numberTranslation.ToNumber = addEditWindowViewModel.NumberTranslation.ToNumber;
            numberTranslation.Description = addEditWindowViewModel.NumberTranslation.Description;
            return numberTranslation;
        }

        public void DisplayMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}