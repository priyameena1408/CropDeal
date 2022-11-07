namespace CaseStudy.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public string Line { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public int PinCode { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } 
    }
}
