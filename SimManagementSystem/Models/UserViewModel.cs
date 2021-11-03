using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimManagementSystem.Models 
{
    public class UserViewModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Password { get; set; }
        public string Name { get; set; }
        public IEnumerable<Menu_ID> Check { get; set; }
        public bool isActive { get; set; }
        public bool isAdmin { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedDateString { get; set; }
    }
    public class ChangePassword
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        [Display(Name = "Old Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Display(Name = "New Password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
    public class LoginbyMobileNo
    {
        [Required]
        public int MobileNo { get; set; }
    }
}