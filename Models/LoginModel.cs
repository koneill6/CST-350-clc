using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Milestone_cst_350.Models
{

    public class LoginModel 
    {
        // Login properties -> Username and Password
        [Required]
        [StringLength(40)]
        public string Username { get; set; }

        [Required]
        [StringLength(40)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
