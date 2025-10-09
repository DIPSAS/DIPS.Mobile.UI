#load "TaskRunner.csx"

/// <summary>
/// Provides build completion and summary functionality
/// </summary>
public static class BuildWindow 
{
    /// <summary>
    /// Runs the build completion routine and executes the specified tasks
    /// </summary>
    /// <param name="args">Build arguments - task names to execute</param>
    /// <param name="projectName">Name of the project being built</param>
    public static async Task RunAsync(string[] args, string projectName)
    {
        var startTime = DateTime.Now;
        
        Console.WriteLine($"🚀 Starting build for {projectName}...");
        
        if (args?.Length > 0)
        {
            Console.WriteLine($"Build arguments: {string.Join(", ", args)}");
            
            // Run each task specified in arguments
            foreach (var taskName in args)
            {
                if (!string.IsNullOrWhiteSpace(taskName))
                {
                    Console.WriteLine($"⚡ Running task: {taskName}");
                    await TaskRunner.RunAsync(taskName);
                }
            }
        }
        else
        {
            Console.WriteLine("No specific tasks specified - build completed");
        }
        
        var duration = DateTime.Now - startTime;
        Console.WriteLine($"🎉 Build completed successfully for {projectName}!");
        Console.WriteLine($"⏱️ Total duration: {duration:mm\\:ss}");
        Console.WriteLine($"📅 Build finished at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
    }
    
    /// <summary>
    /// Displays a build summary with task information
    /// </summary>
    /// <param name="projectName">Name of the project</param>
    /// <param name="taskCount">Number of tasks executed</param>
    /// <param name="duration">Build duration</param>
    public static async Task ShowSummaryAsync(string projectName, int taskCount, TimeSpan duration)
    {
        Console.WriteLine();
        Console.WriteLine("=".PadRight(50, '='));
        Console.WriteLine($"  BUILD SUMMARY - {projectName}");
        Console.WriteLine("=".PadRight(50, '='));
        Console.WriteLine($"Tasks executed: {taskCount}");
        Console.WriteLine($"Total duration: {duration:mm\\:ss}");
        Console.WriteLine($"Completed at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        Console.WriteLine("=".PadRight(50, '='));
        
        await Task.CompletedTask;
    }
}