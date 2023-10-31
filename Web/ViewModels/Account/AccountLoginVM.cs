using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace Web.ViewModels.Account
{
    public class AccountLoginVM
    {
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
