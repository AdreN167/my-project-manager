namespace MyProjectManager.Domain.Dto.Project;

// вместо класса используем record
public record ProjectDto(long Id, string Name, string Color, string Description, string DateCreated);
