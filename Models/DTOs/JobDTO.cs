using NannyNook.Models.DTOs;

namespace NannyNook.DTOs
{
    public class JobDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal? PayRateMin { get; set; }
        public decimal PayRateMax { get; set; }
        public int NumberOfKids { get; set; }
        public bool FullTime { get; set; }
        public string ContactInformation { get; set; }

        public int PosterId {get; set; }

        // Navigation properties
        public UserProfileDTO? Poster { get; set; }
    }
}
