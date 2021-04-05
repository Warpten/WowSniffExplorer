using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.VisualBasic.CompilerServices;

namespace SniffExplorer.UI.Binding
{
    public class LevelRangeRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!(value is string stringValue))
                return new ValidationResult(false, "Invalid characters");

            if (!int.TryParse(stringValue, out var integerValue))
                return new ValidationResult(false, "Invalid characters");

            return ValidationResult.ValidResult;
        }
    }
}
