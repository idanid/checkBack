using checkBack.Models;
using checkBack.Services.Interfaces;

namespace checkBack.Services;

public class TaskService : ITaskService
{
    // Singleton — keeps state across requests (in-memory, no DB)
    private readonly Dictionary<string, List<TaskItem>> _userTasks;

    public TaskService()
    {
        _userTasks = new Dictionary<string, List<TaskItem>>
        {
            ["admin"] =
            [
                new() { Id = "admin-1", Title = "Review pull requests",           Completed = true  },
                new() { Id = "admin-2", Title = "Update team documentation",      Completed = true  },
                new() { Id = "admin-3", Title = "Approve deployment to staging",  Completed = true  },
                new() { Id = "admin-4", Title = "Conduct weekly team standup",    Completed = false },
                new() { Id = "admin-5", Title = "Review security audit report",   Completed = false },
                new() { Id = "admin-6", Title = "Set quarterly OKRs",             Completed = false },
            ],
            ["alice"] =
            [
                new() { Id = "alice-1", Title = "Fix login page bug",             Completed = true  },
                new() { Id = "alice-2", Title = "Write unit tests for auth",      Completed = true  },
                new() { Id = "alice-3", Title = "Implement password reset flow",  Completed = false },
                new() { Id = "alice-4", Title = "Code review for PR #42",         Completed = false },
                new() { Id = "alice-5", Title = "Update API documentation",       Completed = false },
                new() { Id = "alice-6", Title = "Refactor database queries",      Completed = false },
            ],
            ["bob"] =
            [
                new() { Id = "bob-1", Title = "Deploy new feature to production", Completed = true  },
                new() { Id = "bob-2", Title = "Set up CI/CD pipeline",            Completed = true  },
                new() { Id = "bob-3", Title = "Configure monitoring alerts",      Completed = true  },
                new() { Id = "bob-4", Title = "Optimize database indexes",        Completed = true  },
                new() { Id = "bob-5", Title = "Write deployment runbook",         Completed = true  },
                new() { Id = "bob-6", Title = "Update SSL certificates",          Completed = false },
            ],
            ["charlie"] =
            [
                new() { Id = "charlie-1", Title = "Learn React basics",                   Completed = false },
                new() { Id = "charlie-2", Title = "Set up development environment",       Completed = false },
                new() { Id = "charlie-3", Title = "Read project documentation",           Completed = false },
                new() { Id = "charlie-4", Title = "Complete onboarding tasks",            Completed = false },
                new() { Id = "charlie-5", Title = "Attend architecture overview meeting", Completed = false },
                new() { Id = "charlie-6", Title = "Submit first PR for review",           Completed = false },
            ],
        };
    }

    public List<TaskItem> GetTasks(string username)
    {
        var key = username.ToLowerInvariant();
        return _userTasks.TryGetValue(key, out var tasks) ? tasks : [];
    }

    public TaskItem? ToggleTask(string username, string taskId)
    {
        var key = username.ToLowerInvariant();
        if (!_userTasks.TryGetValue(key, out var tasks)) return null;

        var task = tasks.FirstOrDefault(t => t.Id == taskId);
        if (task is null) return null;

        task.Completed = !task.Completed;
        return task;
    }

    public Dictionary<string, List<TaskItem>> GetAllUserTasks() => _userTasks;
}
