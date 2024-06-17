using Microsoft.AspNetCore.Mvc;
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

    
}