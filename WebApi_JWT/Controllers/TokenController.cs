using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi_JWT.Models;
using WebApi_JWT.Provider;

namespace WebApi_JWT.Controllers
{
    public class TokenController : Controller
    {
        [Route("api/CreateToken")]
        [AllowAnonymous]
        [HttpPost]
        [Produces("application/json")]
        public IActionResult CreateToken([FromBody] Usuario user)
        {
            if (user.Name != "Lucas" || user.Password != "*biel2016")
                return Unauthorized();

            var token = new TokenJWTBuilder()
                .AddSecurityKey(Provider.JWTSecurityKey.Create("Secret_Key-0123456789"))
                .AddSubject("Lucas Lima")
                .AddIssuer("Teste.Security.Bearer")
                .AddAudience("Teste.Security.Bearer")
                .AddClaim("UsuarioAPINumero", "1")
                .AddExpiryInMinutes(5)
                .Builder();

            return Ok(token.Value);
        }
    }
}
