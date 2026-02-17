using ApplyWise.Application.DTOs;
using ApplyWise.Application.Interfaces;
using ApplyWise.Domain.Entities;
using ApplyWise.Domain.Interfaces;
using AutoMapper;

namespace ApplyWise.Application.Services;

public class JobService : IJobService
{
    private readonly IJobRepository _jobRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public JobService(IJobRepository jobRepository, ICurrentUserService currentUserService, IMapper mapper)
    {
        _jobRepository = jobRepository;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<Guid> CreateJobAsync(JobApplicationRequest request)
    {
        var jobApplication = new JobApplication()
        {
            Company = request.Company,
            JobTitle = request.JobTitle,
            Description = request.Description,
            URL = request.URL,
            ResumeURL = request.ResumeURL,
            SalaryRange = request.SalaryRange,

            OwnerId = _currentUserService.UserId
        };

        await _jobRepository.InsertJobAsync(jobApplication);

        return jobApplication.Id;
    }

    public async Task<bool> DeleteJobAsync(Guid id)
    {
        var job = await _jobRepository.GetJobByIdAsync(id);

        if (job == null) return false;

        await _jobRepository.DeleteJobAsync(job);
        
        return true;
    }

    public async Task<IEnumerable<JobApplicationResponse>> GetAllJobsAsync()
    {
        var jobs = await _jobRepository.GetAllJobsAsync(_currentUserService.UserId);

        return _mapper.Map<IEnumerable<JobApplicationResponse>>(jobs);
    }

    public async Task<JobApplicationResponse?> GetJobByIdAsync(Guid id)
    {
        var job = await _jobRepository.GetJobByIdAsync(id);

        if (job == null) return null;

        return _mapper.Map<JobApplicationResponse>(job);
    }

    public async Task<JobApplicationResponse?> UpdateJobAsync(Guid id, UpdateJobRequest request)
    {
        var job = await _jobRepository.GetJobByIdAsync(id);

        if (job == null) return null;

        _mapper.Map(request, job);

        await _jobRepository.UpdateJobAsync(job);

        return _mapper.Map<JobApplicationResponse>(job);
    }

}