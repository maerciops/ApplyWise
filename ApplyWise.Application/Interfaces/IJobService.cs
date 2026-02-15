using ApplyWise.Application.DTOs;

namespace ApplyWise.Application.Interfaces;

public interface IJobService
{
    Task<Guid> CreateJobAsync(JobApplicationRequest request);
    Task<IEnumerable<JobApplicationResponse>> GetAllJobsAsync();
}
