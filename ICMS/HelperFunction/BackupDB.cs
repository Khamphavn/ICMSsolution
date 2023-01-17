using ICMS.Model.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.HelperFunction
{
    public static class BackupDB
    {
        public static int BackupDatabase(string backupFolderFullPath)
        {
            using (var connection = new SqlConnection(GlobalConfig.CnnString("ICMSdatabase")))
            {
                string databaseName = connection.Database;

                string fileName = string.Format("{0}-autobackup-{1}.bak", databaseName, DateTime.Now.ToString("yyyy-MMM-dd_HH_mm"));

                string filePath = Path.Combine(backupFolderFullPath, fileName);

                var query = String.Format("BACKUP DATABASE [{0}] TO DISK='{1}'", databaseName, filePath);

                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                return 1;
            }
        }
    }
}
