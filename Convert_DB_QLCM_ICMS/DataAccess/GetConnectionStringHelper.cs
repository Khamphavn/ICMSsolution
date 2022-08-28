using System.Configuration;

namespace Convert_DB_QLCM_ICMS.DataAccess
{
    public static class GetConnectionStringHelper
    {
        public static string GetConnectionString(string conName)
        {
            string strReturn = string.Empty;
            if (!(string.IsNullOrEmpty(conName)))
            {
                strReturn = ConfigurationManager.ConnectionStrings[conName].ConnectionString;
            }
            else
            {
                strReturn = null;
            }
            return strReturn;
        }
    }
}
