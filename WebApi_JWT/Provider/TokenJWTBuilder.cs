using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace WebApi_JWT.Provider
{
    public class TokenJWTBuilder
    {
        #region Atributos Privados
        private SecurityKey SecurityKey { get; set; } = null;
        private string Subject { get; set; } = string.Empty;
        private string Issuer { get; set; } = string.Empty;
        private string Audience { get; set; } = string.Empty;
        private Dictionary<string, string> Claims { get; set; } = new Dictionary<string, string>();
        private int ExpiryInMinutes { get; set; } = 5;
        #endregion

        #region Métodos Públicos
        public TokenJWTBuilder AddSecurityKey(SecurityKey securityKey)
        {
            SecurityKey = securityKey;
            return this;
        }

        public TokenJWTBuilder AddSubject(string subject)
        {
            Subject = subject;
            return this;
        }

        public TokenJWTBuilder AddIssuer(string issuer)
        {
            Issuer = issuer;
            return this;
        }

        public TokenJWTBuilder AddAudience(string audience)
        {
            Audience = audience;
            return this;
        }

        public TokenJWTBuilder AddClaim(string type, string value)
        {
            Claims.Add(type, value);
            return this;
        }

        public TokenJWTBuilder AddClaims(Dictionary<string, string> claims)
        {
            Claims.Union(claims);
            return this;
        }

        public TokenJWTBuilder AddExpiryInMinutes(int expiryInMinutes)
        {
            ExpiryInMinutes = expiryInMinutes;
            return this;
        }

        public TokenJWT Builder()
        {
            EnsureArguments();

            var claims = new List<Claim>(){
                new Claim(JwtRegisteredClaimNames.Sub, this.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            }.Union(this.Claims.Select(item => new Claim(item.Key, item.Value)));

            var token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(ExpiryInMinutes),
                signingCredentials: new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256)
                );

            return new TokenJWT(token);
        }

        private void EnsureArguments()
        {
            if (SecurityKey == null)
                throw new ArgumentNullException("SecurityKey");

            if (string.IsNullOrEmpty(Subject))
                throw new ArgumentNullException("Subject");

            if (string.IsNullOrEmpty(Issuer))
                throw new ArgumentNullException("Issuer");

            if (string.IsNullOrEmpty(Audience))
                throw new ArgumentNullException("Audience");
        }
        #endregion
    }
}
