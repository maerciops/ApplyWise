using ApplyWise.Application.DTOs;

namespace ApplyWise.Application.Interfaces;

public interface IJobService
{
    Task<Guid> CreateJobAsync(JobApplicationRequest request);
    Task<JobApplicationResponse?> GetJobByIdAsync(Guid id);
    Task<IEnumerable<JobApplicationResponse>> GetAllJobsAsync();
    Task<JobApplicationResponse?> UpdateJobAsync(Guid id, UpdateJobRequest request);
    Task<bool> DeleteJobAsync(Guid id);
}
