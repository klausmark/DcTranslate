using System.Collections.Generic;

namespace DcTranslate.Model
{
    public interface INumberTranslationRepository
    {
        IEnumerable<NumberTranslation> GetNumberTranslations(string like = "", long page = 0);
        long LastQueryWouldHaveReturnedThisAmountOfPages { get; }
        long PageSize { get; set; }
        NumberTranslation Add(NumberTranslation numberTranslation);
        void Remove(NumberTranslation numberTranslation);
        void Update(NumberTranslation numberTranslation);
    }
}