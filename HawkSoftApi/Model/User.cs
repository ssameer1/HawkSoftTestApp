using System.ComponentModel.DataAnnotations;

namespace HawkSoftApi.Model
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Address { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }

    }
}
