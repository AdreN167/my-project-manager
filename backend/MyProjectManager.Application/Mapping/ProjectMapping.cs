using AutoMapper;
using MyProjectManager.Domain.Dto.Project;
using MyProjectManager.Domain.Entity;

namespace MyProjectManager.Application.Mapping;

public class ProjectMapping : Profile
{
    public ProjectMapping()
    {
        CreateMap<Project, ProjectDto>()
            .ForCtorParam(ctorParamName: "Id", m => m.MapFrom(s => s.Id))
            .ForCtorParam(ctorParamName: "Name", m => m.MapFrom(s => s.Name))
            .ForCtorParam(ctorParamName: "Description", m => m.MapFrom(s => s.Description))
            .ForCtorParam(ctorParamName: "DateCreated", m => m.MapFrom(s => s.CreatedAt))
            .ForCtorParam(ctorParamName: "Color", m => m.MapFrom(s => s.Color))
            .ReverseMap();
    }
}
