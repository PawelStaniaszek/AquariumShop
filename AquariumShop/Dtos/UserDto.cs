using System.ComponentModel.DataAnnotations;

namespace AquariumShop.Dtos
{
    public class UserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(15, ErrorMessage ="Your Password is limited to {2} to {1} characters", MinimumLength =6)]
        public string Password { get; set; }
    }
}
