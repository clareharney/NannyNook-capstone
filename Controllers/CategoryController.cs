using Microsoft.AspNetCore.Mvc;
using NannyNookcapstone.Data;
using NannyNook.Models.DTOs;
using NannyNook.Models;



namespace NannyNookcapstone.Controllers;

[ApiController]
[Route("api/[controller]")]

public class CategoryController : ControllerBase
{
    private NannyNookDbContext _dbContext;

    public CategoryController(NannyNookDbContext context)
    {
        _dbContext = context;
    }

    [HttpGet]
    public IActionResult GetCategories()
    {
        var categories = _dbContext.Categories.ToList();

        var categoryDTOs = categories.Select(c => new CategoryDTO
        {
            Id = c.Id,
            Name = c.Name
        })
        .ToList();

        return Ok(categoryDTOs);
    }
}
