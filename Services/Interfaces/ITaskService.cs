using checkBack.Models;

namespace checkBack.Services.Interfaces;

public interface ITaskService
{
    List<TaskItem> GetTasks(string username);
    TaskItem? ToggleTask(string username, string taskId);
    Dictionary<string, List<TaskItem>> GetAllUserTasks();
}
