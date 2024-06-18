using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
public IActionResult CreateOccasion([FromBody] Occasion newOccasionDto)
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

        return CreatedAtAction(nameof(GetOccasion), new { id = newOccasion.Id }, newOccasion);
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

[HttpPut("{id}")]

    public async Task<IActionResult> UpdateOccasion(int id, [FromBody] OccasionDTO occasion)
    {
        Occasion occasionToEdit = _dbContext.Occasions.FirstOrDefault(o => o.Id == id);
        if (occasionToEdit == null)
        {
            return NotFound();
        }

        occasionToEdit.Title = occasion.Title;
        occasionToEdit.Description = occasion.Description;
        occasionToEdit.CategoryId = occasion.CategoryId;
        occasionToEdit.City = occasion.City;
        occasionToEdit.State = occasion.State;
        occasionToEdit.Location = occasion.Location;
        occasionToEdit.Date = occasion.Date;
        occasionToEdit.HostUserProfileId = occasion.HostUserProfileId;

        // if (image != null && image.Length > 0)
        //     {
        //         var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "client", "public", "uploads");
        //         if (!Directory.Exists(uploadsFolder))
        //         {
        //             Directory.CreateDirectory(uploadsFolder);
        //         }
        //         var filePath = Path.Combine(uploadsFolder, image.FileName);
        //         using (var stream = new FileStream(filePath, FileMode.Create))
        //         {
        //             await image.CopyToAsync(stream);
        //         }

        //         post.HeaderImage = $"/uploads/{image.FileName}";
        //     }

            _dbContext.SaveChanges();

            return Ok(occasion);
    }



[HttpGet]
public IActionResult GetOccasions()
{
    var occasions = _dbContext.Occasions
        .Include(o => o.HostUserProfile)
        .Include(o => o.Category)
        .Select(o => new
        {
            Occasion = o,
            Category = new 
            {
                o.Category.Id,
                o.Category.Name
            },
            HostUserProfile = new 
            {
                o.HostUserProfile.Id,
                o.HostUserProfile.FirstName,
                o.HostUserProfile.LastName,
                o.HostUserProfile.Bio,
                RSVPs = o.HostUserProfile.RSVPs.Select(r => new 
                {
                    r.HostUserProfileId,
                    r.UserProfileId,
                    r.OccasionId
                }).ToList()
            }
        })
        .AsEnumerable() // Bring data into memory for client-side evaluation
        .Select(x => new OccasionDTO
        {
            Id = x.Occasion.Id,
            Title = x.Occasion.Title,
            OccasionImage = x.Occasion.OccasionImage,
            Description = x.Occasion.Description,
            CategoryId = x.Occasion.CategoryId,
            Category = new CategoryDTO
            {
                Id = x.Category.Id,
                Name = x.Category.Name
            },
            HostUserProfileId = x.HostUserProfile.Id,
            HostUserProfile = new UserProfileDTO
            {
                Id = x.HostUserProfile.Id,
                FirstName = x.HostUserProfile.FirstName,
                LastName = x.HostUserProfile.LastName,
                Bio = x.HostUserProfile.Bio,
                RSVPs = x.HostUserProfile.RSVPs.Select(r => new RSVPDTO
                {
                    HostUserProfileId = r.HostUserProfileId,
                    UserProfileId = r.UserProfileId,
                    OccasionId = r.OccasionId
                }).ToList()
            },
            State = x.Occasion.State,
            City = x.Occasion.City,
            Location = x.Occasion.Location,
            Date = x.Occasion.Date
        })
        .ToList();

    return Ok(occasions);
}


}