using ICMS.Model.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace ICMS.Validation
{
    public class Role_RoleAction_Validation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            
           
            RoleAction roleAction = (value as BindingGroup).Items[0] as RoleAction;

            if (roleAction != null)
            {
                if (roleAction.View == false)
                {
                    if (roleAction.Add == true | roleAction.Edit == true | roleAction.Delete == true | roleAction.Print == true)
                    {
                        return new ValidationResult(false, $"Field is required");
                    }
                }
            }


            return ValidationResult.ValidResult;
        }
    }
}
