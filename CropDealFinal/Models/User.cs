using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CaseStudy.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Phone { get; set; }
        public string? Status { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        
        public Account Account { get; set; }
        
        public Address Address { get; set; }
        
        public IEnumerable<Rating> Ratings { get; set; }
        [JsonIgnore]
        public IEnumerable<CropDetail> CropDetails { get; set; }  = null!;
        [JsonIgnore]
        public IEnumerable<Invoice> FarmerInvoices { get; set; }
        [JsonIgnore]
        public IEnumerable<Invoice> DealerInvoices { get; set; }
    }
}
