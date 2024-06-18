using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace NannyNook.Models.DTOs;
public class UserProfileDTO
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }

    [Required]
    [NotMapped]
    public string UserName { get; set; }

    [Required]
    [NotMapped]
    public string Email { get; set; }

    public string? Bio { get; set; }

    public DateTime CreateDateTime { get; set; }

    [MaxLength(255)]
    [Required]
    public string Location { get; set; }

    public string? ProfileImage { get; set;}

    public bool IsNanny { get; set; }

    public bool IsParent { get; set; }


    public string IdentityUserId { get; set; }

    public IdentityUser IdentityUser { get; set; }

    public string FullName
    {
        get
        {
            return $"{FirstName} {LastName}";
        }
    }
    public ICollection<RSVPDTO> RSVPs { get; set; }
    public ICollection<OccasionDTO> HostedOccasions { get; set; }
}