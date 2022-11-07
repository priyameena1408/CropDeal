using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CaseStudy.Models
{
    public class CropDetail
    {
        [Key]
        public int CropId { get; set; }
        public int CropTypeId { get; set; }
        [ForeignKey("User")]
        public int FarmerId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public string CropName { get; set; } = null!;
        public string? CropImage { get; set; }
        public string Location { get; set; } = null!;
        public int QtyAvailable { get; set; }
        public decimal ExpectedPrice { get; set; }
        [JsonIgnore]
        public CropType CropType { get; set; }
        [JsonIgnore]
        public IEnumerable<Invoice> Invoices { get; set; }
    }
}
