using SERU_BACKEND_TES.Models;

namespace SERU_BACKEND_TES.Interface
{
    public interface LoginIntf
    {
        Task<Users> AuthenticateUserAsync(string name);
        Task<Users> VerifyUserAsync(int Id);
        //Task<Dictionary<string, object>> AuthenticateUser(string name,string pass);
        //Task<string> generateToken(string username, int Id);
        //Task<User> getRefreshTokenAsync(string username);
        //bool Logout(string username);
    }
}
