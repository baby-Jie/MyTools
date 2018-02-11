using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ToolCollection.Validations
{
    public class DigitalIntValidationRule:ValidationRule
    {

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                int val = 0;
                if (int.TryParse(val.ToString(), out val))
                {
                    return new ValidationResult(true, null);
                }
                else
                {
                    return new ValidationResult(false, "input text is not digit");
                }
            }
            catch (Exception)
            {

                return new ValidationResult(false, "input text is not digit");
            }
        }
    }
}
