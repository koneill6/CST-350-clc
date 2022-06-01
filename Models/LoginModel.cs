using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Milestone_cst_350.Models
{
    public class LoginModel 
    {

        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
