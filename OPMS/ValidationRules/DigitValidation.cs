using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OPMS.ValidationRules
{
    public class DigitValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int digit;
            int.TryParse(value?.ToString(), out digit);
            var regex = new Regex(@"^[1-9][0-9]+$");
            var result = regex.Match((digit).ToString());
            if (result.Success)
            {
                return ValidationResult.ValidResult;
            }
            else
            {
                return new ValidationResult(false, "Invalid data format. The first digit from left should not start from 0. The field value should not be 0");
            }
        }
    }
}
