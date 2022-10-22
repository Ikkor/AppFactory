using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class UserRegisterVm
    {

        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "An Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "A password is required")]
        [MinLength(6, ErrorMessage = "Minimum of 6 characters required")]
        [DataType(DataType.Password)]

        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage="Password do not match")]
        public string ConfirmPassword { get; set; }


        public Role Role { get; set; }
        public int? StudentId { get; set; }
    }
}