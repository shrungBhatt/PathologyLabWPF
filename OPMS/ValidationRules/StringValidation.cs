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
    public class StringValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string && !string.IsNullOrEmpty(value as string))
            {
                var regex = new Regex(@"^[a-zA-Z]+$");
                var result = regex.Match(value as string);
                if (result.Success)
                {
                    return ValidationResult.ValidResult;
                }
                else
                {
                    return new ValidationResult(false, "Please enter valid data here. This field only contains letters a-z or A-Z");
                }

            }
            else
            {
                return new ValidationResult(false, "Please enter some data here");
            }
        }

    }
}
