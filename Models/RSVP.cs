using System.ComponentModel.DataAnnotations;


namespace NannyNook.Models;

public class RSVP
    {

        [Required]
        public int UserProfileId { get; set; }
        
        [Required]
        public int OccasionId { get; set; }

        // Navigation properties
        public UserProfile? UserProfile { get; set; }
        public Occasion? Occasion { get; set; }
    }