using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Convert_DB_QLCM_ICMS.ConvertTable
{
    public static class Convert_Old_data
    {
        public static string Format_Organization_Name(string oldName)
        {
            oldName = oldName.Trim();

            if (String.IsNullOrEmpty(oldName))
                throw new ArgumentException("ARGH!");

            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            string newName = textInfo.ToTitleCase(oldName);


            newName = Regex.Replace(newName, @"[&]", "và");
            newName = newName.Replace("KHvàCN", "KH&CN");
            newName = newName.Replace("Và", "và");

            return newName;
        }

        public static string Format_Organization_Adress(string oldAddress)
        {
            oldAddress = oldAddress.Trim();
            string newAddress = "";

            if (String.IsNullOrEmpty(oldAddress))
                return newAddress;

            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            newAddress = textInfo.ToTitleCase(oldAddress);

            newAddress = Regex.Replace(newAddress, @"[&]", "và");

            newAddress = newAddress.Replace("Tp.", "TP.");
            newAddress = newAddress.Replace("Thành Phố", "TP.");
            newAddress = newAddress.Replace("T.", "Tỉnh");


            return newAddress;
        }

        public static string Format_Staff_Name(string oldName)
        {
            oldName = oldName.Trim();
            string newName = "";

            if (String.IsNullOrEmpty(oldName))
                return newName;

            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            newName = oldName.ToLower();
            newName = textInfo.ToTitleCase(newName);

            newName = Regex.Replace(newName, @"[&]", "và");

            return newName;
        }

        public static string Format_MachineTypeCode(string oldName)
        {
            oldName = oldName.Trim();
            string newName = "";

            if (String.IsNullOrEmpty(oldName))
                return newName;

            if (oldName.ToLower() == "n/a")
            {
                return newName;
            }

            return oldName;
        }
    }
}
