using System;
using System.IdentityModel.Tokens.Jwt;

namespace WebApi_JWT.Provider
{
    public class TokenJWT
    {
        public JwtSecurityToken _token;

        public TokenJWT(JwtSecurityToken token)
        {
            _token = token;
        }

        public DateTime ValidTo => _token.ValidTo;
        public string Value => new JwtSecurityTokenHandler().WriteToken(this._token);
    }
}
