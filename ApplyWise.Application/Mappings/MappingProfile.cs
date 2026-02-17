using ApplyWise.Application.DTOs;
using ApplyWise.Domain.Entities;
using AutoMapper;

namespace ApplyWise.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<JobApplication, JobApplicationResponse>().ReverseMap();
        CreateMap<JobApplication, JobApplicationRequest>().ReverseMap();
        CreateMap<JobApplication, UpdateJobRequest>().ReverseMap();
    }
}
