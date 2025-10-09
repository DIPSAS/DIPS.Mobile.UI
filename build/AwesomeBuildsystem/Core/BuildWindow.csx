/// <summary>
/// Provides build completion and summary functionality
/// </summary>
public static class BuildWindow 
{
    /// <summary>
    /// Runs the build completion routine
    /// </summary>
    /// <param name="args">Build arguments</param>
    /// <param name="projectName">Name of the project being built</param>
    public static async Task RunAsync(string[] args, string projectName)
    {
        Console.WriteLine($"🎉 Build completed successfully for {projectName}!");
        
        if (args?.Length > 0)
        {
            Console.WriteLine($"Build arguments: {string.Join(", ", args)}");
        }
        
        Console.WriteLine($"Build finished at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        
        await Task.CompletedTask;
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