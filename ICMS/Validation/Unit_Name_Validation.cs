using ICMS.Model.DataAccess;
using ICMS.Model.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ICMS.Validation
{
    public class Wrapper : DependencyObject
    {
        public static readonly DependencyProperty SelectedItemIdProperty =
             DependencyProperty.Register("SelectedItemId", typeof(int),
             typeof(Wrapper), new FrameworkPropertyMetadata(0));

        public int SelectedItemId
        {
            get { return (int)GetValue(SelectedItemIdProperty); }
            set { SetValue(SelectedItemIdProperty, value); }
        }

    }



    public class Unit_Name_Validation : ValidationRule
    {

        public Wrapper Wrapper { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string inputString = (string)value;

            //int test = this.Wrapper.SelectedItemId;



            if (string.IsNullOrWhiteSpace(inputString))
            {
                return new ValidationResult(false, "Field is required.");
            }

            //if (isUniqueName(inputString, this.Wrapper.SelectedItemId))
            //{
            //    return new ValidationResult(false, "Name must be unique.");
            //}

            return ValidationResult.ValidResult; 
        }

        private bool isUniqueName(string inputString, int SelectedItemId)
        {
            List<Unit> UnitList = GlobalConfig.Connection.Unit_GetAll(GlobalConfig.CnnString("ICMSdatabase"));

            UnitList.RemoveAll(p => p.UnitId == SelectedItemId);


            bool isUniue = UnitList.Any(p => p.Name == inputString);

            return isUniue;
        }
    }


}