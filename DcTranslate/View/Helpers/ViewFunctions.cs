using System;
using DcTranslate.Model;

namespace DcTranslate.View.Helpers
{
    public class ViewFunctions
    {
        public void EditNumberTranslation(NumberTranslation numberTranslation, Action<NumberTranslation> callback)
        {
            callback(new NumberTranslation(fromNumber:numberTranslation.FromNumber,toNumber:"99999999",description:"Description"));
        }
    }
}