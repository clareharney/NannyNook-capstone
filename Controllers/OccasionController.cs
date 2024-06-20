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

        return CreatedAtAction(nameof(GetOccasionById), new { id = newOccasion.Id }, newOccasion);
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Internal server error: {ex.Message}");
    }
}

[HttpGet("{id}")]
public IActionResult GetOccasionById(int id)
{
    var occasion = _dbContext.Occasions
        .Include(o => o.HostUserProfile)
        .Include(o => o.Category)
        .Include(o => o.RSVPs)
        .Select(o => new OccasionDTO
        {
            Id = o.Id,
            Title = o.Title,
            Description = o.Description,
            State = o.State,
            City = o.City,
            Location = o.Location,
            CategoryId = o.CategoryId,
            Date = o.Date,
            OccasionImage = o.OccasionImage,
            HostUserProfileId = o.HostUserProfileId,
            HostUserProfile = new UserProfileDTO
            {
                Id = o.HostUserProfile.Id,
                FirstName = o.HostUserProfile.FirstName,
                LastName = o.HostUserProfile.LastName
            },
            RSVPs = o.RSVPs.Select(r => new RSVPDTO 
            {
                UserProfileId = r.UserProfileId,
                OccasionId = r.OccasionId
            }).ToList()
        })
        .SingleOrDefault(o => o.Id == id);

    if (occasion == null)
    {
        return NotFound();
    }

    return Ok(occasion);
}


    // [HttpGet("{id}")]

    // public IActionResult GetOccasionById(int id)
    // {
    //     var occasions = _dbContext.Occasions
    //     .Include(o => o.HostUserProfile)
    //     .Include(o => o.Category)
    //     .Include(o => o.RSVPs)
    //     .Select(o => new OccasionDTO
    //     {
    //         Id = o.Id,
    //         Title = o.Title,
    //         Description = o.Description,
    //         State = o.State,
    //         City = o.City,
    //         Location = o.Location,
    //         CategoryId = o.CategoryId,
    //         Date = o.Date,
    //         OccasionImage = o.OccasionImage,
    //         HostUserProfileId = o.HostUserProfileId,
    //         RSVPs = o.RSVPs.Select(r => new RSVPDTO 
    //         {
    //             UserProfileId = r.UserProfileId,
    //             OccasionId = r.OccasionId
    //         }).ToList()
    //     }
    //     ).SingleOrDefault(o => o.Id == id);

    //     return Ok(occasions);
    // }





[HttpPut("{id}")]
public IActionResult UpdateOccasion([FromBody] OccasionDTO occasionDto, int id)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    var occasionToEdit = _dbContext.Occasions.FirstOrDefault(o => o.Id == id);
    if (occasionToEdit == null)
    {
        return NotFound();
    }

    occasionToEdit.Title = occasionDto.Title;
    occasionToEdit.Description = occasionDto.Description;
    occasionToEdit.CategoryId = occasionDto.CategoryId;
    occasionToEdit.City = occasionDto.City;
    occasionToEdit.State = occasionDto.State;
    occasionToEdit.Location = occasionDto.Location;
    occasionToEdit.Date = occasionDto.Date;
    occasionToEdit.HostUserProfileId = occasionDto.HostUserProfileId;
    occasionToEdit.OccasionImage = occasionDto.OccasionImage;

     _dbContext.SaveChanges();

    return Ok(occasionToEdit);
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

[HttpGet("{id}/user")]
public IActionResult GetOccasionsByUserId(int id)
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
                    UserProfileId = r.UserProfileId,
                    OccasionId = r.OccasionId
                }).ToList()
            },
            State = x.Occasion.State,
            City = x.Occasion.City,
            Location = x.Occasion.Location,
            Date = x.Occasion.Date
        })
        .Where(o => o.HostUserProfileId != id)
        .ToList();

    return Ok(occasions);
}


[HttpDelete("{id}")]
    //[Authorize]
    public IActionResult DeleteOccasion(int id)
    {
        try
        {
            var post = _dbContext.Occasions.Find(id);
            if (post == null)
            {
                return NotFound();
            }

            _dbContext.Occasions.Remove(post);
            _dbContext.SaveChanges();

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

}