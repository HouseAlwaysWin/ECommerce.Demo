using System;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Demo.API.Domain.DTO {
    public class UserRegisterDto {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength (9, MinimumLength = 4, ErrorMessage = "You must specify password between 4 and 8 character")]
        public string Password { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string KnownAs { get; set; }

        // [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public UserRegisterDto () {
            Created = DateTime.Now;
            LastActive = DateTime.Now;
        }
    }
}