using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProjectManager.Domain.Dto.ProjectTask;
using MyProjectManager.Domain.Interfaces.Services;
using MyProjectManager.Domain.Result;

namespace MyProjectManager.Api.Controllers;

//[Authorize]
[ApiController]
[ApiVersion("1.0")] // вот тут прописываем версию сваггера, в которой должн быть контроллер
[Route("api/v{version:apiVersion}/[controller]")]
public class ProjectTaskController : Controller
{
    private readonly IProjectTaskService _projectTaskService;

    public ProjectTaskController(IProjectTaskService projectTaskService)
    {
        _projectTaskService = projectTaskService;
    }

    [HttpGet("projectTasks/{projectId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ProjectTaskDto>>> GetUserProjects(long projectId)
    {
        var response = await _projectTaskService.GetProjectTasksAsync(projectId);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ProjectTaskDto>>> GetProject(long id)
    {
        var response = await _projectTaskService.GetProjectTaskByIdAsync(id);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ProjectTaskDto>>> Delete(long id)
    {
        var response = await _projectTaskService.DeleteProjectTaskAsync(id);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ProjectTaskDto>>> Create([FromBody] CreateProjectTaskDto dto)
    {
        var response = await _projectTaskService.CreateProjectTaskAsync(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ProjectTaskDto>>> Update([FromBody] UpdateProjectTaskDto dto)
    {
        var response = await _projectTaskService.UpdateProjectTaskAsync(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
}
