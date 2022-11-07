using System.ComponentModel.DataAnnotations;

namespace CaseStudy.Models
{
    public class ExceptionError
    {
        [Key]
        public int Id { get; set; }
        public string ExceptionMessage { get; set; }
        public string ExceptionDateTime { get; set; }
        public string ExceptionAt { get; set; }
        public string ExceptionType { get; set; }
    }
}
