using CaseStudy.Models;
using System.ComponentModel.DataAnnotations;

namespace CaseStudy.Dtos
{
    public class RatingDto
    {
	    [Required]
        public int TotalRating { get; set; } = 0;
	    [Required]
        public string Review { get; set; } = null!;
    }
}
