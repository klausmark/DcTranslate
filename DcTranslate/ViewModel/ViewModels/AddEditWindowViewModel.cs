using DcTranslate.Model;
using DcTranslate.ViewModel.BaseClasses;

namespace DcTranslate.ViewModel.ViewModels
{
    public class AddEditWindowViewModel : NotifyBase
    {
        public NumberTranslation NumberTranslation { get; set; }

        public AddEditWindowViewModel(NumberTranslation numberTranslation)
        {
            NumberTranslation = new NumberTranslation();
            NumberTranslation.Id = numberTranslation.Id;
            NumberTranslation.FromNumber = numberTranslation.FromNumber;
            NumberTranslation.ToNumber = numberTranslation.ToNumber;
            NumberTranslation.Description = numberTranslation.Description;
        }

        public AddEditWindowViewModel() : this(new NumberTranslation())
        {
            NumberTranslation = new NumberTranslation();
        }
    }
}