using System;
using System.Globalization;
using System.Windows.Controls;

namespace Posit
{
    public class FutureDateValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            DateTime time;
            if (!DateTime.TryParse((value ?? "").ToString(),
                CultureInfo.CurrentCulture,
                DateTimeStyles.AssumeLocal | DateTimeStyles.AllowWhiteSpaces,
                out time)) return new ValidationResult(false, "");

            return time.Date < DateTime.Now.Date
                ? new ValidationResult(false, "")
                : ValidationResult.ValidResult;
        }
    }
}