namespace MyProjectManager.Domain.Dto.ProjectTask;

public record UpdateProjectTaskDto(long Id, string Deadline, string Description, bool IsDone);
