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

    [HttpPut]
    [Authorize]
    
    public IActionResult UnRSVP(RSVP rsvp)
    {
        RSVP rsvpToUpdate = _dbContext.RSVPs.SingleOrDefault((r) => r == rsvp);
        if(rsvpToUpdate == null)
        {
            return NotFound();
        }

        _dbContext.SaveChanges();

        return NoContent();
    }

    [HttpPost]
    public IActionResult NewRSVP(RSVP rsvp)
    {
        RSVP rsvpToAdd = new RSVP
        {
            OccasionId = rsvp.OccasionId,
            UserProfileId = rsvp.UserProfileId
        };
        
        RSVP foundRsvp = _dbContext.RSVPs.SingleOrDefault((r) => r == rsvp);
        if(foundRsvp == null)
        {
            _dbContext.RSVPs.Add(rsvpToAdd);

            _dbContext.SaveChanges();
        }
        else
        {
            _dbContext.RSVPs.Remove(foundRsvp);
            _dbContext.RSVPs.Add(rsvpToAdd);
            _dbContext.SaveChanges();        
        }
        return Created($"/api/RSVP/{rsvp}", rsvp);

    }


}