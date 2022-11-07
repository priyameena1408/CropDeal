using CaseStudy.Models;
using System.ComponentModel.DataAnnotations;

namespace CaseStudy.Dtos.UserDtos
{
    public class CreateUserDto
    {
        [Required]
        public string Role { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string Phone { get; set; } = null!;

        public string Status = "Active";

        //Account
        [Required]
        public string Line { get; set; } = null!;
        [Required]
        public string City { get; set; } = null!;
        [Required]
        public string State { get; set; } = null!;


        //Account
        [Required]
        public long AccountNumber { get; set; }
        [Required]
        public string IFSC { get; set; } = null!;
        [Required]
        public string BankName { get; set; } = null!;


    }
}
