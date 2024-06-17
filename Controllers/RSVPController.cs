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
    
}