using System.ComponentModel.DataAnnotations;

namespace NannyNook.Models;

public class Category
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<Occasion> Occasions { get; set; }
    }