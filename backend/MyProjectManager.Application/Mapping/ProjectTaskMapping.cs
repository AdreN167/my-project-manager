using AutoMapper;
using MyProjectManager.Domain.Dto.ProjectTask;
using MyProjectManager.Domain.Entity;

namespace MyProjectManager.Application.Mapping;

public class ProjectTaskMapping : Profile
{
    public ProjectTaskMapping()
    {
        CreateMap<ProjectTask, ProjectTaskDto>()
            .ForCtorParam(ctorParamName: "Id", m => m.MapFrom(s => s.Id))
            .ForCtorParam(ctorParamName: "Deadline", m => m.MapFrom(s => s.Deadline))
            .ForCtorParam(ctorParamName: "Description", m => m.MapFrom(s => s.Description))
            .ForCtorParam(ctorParamName: "DateCreated", m => m.MapFrom(s => s.CreatedAt))
            .ForCtorParam(ctorParamName: "IsDone", m => m.MapFrom(s => s.IsDone))
            .ReverseMap();
    }
}
