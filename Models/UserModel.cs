using System.ComponentModel.DataAnnotations;

namespace Milestone_cst_350.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [Display(Name = "First Name"), Required]
        [StringLength(40, ErrorMessage = "Input must be between 1 - 40 characters", MinimumLength = 1)]
        public string Firstname { get; set; }

        [Display(Name = "Last Name"), Required]
        [StringLength(40, ErrorMessage = "Input must be between 1 - 40 characters", MinimumLength = 1)]
        public string Lastname { get; set; }

        [Display(Name = "Gender (Sex)"), Required]
        [StringLength(40, ErrorMessage = "Input must be between 1 - 40 characters", MinimumLength = 1)]
        public string Sex { get; set; } // All the time :smirk:

        [Display(Name = "Age"), Required]
        [Range(0, 120, ErrorMessage = "Age must be in the range 0 - 120")]
        public int Age { get; set; }

        [Display(Name = "State"), Required]
        [StringLength(40, ErrorMessage = "Input must be between 1 - 40 characters", MinimumLength = 1)]
        public string State { get; set; }

        [Display(Name = "Email Address"), Required]
        //[StringLength(40, ErrorMessage = "Input must be between 1 - 40 characters", MinimumLength = 1)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Username"), Required]
        [StringLength(40, ErrorMessage = "Input must be between 1 - 40 characters", MinimumLength = 1)]
        public string Username { get; set; }

        [Display(Name = "Password"), Required]
        //[StringLength(40, ErrorMessage = "Input must be between 1 - 40 characters", MinimumLength = 1)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public UserModel()
        {
            // ...
        }

        public UserModel(int id, string firstname, string lastname, string sex, int age, string state, string email, string username, string password)
        {
            Id = id;
            Firstname = firstname;
            Lastname = lastname;
            Sex = sex;
            Age = age;
            State = state;
            Email = email;
            Username = username;
            Password = password;
        }
    }
}
