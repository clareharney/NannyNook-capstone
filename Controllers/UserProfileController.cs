using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NannyNook.Models;
using NannyNook.Models.DTOs;
using NannyNookcapstone.Data;

namespace NannyNook.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UserProfileController : ControllerBase
{
    private NannyNookDbContext _dbContext;

    public UserProfileController(NannyNookDbContext context)
    {
        _dbContext = context;
    }

        [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        UserProfile user = _dbContext
            .UserProfiles
            .Include(up => up.IdentityUser)
            .SingleOrDefault(up => up.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        user.Email = user.IdentityUser.Email;
        user.UserName = user.IdentityUser.UserName;
        return Ok(user);
    }

    [HttpPut("{id}")]
public IActionResult UpdateProfile([FromBody] UserProfileDTO profileDTO, int id)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    var userProfile = _dbContext.UserProfiles
        .Include(up => up.IdentityUser)
        .Include(up => up.RSVPs)
        .Include(up => up.HostedOccasions)
        .SingleOrDefault(up => up.Id == id);

    if (userProfile == null)
    {
        return NotFound();
    }

    // Update fields from the DTO
    userProfile.FirstName = profileDTO.FirstName;
    userProfile.LastName = profileDTO.LastName;
    userProfile.Bio = profileDTO.Bio;
    userProfile.Location = profileDTO.Location;
    userProfile.ProfileImage = profileDTO.ProfileImage;

    // Update fields in the IdentityUser
    if (userProfile.IdentityUser != null)
    {
        userProfile.IdentityUser.UserName = profileDTO.UserName;
        userProfile.IdentityUser.Email = profileDTO.Email;
    }

    // Save changes to the database
    _dbContext.SaveChanges();

    return Ok(userProfile);
}

}