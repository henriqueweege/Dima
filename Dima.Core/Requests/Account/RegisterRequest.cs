using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Account
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "E-mail must be provided.")]
        [EmailAddress(ErrorMessage = "E-mail invalid.")]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage = "Password invalid.")]
        public string Password { get; set; }
        public string UserId { get; set; } = default!;
    }
}
