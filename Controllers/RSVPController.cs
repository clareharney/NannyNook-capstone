using Microsoft.AspNetCore.Mvc;
using NannyNook.Models;
using NannyNook.Models.DTOs;
using NannyNookcapstone.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace NannyNook.Controllers;

[ApiController]
[Route("api/[controller]")]

public class RSVPController : ControllerBase
{
    private NannyNookDbContext _dbContext;

    public RSVPController(NannyNookDbContext context)
    {
        _dbContext = context;
    }

    // [HttpGet]
    // public IActionResult GetRSVPs()
    // {
    //     return Ok(_dbContext.RSVPs.ToList());
    // }

//     [HttpGet]
// public IActionResult GetRSVPs()
// {
//     var rsvp = _dbContext.RSVPs
//         .Include(r => r.UserProfile)
//         .Include(r => r.Occasion)

//     if (rsvp == null)
//     {
//         return NotFound();
//     }

//     return Ok(rsvp);
// }

[HttpGet]
        public IActionResult GetRSVPs()
        {
            var rsvps = _dbContext.RSVPs
                .Include(r => r.UserProfile)
                .Include(r => r.Occasion)
                .Select(r => new RSVPDTO
                {
                    UserProfileId = r.UserProfileId,
                    OccasionId = r.OccasionId,
                    UserProfile = new UserProfileDTO
                    {
                        Id = r.UserProfile.Id,
                        FirstName = r.UserProfile.FirstName,
                        LastName = r.UserProfile.LastName,
                        Bio = r.UserProfile.Bio,
                        Location = r.UserProfile.Location,
                        ProfileImage = r.UserProfile.ProfileImage,
                        IsNanny = r.UserProfile.IsNanny,
                        IsParent = r.UserProfile.IsParent
                    },
                    Occasion = new OccasionDTO
                    {
                        Id = r.Occasion.Id,
                        Title = r.Occasion.Title,
                        Description = r.Occasion.Description,
                        State = r.Occasion.State,
                        City = r.Occasion.City,
                        Location = r.Occasion.Location,
                        CategoryId = r.Occasion.CategoryId,
                        Date = r.Occasion.Date,
                        OccasionImage = r.Occasion.OccasionImage,
                        HostUserProfileId = r.Occasion.HostUserProfileId
                    }
                })
                .ToList();

            if (!rsvps.Any())
            {
                return NotFound();
            }

            return Ok(rsvps);
        }

    [HttpDelete("{userProfileId}/{occasionId}")]
        public IActionResult UnRSVP(int userProfileId, int occasionId)
        {
            try
            {
                var rsvp = _dbContext.RSVPs
                    .FirstOrDefault(r => r.UserProfileId == userProfileId && r.OccasionId == occasionId);

                if (rsvp == null)
                {
                    return NotFound("RSVP not found.");
                }

                _dbContext.RSVPs.Remove(rsvp);
                _dbContext.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    [HttpPost]
public IActionResult NewRSVP(RSVP rsvp)
{
    try
    {
        var existingRSVP = _dbContext.RSVPs
            .FirstOrDefault(r => r.UserProfileId == rsvp.UserProfileId && r.OccasionId == rsvp.OccasionId);

        if (existingRSVP != null)
        {
            return BadRequest(new { message = "RSVP already exists." });
        }

        // Ensure the Occasion exists
        var occasion = _dbContext.Occasions
            .FirstOrDefault(o => o.Id == rsvp.OccasionId);

        if (occasion == null)
        {
            return BadRequest(new { message = "Occasion does not exist." });
        }

        // Ensure the UserProfile exists
        var userProfile = _dbContext.UserProfiles
            .FirstOrDefault(up => up.Id == rsvp.UserProfileId);

        if (userProfile == null)
        {
            return BadRequest(new { message = "UserProfile does not exist." });
        }

        _dbContext.RSVPs.Add(rsvp);
        _dbContext.SaveChanges();

        return Created($"/api/RSVP/{rsvp.UserProfileId}/{rsvp.OccasionId}", rsvp);
    }
    catch (DbUpdateException ex)
    {
        // Log the detailed error message for further investigation
        var errorMessage = ex.InnerException?.Message ?? ex.Message;
        return StatusCode(500, new { message = $"Internal server error: {errorMessage}" });
    }
    catch (Exception ex)
    {
        return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
    }
}



}