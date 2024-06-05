using System.ComponentModel.DataAnnotations;

namespace SERU_BACKEND_TES.ViewModels.LoginVM
{
    public class Login
    {
        [Required(ErrorMessage = "Username tidak boleh kosong !!")]
        public string name { get; set; }
        [Required(ErrorMessage = "Password tidak boleh kosong !!")]
        public string password { get; set; }
    }

    public class LoginResp
    {
        public string name { get; set; }
        public string token { get; set; }
        public string level { get; set; }
    }

}