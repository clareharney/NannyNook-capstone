using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace NannyNook.Models
{
    public class Job
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public decimal? PayRateMin { get; set; }
        [Required]
        public decimal PayRateMax { get; set; }
        [Required]
        public int NumberOfKids { get; set; }
        [Required]
        public bool FullTime { get; set; }
        [Required]
        public string ContactInformation { get; set; }

        public int PosterId {get; set; }

        // Navigation properties
        public UserProfile? Poster { get; set; }
    }
}
