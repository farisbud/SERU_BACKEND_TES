using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SERU_BACKEND_TES.Helper;
using SERU_BACKEND_TES.Interface;
using SERU_BACKEND_TES.Models;
using SERU_BACKEND_TES.ViewModels.JwtSetting;
using SERU_BACKEND_TES.ViewModels.LoginVM;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SERU_BACKEND_TES.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly LoginIntf _loginIntf;
        private readonly GenerateJwt _generateJwt;
        public AuthController(LoginIntf loginIntf, GenerateJwt generateJwt)
        {
            _loginIntf = loginIntf;
            _generateJwt = generateJwt;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(Login request)
        {
            try
            {
                var user = await _loginIntf.AuthenticateUserAsync(request.name);

                if (user != null && user.password != null)
                {
                    if (Hashing.ValidatePassword(request.password, user.password))
                    {

                        string token = _generateJwt.GenerateToken(user);
                        string cookies = _generateJwt.GenerateCookies(user);

                        var newResp = new LoginResp
                        {
                            name = user.name,
                            token = token,
                            level = user.is_admin ? "admin" : "user"
                        };

                        Response.Cookies.Append("seruu", cookies, new CookieOptions
                        {
                            HttpOnly = true,
                            SameSite = SameSiteMode.None,
                            Expires = DateTime.Now.AddHours(7),
                            Secure = true
                        });

                        return Ok(new { status = 200, message = "berhasil login", data = newResp });
                    }
                    else
                    {
                        return BadRequest(new { status = 406, message = "nama atau password salah!!" });
                    }
                }
                else
                {
                    return BadRequest(new { status = 406, message = "nama atau password salah!!" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            var token = await _generateJwt.Verify(Request.Cookies["seruu"]);

            if (token != null && !token.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
            {
                return Unauthorized();
            }

            if (token == null)
            {
                return Unauthorized();
            }

            var id = token?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;
            Users user = await _loginIntf.VerifyUserAsync(Convert.ToInt32(id));

            if (user == null)
            {
                return Unauthorized();
            }

            try
            {
                var tokenResponse = new LoginResp
                {
                    name = user.name,
                    level = user.is_admin ? "admin" : "user",
                    token = _generateJwt.GenerateRefreshToken(user),
                };

                return Ok(tokenResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }

        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var token = await _generateJwt.Verify(Request.Cookies["seruu"]);

                if (token == null)
                {
                    Response.Cookies.Delete("seruu");
                    return Unauthorized();
                }

                Response.Cookies.Delete("seruu");

                return Ok(new { status = 200, message = "berhasil Logout" });
            }
            catch (Exception ex)
            {
                {
                    return BadRequest();
                }


            }

            //[NonAction]
            //public LoginResp Authenticate(Users user)
            //{

            //}
        }
    }
}
