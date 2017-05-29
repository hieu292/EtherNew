using System.ComponentModel.DataAnnotations;

namespace AirDoc.Controllers.Account.Models
{
    public class ForgotPasswordModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }
}
