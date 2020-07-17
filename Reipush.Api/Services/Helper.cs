using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Reipush.Api.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
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

        public static string GenerateToken(int id, string email, bool isAdmin, string tokenSecret)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSecret));
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Sid, id.ToString()));
            claims.Add(new Claim(ClaimTypes.Email, email));
            claims.Add(new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Email, email));
            if (isAdmin)
            {
                claims.Add(new Claim("AdminUser", ""));
            }
            var token = new JwtSecurityToken(
                issuer: "reipush.com",
                audience: "reipush.com",
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            ); 
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string GenerateToken(IEnumerable<Claim> claims, string tokenSecret)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSecret));
            var token = new JwtSecurityToken(
                issuer: "reipush.com",
                audience: "reipush.com",
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        

        public static ClaimsPrincipal GetPrincipalFromExpiredToken(string token, string tokenSecret)
        {
            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "reipush.com",
                    ValidAudience = "reipush.com",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSecret)),
                    ValidateLifetime = false
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
                var jwtSecurityToken = securityToken as JwtSecurityToken;
                if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    throw new SecurityTokenException("Invalid token");

                return principal;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                throw new SecurityTokenException("Invalid token");
            }
        }
        
    }



}
