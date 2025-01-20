using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementModels
{
    public class UserModel
    {
        [Key]
        [Required]
        [DisplayName("User ID")]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(100,MinimumLength =16, ErrorMessage ="Username must be at least 16 characters.")]
        [DisplayName("Username")]
        public string UserName { get; set; }

        [Required]
        public string PasswordHash { get; set; }
        public string Email { get; set; }

        [Required]
        [DisplayName("PhoneNumber")]
        [StringLength(11,MinimumLength =11, ErrorMessage = "Phone number must be 11 characters.")]
        public string PhoneNumber { get; set; }
    }
}
