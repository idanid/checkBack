using checkBack.Common;
using checkBack.DTOs.Responses;
using checkBack.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace checkBack.Controllers;

[ApiController]
[Route("api/tasks")]
[Authorize]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public IActionResult GetTasks()
    {
        var username = User.Identity?.Name;
        if (string.IsNullOrEmpty(username))
            return Unauthorized(ApiResponse<object>.Fail("Invalid token"));

        var tasks = _taskService.GetTasks(username);
        var dtos = tasks.Select(t => new TaskDto
        {
            Id = t.Id,
            Title = t.Title,
            Completed = t.Completed
        }).ToList();

        return Ok(ApiResponse<List<TaskDto>>.Ok(dtos));
    }

    [HttpGet("admin")]
    [Authorize(Roles = "admin")]
    public IActionResult GetAllUserTasks()
    {
        var allTasks = _taskService.GetAllUserTasks();

        var result = allTasks.Select(kvp => new UserTasksDto
        {
            Username = kvp.Key,
            Tasks = kvp.Value.Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Completed = t.Completed
            }).ToList()
        }).ToList();

        return Ok(ApiResponse<List<UserTasksDto>>.Ok(result));
    }

    [HttpPatch("{id}")]
    public IActionResult ToggleTask(string id)
    {
        var username = User.Identity?.Name;
        if (string.IsNullOrEmpty(username))
            return Unauthorized(ApiResponse<object>.Fail("Invalid token"));

        var task = _taskService.ToggleTask(username, id);
        if (task is null)
            return NotFound(ApiResponse<object>.Fail("Task not found"));

        return Ok(ApiResponse<TaskDto>.Ok(new TaskDto
        {
            Id = task.Id,
            Title = task.Title,
            Completed = task.Completed
        }));
    }
}
