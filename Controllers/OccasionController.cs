using Microsoft.AspNetCore.Mvc;
using NannyNook.Models;
using NannyNook.Models.DTOs;
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
public IActionResult CreateOccasion(Occasion newOccasionDto)
{
    try
    {

        var newOccasion = new Occasion
        {
            Title = newOccasionDto.Title,
            Description = newOccasionDto.Description,
            City = newOccasionDto.City,
            State = newOccasionDto.State,
            Location = newOccasionDto.Location,
            Date = newOccasionDto.Date,
            CategoryId = newOccasionDto.CategoryId,
            OccasionImage = newOccasionDto.OccasionImage,
            HostUserProfileId = newOccasionDto.HostUserProfileId
        };

        _dbContext.Occasions.Add(newOccasion);
        _dbContext.SaveChanges();

        return Ok(newOccasion);
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Internal server error: {ex.Message}");
    }
}


    [HttpGet("{id}")]
        public IActionResult GetOccasion(int id)
        {
            var occasion = _dbContext.Occasions
                .FirstOrDefault(o => o.Id == id);

            if (occasion == null)
            {
                return NotFound();
            }

            return Ok(occasion);
        }


}