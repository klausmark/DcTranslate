using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace DcTranslate.View.ValidationRules
{
    public class FromNumberValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string s = (string)value;
            var match = Regex.Match(s, @"^18\d{4}$");
            if (match.Success) return ValidationResult.ValidResult;
            return new ValidationResult(false, "Not valid from number number");
        }
    }
}