using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace RepManagement.Common
{
    public static class UtilFunc
    {
        public static string SHA256(string str)
        {
            
            byte[] SHA256Data = Encoding.UTF8.GetBytes(str);

            SHA256Managed Sha256 = new SHA256Managed();
            byte[] by = Sha256.ComputeHash(SHA256Data);

            return BitConverter.ToString(by).Replace("-", "").ToLower(); //64
                                    
        }
    }
}
