using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

// Interfaces
interface IProjectService
{
    void CreateProject(string projectName);
    List<string> GetProjects();
}

interface ITaskService
{
    void AddTask(string projectGuid, string taskName);
    List<string> GetTasks(string projectGuid);
}

// Implementations
class ProjectService : IProjectService
{
    private readonly Dictionary<string, string> projects = new Dictionary<string, string>();

    public void CreateProject(string projectName)
    {
        var projectGuid = Guid.NewGuid().ToString();
        projects[projectGuid] = projectName;
    }

    public List<string> GetProjects()
    {
        return projects.Values.ToList();
    }
}

class TaskService : ITaskService
{
    private readonly Dictionary<string, List<string>> tasks = new Dictionary<string, List<string>>();

    public void AddTask(string projectGuid, string taskName)
    {
        if (!tasks.ContainsKey(projectGuid))
        {
            tasks[projectGuid] = new List<string>();
        }
        tasks[projectGuid].Add(taskName);
    }

    public List<string> GetTasks(string projectGuid)
    {
        if (tasks.ContainsKey(projectGuid))
        {
            return tasks[projectGuid];
        }
        return new List<string>();
    }
}

class Program
{
    static void Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddTransient<IProjectService, ProjectService>()
            .AddScoped<ITaskService, TaskService>()
            .AddSingleton<IProjectService, ProjectService>()
            .AddSingleton<ITaskService, TaskService>()
            .BuildServiceProvider();

        // Transient ProjectService and TaskService
        var transientProjectService = serviceProvider.GetRequiredService<IProjectService>();
        var transientTaskService = serviceProvider.GetRequiredService<ITaskService>();

        transientProjectService.CreateProject("Project A");
        var transientProjectGuid = transientProjectService.GetProjects().FirstOrDefault();
        transientTaskService.AddTask(transientProjectGuid, "Task 1");

        Console.WriteLine("Transient Project Guid: " + transientProjectGuid);
        Console.WriteLine("Transient Tasks: " + string.Join(", ", transientTaskService.GetTasks(transientProjectGuid)));

        // Scoped ProjectService and TaskService
        using (var scope = serviceProvider.CreateScope())
        {
            var scopedProjectService = scope.ServiceProvider.GetRequiredService<IProjectService>();
            var scopedTaskService = scope.ServiceProvider.GetRequiredService<ITaskService>();

            scopedProjectService.CreateProject("Project B");
            var scopedProjectGuid = scopedProjectService.GetProjects().FirstOrDefault();
            scopedTaskService.AddTask(scopedProjectGuid, "Task 2");

            Console.WriteLine("\nScoped Project Guid: " + scopedProjectGuid);
            Console.WriteLine("Scoped Tasks: " + string.Join(", ", scopedTaskService.GetTasks(scopedProjectGuid)));
        }

        // Singleton ProjectService and TaskService
        var singletonProjectService = serviceProvider.GetRequiredService<IProjectService>();
        var singletonTaskService = serviceProvider.GetRequiredService<ITaskService>();

        singletonProjectService.CreateProject("Project C");
        var singletonProjectGuid = singletonProjectService.GetProjects().FirstOrDefault();
        singletonTaskService.AddTask(singletonProjectGuid, "Task 3");

        Console.WriteLine("\nSingleton Project Guid: " + singletonProjectGuid);
        Console.WriteLine("Singleton Tasks: " + string.Join(", ", singletonTaskService.GetTasks(singletonProjectGuid)));
    }
}
