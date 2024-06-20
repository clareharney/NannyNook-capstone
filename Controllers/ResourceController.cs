using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using NannyNook.DTOs;
using NannyNook.Models;
using NannyNook.Models.DTOs;
using NannyNookcapstone.Data;

namespace NannyNook.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ResourceController : ControllerBase
{
    private NannyNookDbContext _dbContext;

    public ResourceController(NannyNookDbContext context)
    {
        _dbContext = context;
    }

    [HttpGet]
        public IActionResult GetResources()
        {
            try
            {
                var resources = _dbContext.Resources
                    .Select(r => new ResourceDTO
                    {
                        Id = r.Id,
                        Title = r.Title,
                        Description = r.Description,
                        Type = r.Type,
                        Url = r.Url,
                        Author = r.Author,
                        PublicationDate = r.PublicationDate
                    })
                    .ToList();

                return Ok(resources);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetResourceById(int id)
        {
            try
            {
                var resource = _dbContext.Resources
                    .Where(r => r.Id == id)
                    .Select(r => new ResourceDTO
                    {
                        Id = r.Id,
                        Title = r.Title,
                        Description = r.Description,
                        Type = r.Type,
                        Url = r.Url,
                        Author = r.Author,
                        PublicationDate = r.PublicationDate
                    })
                    .FirstOrDefault();

                if (resource == null)
                {
                    return NotFound();
                }

                return Ok(resource);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


}