using ApplyWise.Domain.Entities;
using ApplyWise.Domain.Interfaces;
using ApplyWise.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ApplyWise.Infrastructure.Repositories;

public class JobRepository : IJobRepository
{
    private readonly ApplicationDbContext _jobContext;

    public JobRepository(ApplicationDbContext context)
    {
        _jobContext = context;
    }

    public Task DeleteJobAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<JobApplication>> GetAllJobsAsync(Guid userId)
    {
        return await _jobContext
            .Jobs
            .Where(job => job.OwnerId == userId)
            .ToListAsync();
    }

    public Task<JobApplication?> GetJobByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task InsertJobAsync(JobApplication job)
    {
        await _jobContext.Jobs.AddAsync(job);
        await _jobContext.SaveChangesAsync();
    }

    public Task UpdateJobAsync(JobApplication job)
    {
        throw new NotImplementedException();
    }
}