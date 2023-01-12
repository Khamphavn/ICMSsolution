using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GeneratePassword
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string passwordAdmin = GenerateSHA512String("admin");
            string passwordQM = GenerateSHA512String("123");
            string passwordTM = GenerateSHA512String("123");
            string passwordTechnical = GenerateSHA512String("123");
            string passwordViewer = GenerateSHA512String("123");



            string password_lnThiem = GenerateSHA512String("123");
            string password_hqTuan = GenerateSHA512String("123");
            string password_bdKy = GenerateSHA512String("123");
            string password_nnQuynh = GenerateSHA512String("123");
            string password_dtmLinh = GenerateSHA512String("123");
            string password_dtNhung = GenerateSHA512String("123");
            string password_tvTrung = GenerateSHA512String("123");
            string password_ndNguyen = GenerateSHA512String("123");
            string password_btaDuong = GenerateSHA512String("123");





            Console.ReadLine();
        }

        private static string GenerateSHA512String(string inputString)
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
