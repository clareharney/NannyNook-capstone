using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NannyNook.DTOs;
using NannyNook.Models;
using NannyNook.Models.DTOs;
using NannyNookcapstone.Data;

namespace NannyNook.Controllers;


[ApiController]
[Route("api/[controller]")]
public class JobController : ControllerBase
{
    private NannyNookDbContext _dbContext;

    public JobController(NannyNookDbContext context)
    {
        _dbContext = context;
    }

    [HttpGet]
        public IActionResult GetJobs()
        {
            try
            {
                var jobs = _dbContext.Jobs
                    .Select(j => new JobDTO
                    {
                        Id = j.Id,
                        Title = j.Title,
                        Description = j.Description,
                        PayRateMin = j.PayRateMin,
                        PayRateMax = j.PayRateMax,
                        NumberOfKids = j.NumberOfKids,
                        FullTime = j.FullTime,
                        ContactInformation = j.ContactInformation,
                        PosterId = j.PosterId

                    })
                    .ToList();

                return Ok(jobs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetJobById(int id)
        {
                var job = _dbContext.Jobs
                .Select(job => new JobDTO
                {
                    Id = job.Id,
                    Title = job.Title,
                    Description = job.Description,
                    PayRateMin = job.PayRateMin,
                    PayRateMax = job.PayRateMax,
                    NumberOfKids = job.NumberOfKids,
                    FullTime = job.FullTime,
                    ContactInformation = job.ContactInformation,
                    PosterId = job.PosterId,
                    Poster = new UserProfileDTO
                    {
                        Id = job.Poster.Id,
                        FirstName = job.Poster.FirstName,
                        LastName = job.Poster.LastName,
                        Bio = job.Poster.Bio
                    }
                })
                .SingleOrDefault(job => job.Id == id);

            if (job == null)
            {
                return NotFound();
            }


                return Ok(job);
        }

        [HttpGet("{id}/user")]
        public IActionResult GetJobsByPosterId(int id)
        {
            try
            {
                var jobs = _dbContext.Jobs
                    .Include(j => j.Poster)
                    .Select(j => new
                    {
                        Job = j,
                        Poster = new
                        {
                            j.Poster.Id,
                            j.Poster.FirstName,
                            j.Poster.LastName,
                            j.Poster.Bio,
                        }
                    })
                    .AsEnumerable() // Bring data into memory for client-side evaluation
                    .Select(x => new JobDTO
                    {
                        Id = x.Job.Id,
                        Title = x.Job.Title,
                        Description = x.Job.Description,
                        PayRateMin = x.Job.PayRateMin,
                        PayRateMax = x.Job.PayRateMax,
                        NumberOfKids = x.Job.NumberOfKids,
                        FullTime = x.Job.FullTime,
                        ContactInformation = x.Job.ContactInformation,
                        PosterId = x.Job.PosterId,
                        Poster = new UserProfileDTO
                        {
                            Id = x.Poster.Id,
                            FirstName = x.Poster.FirstName,
                            LastName = x.Poster.LastName,
                            Bio = x.Poster.Bio,
                        }
                    });

                return Ok(jobs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult CreateJob([FromBody] JobDTO jobDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var job = new Job
                {
                    Title = jobDTO.Title,
                    Description = jobDTO.Description,
                    PayRateMin = jobDTO.PayRateMin,
                    PayRateMax = jobDTO.PayRateMax,
                    NumberOfKids = jobDTO.NumberOfKids,
                    FullTime = jobDTO.FullTime,
                    ContactInformation = jobDTO.ContactInformation,
                    PosterId = jobDTO.PosterId
                };

                _dbContext.Jobs.Add(job);
                _dbContext.SaveChanges();

                jobDTO.Id = job.Id; // Update jobDTO with the generated ID

                return CreatedAtAction(nameof(GetJobById), new { id = job.Id }, jobDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateJob(int id, [FromBody] JobDTO jobDTO)
        {
            try
            {
                if (id != jobDTO.Id)
                {
                    return BadRequest("Job ID mismatch");
                }

                var existingJob = _dbContext.Jobs.Find(id);

                if (existingJob == null)
                {
                    return NotFound();
                }

                // Update fields based on DTO
                existingJob.Title = jobDTO.Title;
                existingJob.Description = jobDTO.Description;
                existingJob.PayRateMin = jobDTO.PayRateMin;
                existingJob.PayRateMax = jobDTO.PayRateMax;
                existingJob.NumberOfKids = jobDTO.NumberOfKids;
                existingJob.FullTime = jobDTO.FullTime;
                existingJob.ContactInformation = jobDTO.ContactInformation;
                existingJob.PosterId = jobDTO.PosterId;

                _dbContext.Jobs.Update(existingJob);
                _dbContext.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteJob(int id)
        {
            try
            {
                var job = _dbContext.Jobs.Find(id);

                if (job == null)
                {
                    return NotFound();
                }

                _dbContext.Jobs.Remove(job);
                _dbContext.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
}