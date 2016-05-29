using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace DcTranslate.View.ValidationRules
{
    public class E164ValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string s = (string) value;
            var match = Regex.Match(s, @"^\+\d+$");
            if (match.Success) return ValidationResult.ValidResult;
            return new ValidationResult(false, "Not valid E.164 number");
        }
    }
}
