using System.ComponentModel.DataAnnotations;
using EduNurse.Api.Shared.Command;

namespace EduNurse.Auth.Shared.Commands
{
    public class RegisterCommand : ICommand
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
