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

    [HttpGet]
    public IActionResult GetRSVPs()
    {
        return Ok(_dbContext.RSVPs.ToList());
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