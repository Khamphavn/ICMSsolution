using System.Configuration;
using System.IO;

namespace ICMS.AppSettings
{
    public static class AppSettings
    {
        public static double MINTEMPERATUE { get; private set; }
        public static double MAXTEMPERATUE { get; private set; }
        public static int DECIMALPOINTTEMPERATURE { get; private set; }


        public static double MAXPRESSURE { get; private set; }
        public static double MINPRESSURE { get; private set; }
        public static int DECIMALPOINTPRESSURE { get; private set; }

        public static double MAXHUMIDITY { get; private set; }
        public static double MINHUMIDITY { get; private set; }
        public static int DECIMALPOINTUMIDITY { get; private set; }

        public static int MAXSENSORS { get; private set; }
        public static int MAXCALIBDATA { get; private set; }

        public static int MONTHBEFORETODAY { get; private set; }


        public static string TEMPLATEFOLDER { get; private set; }


        public static void InitializeAppSettings()
        {
            GetMinTemperature();
            GetMaxTemperature();
            GetDecimalPointTemperature();

            GetMinPressure();
            GetMaxPressure();
            GetDecimalPointPressure();

            GetMinHumidity();
            GetMaxHumidity();
            GetDecimalPointHumidity();

            GetMaxSensors();
            GetMaxCalibData();

            GetCertificateBeforeToday();

            GetTemplateFolder();
        }


        #region private methods
        private static void GetMinTemperature()
        {
            string mintemp = ConfigurationManager.AppSettings["MINTEMPERATURE"];
            bool success = double.TryParse(ConfigurationManager.AppSettings["MINTEMPERATURE"], out double minTemperature);

            if (success)
            {
                MINTEMPERATUE = minTemperature;
            }
            else
            {
                MINTEMPERATUE = 0;
            }
        }
        private static void GetMaxTemperature()
        {
            bool success = double.TryParse(ConfigurationManager.AppSettings["MAXTEMPERATURE"], out double maxTemperature);

            if (success)
            {
                MAXTEMPERATUE = maxTemperature;
            }
            else
            {
                MAXTEMPERATUE = 50;
            }
        }
        private static void GetDecimalPointTemperature()
        {
            bool success = int.TryParse(ConfigurationManager.AppSettings["DECIMALPOINTTEMPERATURE"], out int decimalPointTemperature);

            if (success)
            {
                if (decimalPointTemperature >= 0 & decimalPointTemperature <= 10)
                    DECIMALPOINTTEMPERATURE = decimalPointTemperature;
            }
            else
            {
                DECIMALPOINTTEMPERATURE = 2;
            }
        }

        private static void GetMinPressure()
        {
            bool success = double.TryParse(ConfigurationManager.AppSettings["MINPRESSURE"], out double minPressure);

            if (success)
            {
                MINPRESSURE = minPressure;
            }
            else
            {
                MINPRESSURE = 900;
            }
        }
        private static void GetMaxPressure()
        {
            bool success = double.TryParse(ConfigurationManager.AppSettings["MAXPRESSURE"], out double maxPressure);

            if (success)
            {
                MAXPRESSURE = maxPressure;
            }
            else
            {
                MAXPRESSURE = 1084.8;
            }
        }
        private static void GetDecimalPointPressure()
        {
            bool success = int.TryParse(ConfigurationManager.AppSettings["DECIMALPOINTPRESSURE"], out int decimalPointPressure);

            if (success)
            {
                if (decimalPointPressure >= 0 & decimalPointPressure <= 10)
                    DECIMALPOINTPRESSURE = decimalPointPressure;
            }
            else
            {
                DECIMALPOINTPRESSURE = 2;
            }
        }

        private static void GetMinHumidity()
        {
            bool success = double.TryParse(ConfigurationManager.AppSettings["MINHUMIDITY"], out double minHumidity);

            if (success)
            {
                MINHUMIDITY = minHumidity;
            }
            else
            {
                MINHUMIDITY = 0;
            }
        }
        private static void GetMaxHumidity()
        {
            bool success = double.TryParse(ConfigurationManager.AppSettings["MAXHUMIDITY"], out double maxHumidity);

            if (success)
            {
                MAXHUMIDITY = maxHumidity;
            }
            else
            {
                MAXHUMIDITY = 100;
            }
        }
        private static void GetDecimalPointHumidity()
        {
            bool success = int.TryParse(ConfigurationManager.AppSettings["DECIMALPOINTUMIDITY"], out int decimalPointHumidity);

            if (success)
            {
                if (decimalPointHumidity >= 0 & decimalPointHumidity <= 10)
                    DECIMALPOINTUMIDITY = decimalPointHumidity;
            }
            else
            {
                DECIMALPOINTUMIDITY = 2;
            }
        }

        private static void GetMaxSensors()
        {
            bool success = int.TryParse(ConfigurationManager.AppSettings["MAXSENSORS"], out int maxSensors);

            if (success)
            {
                if (maxSensors >= 1)
                {
                    MAXSENSORS = maxSensors;
                }
            }
            else
            {
                MAXSENSORS = 10;
            }
        }

        private static void GetMaxCalibData()
        {
            bool success = int.TryParse(ConfigurationManager.AppSettings["MAXCALIBDATA"], out int maxCalibData);

            if (success)
            {
                if (maxCalibData >= 1)
                {
                    MAXCALIBDATA = maxCalibData;
                }
            }
            else
            {
                MAXCALIBDATA = 10;
            }
        }

        private static void GetCertificateBeforeToday()
        {
            bool success = int.TryParse(ConfigurationManager.AppSettings["MONTHBEFORETODAY"], out int certificateBeforeToday);

            if (success)
            {
                if (certificateBeforeToday >= 1)
                {
                    MONTHBEFORETODAY = certificateBeforeToday;
                }
            }
            else
            {
                MONTHBEFORETODAY = 1;
            }
        }

        private static void GetTemplateFolder()
        {
            string templateFolder = ConfigurationManager.AppSettings["TEMPLATEFOLDER"];

            // Check folder exsit
            bool isFolderExist = Directory.Exists(templateFolder);

            if (isFolderExist)
            {
                TEMPLATEFOLDER = templateFolder;
            }
            else
            {
                string currentDirectoryPath = Directory.GetCurrentDirectory();
                string templateFolderPath = Path.Combine(currentDirectoryPath, "CertificateTemplates");
                TEMPLATEFOLDER = templateFolderPath;
            }
        }

        #endregion


    }
}
