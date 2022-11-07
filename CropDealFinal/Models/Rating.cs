using System.Text.Json.Serialization;

namespace CaseStudy.Models
{
    public class Rating
    {
        public int RatingId { get; set; }
        public int TotalRating { get; set; }
        public string Review { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}
