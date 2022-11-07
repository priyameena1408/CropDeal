using System.ComponentModel.DataAnnotations;

namespace CaseStudy.Models
{
    public class Subscription
    {
        [Key]
        public int SubId { get; set; }
        public int DealerId { get; set; }
        public int CropTypeId {get;set;}
    }
}
