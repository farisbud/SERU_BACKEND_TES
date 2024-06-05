using Azure;
using Azure.Core;
using Dapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SERU_BACKEND_TES.Helper;
using SERU_BACKEND_TES.Interface;
using SERU_BACKEND_TES.Models;
using SERU_BACKEND_TES.Models.Data;
using SERU_BACKEND_TES.ViewModels.JwtSetting;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SERU_BACKEND_TES.Services
{
    public class LoginSvc : LoginIntf
    {
        private readonly DapperDBContext _dapperDB;

        private readonly JwtSettings _jwtSettings;
        public LoginSvc(DapperDBContext dapperDB, IOptions<JwtSettings> jwtSettings)
        {
            _dapperDB = dapperDB;
            _jwtSettings = jwtSettings.Value;
        }

        //public async Task<Dictionary<string, object>> AuthenticateUser(string name, string password)
        //{
        //    var res = new Dictionary<string, object>();

        //    try
        //    {
        //        var db = _dapperDB.CreateConnection();

        //        string sql = @"Select id, [name], password, [is_admin] from users where [name] = @name";

        //        var result = await db.QueryFirstAsync<Users>(sql, new
        //        {
        //            @name = name,
        //        }).ConfigureAwait(false);

        //        if (result != null)
        //        {
        //            if (Hashing.ValidatePassword(password, result.password))
        //            {
        //                var dataToken = new Dictionary<string, object>();
        //                var tokenhandler = new JwtSecurityTokenHandler();
        //                var tokenkey = Encoding.UTF8.GetBytes(_jwtSettings.securityKey);
        //                var tokenDescriptor = new SecurityTokenDescriptor
        //                {
        //                    Subject = new ClaimsIdentity(
        //                                new List<Claim>
        //                                {
        //                                        new Claim(ClaimTypes.Sid, result.id.ToString()),
        //                                        new Claim(ClaimTypes.Name, result.name)
        //                                        , new Claim(ClaimTypes.Role, true ? "admin" : "user")
        //                                }
        //                            ),
        //                    Expires = DateTime.UtcNow.AddHours(2),
        //                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)
        //                };

        //                var token = tokenhandler.CreateToken(tokenDescriptor);

        //                string finalToken = tokenhandler.WriteToken(token);
                        
        //                List<Dictionary<string,object>> data = new List<Dictionary<string,object>>();

        //                dataToken["name"] = result.name;
        //                dataToken["token"] = finalToken;
        //                dataToken["level"] = result.is_admin ? "admin" : "user";

        //                data.Add(dataToken);

                     
        //                res["status"] = 200;
        //                res["message"] = "berhasil login";
        //                res["data"] = data;
        //            }
        //            else
        //            {
        //                res["status"] = 406;
        //                res["message"] = "nama atau password salah!!";
        //            }
        //        }
        //        else
        //        {
        //            res["status"] = 406;
        //            res["message"] = "nama atau password salah!!";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        res["status"] = 400;
        //        res["message"] = ex.Message.ToString();
        //    }
        //    return res;
        //}

        public async Task<Users> AuthenticateUserAsync(string name)
        {
            var db = _dapperDB.CreateConnection();

            string sql = @"Select id, name, password, is_admin from users where name = @name";

            var result = await db.QueryAsync<Users>(sql, new
            {
                @name = name,
            }).ConfigureAwait(false);

            return result.FirstOrDefault();
        }

        public async Task<Users> VerifyUserAsync(int id)
        {
            var db = _dapperDB.CreateConnection();

            string sql = @"Select id, name, password, is_admin from users where id = @userId";

            var result = await db.QueryAsync<Users>(sql, new
            {
                @userId = id,
            }).ConfigureAwait(false);

            return result.FirstOrDefault();
        }
    }
}
