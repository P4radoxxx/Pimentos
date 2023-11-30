using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace API_piment.Services.Token
{
    public class JWTService
    {
        private readonly string _secretKey;

        public JWTService()
        {
            
            _secretKey = Environment.GetEnvironmentVariable("SECRET_KEY");

            if (string.IsNullOrEmpty(_secretKey))
            {
                // Si la clé n'est pas définie, générer une nouvelle clé
                _secretKey = GenerateRandomKey();
                // Stocker la nouvelle clé dans les variables d'environnement
                Environment.SetEnvironmentVariable("SECRET_KEY", _secretKey);
            }
        }

        public string GenerateToken(string userId, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Convert.FromBase64String(_secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                        new Claim(ClaimTypes.NameIdentifier, userId),
                        new Claim(ClaimTypes.Role, role )

                    }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        
        private string GenerateRandomKey(int length = 32)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var bytes = new byte[length];
                rngCryptoServiceProvider.GetBytes(bytes);
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
