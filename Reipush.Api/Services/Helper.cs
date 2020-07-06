using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reipush.Api.Services
{
    public static class Helper
    {

        /// <summary>
        /// Returns the hash of the given string. 
        /// </summary>
        /// <param name="stringToHash" />string for which the hash should be generated
        /// <param name="hashAlgorithm" />Hash algorithm. Ex: MD5, SHA1, SHA256, SHA384, SHA512
        /// <returns></returns>
        public static string GetHash(this string stringToHash, string hashAlgorithm)
        {
            var algorithm = System.Security.Cryptography.HashAlgorithm.Create(hashAlgorithm);
            byte[] hash = algorithm.ComputeHash(System.Text.Encoding.UTF8.GetBytes(stringToHash));

            // ToString("x2")  converts byte in hexadecimal value
            string encryptedVal = string.Concat(hash.Select(b => b.ToString("x2"))).ToUpperInvariant();
            return encryptedVal;
        }
    }
}
