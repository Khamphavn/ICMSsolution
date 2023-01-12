using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.HelperFunction
{
    public static class PasswordHelper
    {
        public static string GenerateSHA512String(string inputString)
        {
            byte[] data = Encoding.UTF8.GetBytes(inputString);
            using (SHA512 shaM = new SHA512Managed())
            {
                byte[] result = shaM.ComputeHash(data);
                return Convert.ToBase64String(result);

            }
        }
    }
}