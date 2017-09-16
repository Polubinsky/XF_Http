using PCLCrypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UkrGo.Extensions
{
    static class HashExtension
    {
        public static string GetSHA(this string s)
        {
            byte[] data = Encoding.UTF8.GetBytes(s);
            var hasher = WinRTCrypto.HashAlgorithmProvider.OpenAlgorithm(HashAlgorithm.Md5);
            byte[] hash = hasher.HashData(data);
            string hashBase64 = Convert.ToBase64String(hash);
            return hashBase64;
        }
    }
}
