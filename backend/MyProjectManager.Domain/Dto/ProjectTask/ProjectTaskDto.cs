namespace MyProjectManager.Domain.Dto.ProjectTask;

public record ProjectTaskDto(long Id, string Deadline, string Description, bool IsDone, string DateCreated);
