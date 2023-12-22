using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/v1/token")]
    public class TokenController : ControllerBase
    {
        private ILogger<TokenController> _logger;
        public TokenController(ILogger<TokenController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Obsolete]
        public ActionResult GenToken()
        {
            var _secretKey = "p5PvtN2GczOSiI8u8H2Qvfxlg4ZazHCQPSDm5u6b3HQ=";  // Remove the "!" from the key declaration
            var _audience = "winona";
            var _issuer = "winona";
            var key = Encoding.UTF8.GetBytes(_secretKey);

            using (var sha256 = new System.Security.Cryptography.SHA256Managed())
            {
                key = sha256.ComputeHash(key);
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                claims: new List<Claim>
                {
            new Claim(ClaimTypes.Role, "Editor"), // ใช้ค่าจาก boyRow เป็น Role
                                                // เพิ่ม claims อื่น ๆ ตามต้องการ
                }
            );

            var tokenString = tokenHandler.WriteToken(tokenDescriptor);

            return Ok(tokenString);
        }

    }
}