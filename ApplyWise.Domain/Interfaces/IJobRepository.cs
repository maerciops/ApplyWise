using ApplyWise.Domain.Entities;

namespace ApplyWise.Domain.Interfaces;

public interface IJobRepository
{
    Task<JobApplication?> GetJobByIdAsync(Guid id);
    Task<IEnumerable<JobApplication>> GetAllJobsAsync();
    Task InsertJobAsync(JobApplication job);
    Task UpdateJobAsync(JobApplication job);
    Task DeleteJobAsync(Guid id);
}