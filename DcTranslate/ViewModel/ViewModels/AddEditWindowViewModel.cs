using DcTranslate.Model;
using DcTranslate.ViewModel.BaseClasses;

namespace DcTranslate.ViewModel.ViewModels
{
    public class AddEditWindowViewModel : NotifyBase
    {
        public NumberTranslation NumberTranslation { get; set; }
        public bool IsEdit { get; set; }

        public AddEditWindowViewModel(NumberTranslation numberTranslation)
        {
            NumberTranslation = new NumberTranslation();
            NumberTranslation.Id = numberTranslation.Id;
            NumberTranslation.FromNumber = numberTranslation.FromNumber;
            NumberTranslation.ToNumber = numberTranslation.ToNumber;
            NumberTranslation.Description = numberTranslation.Description;
            IsEdit = true;
        }

        public AddEditWindowViewModel() : this(new NumberTranslation())
        {
            NumberTranslation = new NumberTranslation();
            IsEdit = false;
        }
    }
}