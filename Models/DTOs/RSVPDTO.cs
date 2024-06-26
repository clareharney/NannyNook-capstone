using System.ComponentModel.DataAnnotations;


namespace NannyNook.Models.DTOs;

public class RSVPDTO
    {
        
        [Required]
        public int UserProfileId { get; set; }
        
        [Required]
        public int OccasionId { get; set; }

        // Navigation properties
        public UserProfileDTO? UserProfile { get; set; }
        public OccasionDTO? Occasion { get; set; }
    }