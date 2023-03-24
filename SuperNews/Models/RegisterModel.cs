using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SuperNews.Models
{
    public class RegisterModel
    {
        [Required]
        public string Email { get; set; }

        public string Role { get; set; }

        public List<IdentityRole> AllRoles { get; set; }

        public IEnumerable<string> Roles { get; set; }

        public IList<string> UserRoles { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

        public RegisterModel()
        {
            AllRoles = new List<IdentityRole>();
            UserRoles = new List<string>();
        }
    }
}
