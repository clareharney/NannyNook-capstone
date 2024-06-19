using System.ComponentModel.DataAnnotations;

namespace NannyNook.Models;

public class Occasion
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string State { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string City { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Location { get; set; }
        
        [Required]
        public int CategoryId { get; set; }
        
        [Required]
        public DateTime Date { get; set; }
        
        public string? OccasionImage { get; set; }
        
        public int HostUserProfileId { get; set; }

        // Navigation properties
        public UserProfile? HostUserProfile { get; set; }
        public Category? Category { get; set; }

        public List<RSVP>? RSVPs { get; set; }

        //calculated properties

        public string FormattedDate => Date.ToString("MMMM dd, yyyy, hh:mm");
        
}