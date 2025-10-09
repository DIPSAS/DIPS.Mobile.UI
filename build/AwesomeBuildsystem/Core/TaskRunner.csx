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
    /// Runs a single task by name or alias
    /// </summary>
    /// <param name="taskNameOrAlias">Name or alias of the task to run</param>
    public static async Task RunAsync(string taskNameOrAlias)
    {
        var actualTaskName = ResolveTaskName(taskNameOrAlias);
        
        if (actualTaskName != null && tasks.TryGetValue(actualTaskName, out var task))
        {
            Console.WriteLine($"Running task: {actualTaskName}" + (actualTaskName != taskNameOrAlias ? $" (alias: {taskNameOrAlias})" : ""));
            
            if (task.BeforeAction != null) await task.BeforeAction();
            if (task.Action != null) await task.Action();
            if (task.AfterAction != null) await task.AfterAction();
            
            Console.WriteLine($"Completed task: {actualTaskName}");
        }
        else
        {
            Console.WriteLine($"Task '{taskNameOrAlias}' not found");
        }
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