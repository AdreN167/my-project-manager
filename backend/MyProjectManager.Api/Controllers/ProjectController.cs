using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProjectManager.Domain.Dto.Project;
using MyProjectManager.Domain.Interfaces.Services;
using MyProjectManager.Domain.Result;

namespace MyProjectManager.Api.Controllers;

//[Authorize]
[ApiController]
[ApiVersion("1.0")] // вот тут прописываем версию сваггера, в которой должн быть контроллер
[Route("api/v{version:apiVersion}/[controller]")]
//[Route("api/v1/project")]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet("userProjects/{userLogin}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CollectionResult<ProjectDto>>> GetUserProjects(string userLogin)
    {
        var response = await _projectService.GetProjectsAsync(userLogin);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ProjectDto>>> GetProject(long id)
    {
        var response = await _projectService.GetProjectByIdAsync(id);
        if (response.IsSuccess)
        {
            return  Ok(response);
        }
        return BadRequest(response);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ProjectDto>>> Delete(long id)
    {
        var response = await _projectService.DeleteProjectAsync(id);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ProjectDto>>> Create([FromBody]CreateProjectDto dto)
    {
        var response = await _projectService.CreateProjectAsync(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ProjectDto>>> Update([FromBody] UpdateProjectDto dto)
    {
        var response = await _projectService.UpdateProjectAsync(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
}
