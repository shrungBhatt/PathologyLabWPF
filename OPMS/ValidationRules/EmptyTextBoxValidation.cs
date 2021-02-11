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
    public class EmptyTextBoxValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string && !string.IsNullOrEmpty(value as string))
            {
                return ValidationResult.ValidResult;
            }
            else
            {
                return new ValidationResult(false, "Please enter some text here");
            }
        }
    }
}
