/// <summary>
/// Represents a task definition with dependencies and actions
/// </summary>
public class TaskDefinition 
{
    public string Name { get; set; }
    public string DescriptionText { get; set; }
    public string AliasText { get; set; }
    public Func<Task> BeforeAction { get; set; }
    public Func<Task> Action { get; set; }
    public Func<Task> AfterAction { get; set; }
    public List<string> Dependencies { get; set; } = new List<string>();
    
    /// <summary>
    /// Sets the main task action (async version)
    /// </summary>
    public TaskDefinition AsyncTask(Func<Task> action) { Action = action; return this; }
    
    /// <summary>
    /// Sets the main task action (async version)
    /// </summary>
    public TaskDefinition Does(Func<Task> action) { Action = action; return this; }
    
    /// <summary>
    /// Sets the main task action (sync version)
    /// </summary>
    public TaskDefinition Does(Action action) { Action = () => { action(); return Task.CompletedTask; }; return this; }
    
    /// <summary>
    /// Sets the task alias
    /// </summary>
    public TaskDefinition Alias(string alias) { AliasText = alias; return this; }
    
    /// <summary>
    /// Sets the task description
    /// </summary>
    public TaskDefinition Description(string description) { DescriptionText = description; return this; }
    
    /// <summary>
    /// Sets the action to run before the main task
    /// </summary>
    public TaskDefinition DoesBefore(Func<Task> action) { BeforeAction = action; return this; }
    
    /// <summary>
    /// Sets the action to run after the main task
    /// </summary>
    public TaskDefinition DoesAfter(Func<Task> action) { AfterAction = action; return this; }
    
    /// <summary>
    /// Adds a dependency on another task
    /// </summary>
    public TaskDefinition IsDependentOn(string taskName) { Dependencies.Add(taskName); return this; }
}

/// <summary>
/// Simple task runner for build automation
/// </summary>
public static class TaskRunner 
{
    private static Dictionary<string, TaskDefinition> tasks = new Dictionary<string, TaskDefinition>();
    private static System.Collections.Concurrent.ConcurrentDictionary<string, Task> _executedTasks = new System.Collections.Concurrent.ConcurrentDictionary<string, Task>();
    
    /// <summary>
    /// Creates a new async task definition
    /// </summary>
    /// <param name="name">Task name</param>
    /// <returns>Task definition for chaining</returns>
    public static TaskDefinition AsyncTask(string name)
    {
        var task = new TaskDefinition { Name = name };
        tasks[name] = task;
        return task;
    }
    
    /// <summary>
    /// Resolves a task name or alias to the actual task name
    /// </summary>
    /// <param name="taskNameOrAlias">Task name or alias</param>
    /// <returns>The actual task name, or null if not found</returns>
    private static string ResolveTaskName(string taskNameOrAlias)
    {
        // First try direct task name lookup
        if (tasks.ContainsKey(taskNameOrAlias))
        {
            return taskNameOrAlias;
        }
        
        // Then try alias lookup
        var taskByAlias = tasks.Values.FirstOrDefault(t => t.AliasText == taskNameOrAlias);
        return taskByAlias?.Name;
    }
    
    /// <summary>
    /// Runs a single task by name or alias.
    /// Dependencies declared with IsDependentOn are resolved and run first.
    /// Each task is guaranteed to run at most once per build session.
    /// </summary>
    /// <param name="taskNameOrAlias">Name or alias of the task to run</param>
    public static Task RunAsync(string taskNameOrAlias)
    {
        var actualTaskName = ResolveTaskName(taskNameOrAlias);
        
        if (actualTaskName == null || !tasks.TryGetValue(actualTaskName, out var task))
        {
            Console.WriteLine($"Task '{taskNameOrAlias}' not found");
            return Task.CompletedTask;
        }

        // GetOrAdd ensures the task body executes exactly once even when called concurrently.
        return _executedTasks.GetOrAdd(actualTaskName, _ => ExecuteTaskAsync(task, actualTaskName, taskNameOrAlias));
    }

    private static async Task ExecuteTaskAsync(TaskDefinition task, string actualTaskName, string taskNameOrAlias)
    {
        // Run all dependencies first, in parallel when there are multiple.
        if (task.Dependencies.Count == 1)
        {
            await RunAsync(task.Dependencies[0]);
        }
        else if (task.Dependencies.Count > 1)
        {
            await Task.WhenAll(task.Dependencies.Select(dep => RunAsync(dep)));
        }

        Console.WriteLine($"Running task: {actualTaskName}" + (actualTaskName != taskNameOrAlias ? $" (alias: {taskNameOrAlias})" : ""));
        
        if (task.BeforeAction != null) await task.BeforeAction();
        if (task.Action != null) await task.Action();
        if (task.AfterAction != null) await task.AfterAction();
        
        Console.WriteLine($"Completed task: {actualTaskName}");
    }
    
    /// <summary>
    /// Runs multiple tasks in sequence
    /// </summary>
    /// <param name="taskNames">Array of task names to run</param>
    /// <param name="continueOnError">Whether to continue if a task fails</param>
    public static async Task RunAsync(string[] taskNames, bool continueOnError)
    {
        foreach (var taskName in taskNames)
        {
            try
            {
                await RunAsync(taskName);
            }
            catch (Exception ex) when (continueOnError)
            {
                Console.WriteLine($"Task '{taskName}' failed: {ex.Message}");
            }
        }
    }
    
    /// <summary>
    /// Gets all registered tasks
    /// </summary>
    /// <returns>Dictionary of all tasks</returns>
    public static IReadOnlyDictionary<string, TaskDefinition> GetTasks() => tasks;
}