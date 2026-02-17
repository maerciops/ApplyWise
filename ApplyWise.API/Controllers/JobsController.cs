using ApplyWise.Application.DTOs;
using ApplyWise.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApplyWise.API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class JobsController : ControllerBase
{
    private readonly IJobService _jobService;

    public JobsController(IJobService jobService)
    {
        _jobService = jobService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(JobApplicationRequest request)
    {
        var id = await _jobService.CreateJobAsync(request);

        return Created($"/api/jobs/{id}", new { id });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var jobsList = await _jobService.GetAllJobsAsync();

        return Ok(jobsList);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateJobRequest request)
    {
        var job = await _jobService.UpdateJobAsync(id, request);

        if (job == null)
        {
            return NotFound();
        }

        return Ok(job);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var succes = await _jobService.DeleteJobAsync(id);

        if (succes)
        {
            return NoContent();
        } else
        {
            return NotFound();
        }
    }
}
