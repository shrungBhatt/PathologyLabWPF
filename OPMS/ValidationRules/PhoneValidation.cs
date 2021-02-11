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
    public class PhoneValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is int)
            {
                var regex = new Regex(@"^[1-9][0-9]+$");
                var result = regex.Match(((int)value).ToString());
                if (result.Success)
                {
                    return ValidationResult.ValidResult;
                }
                else
                {
                    return new ValidationResult(false, "Invalid data format. The first digit from left should not start from 0. The field value should not be 0");
                }

            }
            else
            {
                return new ValidationResult(false, "Please enter some value here");
            }
        }
    }
}
