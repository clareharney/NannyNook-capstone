using System.ComponentModel.DataAnnotations;

namespace NannyNook.Models.DTOs;

public class Occasion2DTO
    {
        [Key]
        public int Id { get; set; }
        
        [MaxLength(100)]
        public string Title { get; set; }
        
        [MaxLength(500)]
        public string Description { get; set; }
        
        [MaxLength(50)]
        public string State { get; set; }
        
        [MaxLength(50)]
        public string City { get; set; }
        
        [MaxLength(100)]
        public string Location { get; set; }
        
        public int CategoryId { get; set; }
        
        public DateTime Date { get; set; }
        
        public string OccasionImage { get; set; }
        
        public int HostUserProfileId { get; set; }
    }