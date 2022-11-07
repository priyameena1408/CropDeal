namespace CaseStudy.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public long AccountNumber  { get; set; }
        public string IFSCCode { get; set; } = null!;
        public string BankName { get; set; } = null!;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
