using MyProjectManager.Api.Controllers;
using MyProjectManager.Domain.Interfaces.Services;
using Xunit;
using Moq;
using MyProjectManager.Domain.Dto.Project;
using MyProjectManager.Domain.Result;
using Microsoft.AspNetCore.Mvc;
using MyProjectManager.Domain.Enum;

namespace MyProjectManager.Tests.Projects.Commands;

public class ProjectControllerTests
{
    [Fact]
    public async Task Create_OkObjectResult_WhenCreateProjectDtoIsValid()
    {
        // Arrange
        var name = "name";
        var description = "description";
        var color = "#DDDDDD";
        var userId = 1;
        var newProject = new CreateProjectDto(name, description, color, userId);

        var service = new Mock<IProjectService>();
        service.Setup(repo => repo.CreateProjectAsync(newProject)).ReturnsAsync(CreateFakeProject(newProject));

        var controller = new ProjectController(service.Object);

        // Act
        var result = await controller.Create(newProject);

        // Assert
        // проверка на возвращаемый тип метода
        var actionResult = Assert.IsType<ActionResult<BaseResult<ProjectDto>>>(result);
        // проверка на результат работы метода
        var model = Assert.IsAssignableFrom<OkObjectResult>(
            actionResult.Result);
        // проверка на равенство моделей
        Assert.Equal(model.Value, (result.Result as OkObjectResult).Value);

    }
    private BaseResult<ProjectDto> CreateFakeProject(CreateProjectDto project)
    {
        return new BaseResult<ProjectDto>
        {
            Data = new ProjectDto(1, project.Color, project.Name, project.Description, DateTime.UtcNow.ToString())
        };
    }

    [Fact]
    public async Task Create_BadRequestResult_WhenProjectIsAlreadyExists()
    {
        // Arrange
        var newProject = new CreateProjectDto("", "", "", 1);

        var service = new Mock<IProjectService>();
        service.Setup(repo => repo.CreateProjectAsync(newProject)).ReturnsAsync(CreateAlreadyExistanceProject(newProject));

        var controller = new ProjectController(service.Object);

        // Act
        var result = await controller.Create(newProject);

        // Assert
        // проверка на возвращаемый тип метода
        var actionResult = Assert.IsType<ActionResult<BaseResult<ProjectDto>>>(result);
        // проверка на результат работы метода
        var model = Assert.IsAssignableFrom<BadRequestObjectResult>(
            actionResult.Result);
        // проверка на равенство моделей
        Assert.Equal(model.Value, (result.Result as BadRequestObjectResult).Value);

    }
    private BaseResult<ProjectDto> CreateAlreadyExistanceProject(CreateProjectDto project)
    {
        return new BaseResult<ProjectDto>()
        {
            ErrorCode = (int)ErrorCodes.ProjectAlreadyExists,
            ErrorMessage = "Project already exists"
        };
    }

    [Fact]
    public async Task Delete_OkObjectResult_WhenProjectExists()
    {
        // Arrange
        var deleteProjectId = 1;

        var service = new Mock<IProjectService>();
        service.Setup(repo => repo.DeleteProjectAsync(deleteProjectId)).ReturnsAsync(DeleteExistanceProject(deleteProjectId));

        var controller = new ProjectController(service.Object);

        // Act
        var result = await controller.Delete(deleteProjectId);

        // Assert
        // проверка на возвращаемый тип метода
        var actionResult = Assert.IsType<ActionResult<BaseResult<ProjectDto>>>(result);
        // проверка на результат работы метода
        var model = Assert.IsAssignableFrom<OkObjectResult>(
            actionResult.Result);
        // проверка на равенство моделей
        Assert.Equal(model.Value, (result.Result as OkObjectResult).Value);
    }

    private BaseResult<ProjectDto> DeleteExistanceProject(long id)
    {
        var deletetProject = new ProjectDto(id, "#FFFFFF", "name", "descr", DateTime.UtcNow.ToString());
        return new BaseResult<ProjectDto>()
        {
            Data = deletetProject
        };
    }

    [Fact]
    public async Task Delete_BadRequestResult_WhenProjectNotExists()
    {
        // Arrange
        var deleteProjectId = 1;

        var service = new Mock<IProjectService>();
        service.Setup(repo => repo.DeleteProjectAsync(deleteProjectId)).ReturnsAsync(DeleteNotExistanceProject(deleteProjectId));

        var controller = new ProjectController(service.Object);

        // Act
        var result = await controller.Delete(deleteProjectId);

        // Assert
        // проверка на возвращаемый тип метода
        var actionResult = Assert.IsType<ActionResult<BaseResult<ProjectDto>>>(result);
        // проверка на результат работы метода
        var model = Assert.IsAssignableFrom<BadRequestObjectResult>(
            actionResult.Result);
        // проверка на равенство моделей
        Assert.Equal(model.Value, (result.Result as BadRequestObjectResult).Value);
    }

    private BaseResult<ProjectDto> DeleteNotExistanceProject(long id)
    {
        return new BaseResult<ProjectDto>()
        {
            ErrorCode = (int)ErrorCodes.ProjectNotFound,
            ErrorMessage = "Project not found"
        };
    }

    [Fact]
    public async Task Update_OkObjectResult_WhenProjectExists()
    {
        // Arrange
        var deleteProjectId = 1;

        var service = new Mock<IProjectService>();
        service.Setup(repo => repo.DeleteProjectAsync(deleteProjectId)).ReturnsAsync(UpdateExistanceProject(deleteProjectId));

        var controller = new ProjectController(service.Object);

        // Act
        var result = await controller.Delete(deleteProjectId);

        // Assert
        // проверка на возвращаемый тип метода
        var actionResult = Assert.IsType<ActionResult<BaseResult<ProjectDto>>>(result);
        // проверка на результат работы метода
        var model = Assert.IsAssignableFrom<OkObjectResult>(
            actionResult.Result);
        // проверка на равенство моделей
        Assert.Equal(model.Value, (result.Result as OkObjectResult).Value);
    }

    private BaseResult<ProjectDto> UpdateExistanceProject(long id)
    {
        var deletetProject = new ProjectDto(id, "#FFFFFF", "name", "descr", DateTime.UtcNow.ToString());
        return new BaseResult<ProjectDto>()
        {
            Data = deletetProject
        };
    }

    [Fact]
    public async Task Update_BadRequestResult_WhenProjectNotExists()
    {
        // Arrange
        var deleteProjectId = 1;

        var service = new Mock<IProjectService>();
        service.Setup(repo => repo.DeleteProjectAsync(deleteProjectId)).ReturnsAsync(UpdateNotExistanceProject(deleteProjectId));

        var controller = new ProjectController(service.Object);

        // Act
        var result = await controller.Delete(deleteProjectId);

        // Assert
        // проверка на возвращаемый тип метода
        var actionResult = Assert.IsType<ActionResult<BaseResult<ProjectDto>>>(result);
        // проверка на результат работы метода
        var model = Assert.IsAssignableFrom<BadRequestObjectResult>(
            actionResult.Result);
        // проверка на равенство моделей
        Assert.Equal(model.Value, (result.Result as BadRequestObjectResult).Value);
    }

    private BaseResult<ProjectDto> UpdateNotExistanceProject(long id)
    {
        return new BaseResult<ProjectDto>()
        {
            ErrorCode = (int)ErrorCodes.ProjectNotFound,
            ErrorMessage = "Project not found"
        };
    }


}

