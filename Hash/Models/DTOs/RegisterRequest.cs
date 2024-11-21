using System.ComponentModel.DataAnnotations;

namespace Hash.Models.DTOs
{
    public class RegisterRequest
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public bool IsAdmin { get; set; } = false;
    }
} 