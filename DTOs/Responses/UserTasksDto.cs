namespace checkBack.DTOs.Responses;

public class UserTasksDto
{
    public string Username { get; set; } = string.Empty;
    public List<TaskDto> Tasks { get; set; } = [];
}
