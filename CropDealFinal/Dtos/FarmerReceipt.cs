using CaseStudy.Models;

namespace CaseStudy.Dtos
{
    public class FarmerReceipt
    {
        public int InvoiceId { get; set; }
        public long DealerAccNumber { get; set; }
        public long FarmerAccNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string CropName { get; set; }
        public string CropType { get; set; }
        public Decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
