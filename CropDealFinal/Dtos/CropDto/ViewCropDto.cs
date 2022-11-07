using System.ComponentModel.DataAnnotations;

namespace CaseStudy.Dtos.CropDto
{
    public class ViewCropDto
    {
        [Required]
        public string CropType { get; set; } = null!;

        [Required]
        public string CropName { get; set; } = null!;

        [Required]
        public string CropLocation { get; set; } = null!;

        [Required]
        public int CropQtyAvailable { get; set; }

        [Required]
        public decimal CropExpectedPrice { get; set; }

        [Required]
        public string FarmerName { get; set; } = null!;

        [Required]
        public string FarmerEmail { get; set; } = null!;

        [Required]
        public string FarmerPhone { get; set; } = null!;
        public int FarmerId { get; set; }
        public string CropImg { get; set; }
        public string FarmerRating { get; set; } 
    }
}
