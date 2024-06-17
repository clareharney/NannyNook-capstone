using Microsoft.AspNetCore.Mvc;
using NannyNook.Models;
using NannyNookcapstone.Data;

namespace NannyNook.Controllers;


[ApiController]
[Route("api/[controller]")]
public class OccasionController : ControllerBase
{
    private NannyNookDbContext _dbContext;

    public OccasionController(NannyNookDbContext context)
    {
        _dbContext = context;
    }

    [HttpPost]

    public IActionResult CreateOccasion([FromBody] Occasion occasion)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!_dbContext.Categories.Any(c => c.Id == occasion.CategoryId))
        {
            return BadRequest("Invalid CategoryId. Category does not exist.");
        }

        if (!_dbContext.UserProfiles.Any(u => u.Id == occasion.HostUserProfileId))
        {
            return BadRequest("Invalid UserProfileId. User profile does not exist.");
        }

        _dbContext.Occasions.Add(occasion);
        _dbContext.SaveChanges();

        return Ok();
    }

}