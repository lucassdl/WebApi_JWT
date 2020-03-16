using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebApi_JWT.Provider
{
    public class JWTSecurityKey
    {
        public static SymmetricSecurityKey Create(string secret)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        }
    }
}
