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
            string passwordAdmin = GenerateSHA512String("AdminAdmin");
            string passwordQM = GenerateSHA512String("QMUser123456");
            string passwordTM = GenerateSHA512String("TMUser123456");
            string passwordTechnical = GenerateSHA512String("TechnicalUser123456");
            string passwordViewer = GenerateSHA512String("ViewerUser123456");



            string password_lnThiem = GenerateSHA512String("lnThiem123456");
            string password_hqTuan = GenerateSHA512String("hqTuanhqTuan2022");
            string password_bdKy = GenerateSHA512String("bdKybdKy321");
            string password_nnQuynh = GenerateSHA512String("nnQuynh123");
            string password_dtmLinh = GenerateSHA512String("dtmLinhLinh123456");
            string password_dtNhung = GenerateSHA512String("dtNhungNhung123456");
            string password_tvTrung = GenerateSHA512String("tvTrungTrung123456");
            string password_ndNguyen = GenerateSHA512String("ndNguyenNguyen123456");
            string password_btaDuong = GenerateSHA512String("btaDuong123456");





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
