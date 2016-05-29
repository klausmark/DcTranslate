using DcTranslate.Model;

namespace DcTranslate.View.Helpers
{
    public interface IViewFunctions
    {
        bool EditNumberTranslation(NumberTranslation numberTranslation);
        NumberTranslation AddNumberTranslation();
        void DisplayMessage(string message);
    }
}