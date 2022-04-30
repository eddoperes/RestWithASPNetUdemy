﻿using Microsoft.IdentityModel.Tokens;
using RestWithASPNetUdemy.Configurations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RestWithASPNetUdemy.Service.Implementations
{
    public class TokenServiceImplementation : ITokenService
    {

        private TokenConfiguration _tokenConfiguration;

        public TokenServiceImplementation(TokenConfiguration tokenConfiguration) {
            _tokenConfiguration = tokenConfiguration;
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfiguration.Secret));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var options = new JwtSecurityToken(
                issuer : _tokenConfiguration.Issuer,
                audience : _tokenConfiguration.Audience,
                claims :claims,
                expires: DateTime.Now.AddMinutes(_tokenConfiguration.Minutes)   ,
                signingCredentials: signingCredentials
            );
            string tokenString = new JwtSecurityTokenHandler().WriteToken(options);
            return tokenString;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create()) {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);  
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters { 
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfiguration.Secret)),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;    
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (securityToken == null ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCulture))
            {
                throw new SecurityTokenException("Invalid token");  
            }
            return principal;
        }

    }

}