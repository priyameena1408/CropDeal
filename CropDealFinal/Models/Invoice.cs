using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CaseStudy.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public Decimal Amount { get; set; }
        public DateTime InvoiceDate { get; set; }
        [ForeignKey("UserId")]
        public int FarmerId { get; set; }
       
        [ForeignKey("UserId")]
        public int DealerId { get; set; } 
       
        [ForeignKey("CropDetail")]
        public int CropId { get; set; }
        
        public CropDetail CropDetail { get; set; }
        
        public User Farmer { get; set; }
        
        public User Dealer { get; set; }
    }
}
