/// <summary>
/// Based on this https://learn.microsoft.com/en-us/azure/devops/pipelines/scripts/logging-commands?view=azure-devops&tabs=bash
/// </summary>
public static class Logger
{
    public static void LogDebug(string message)
    {
        string prefix = "##[debug]";
        Console.WriteLine($"{prefix}{message}");
    }

    public static void LogError(string message, bool shouldExit)
    {
        string prefix = "##vso[task.logissue type=error]";
        Console.WriteLine($"{prefix}❌ {message}");
        
        if(shouldExit)
        {
            throw new Exception(message);
        }
    }

    // TODO: Could maybe use ##vso[task.logissue type=warning]? Same as in LogError
    // From here: https://learn.microsoft.com/en-us/azure/devops/pipelines/scripts/logging-commands?view=azure-devops&tabs=bash
    public static void LogWarning(string message)
    {
        string prefix = "##[warning]";
        Console.WriteLine($"{prefix}⚠️ {message}");
    }

    public static void LogSuccess(string message)
    {
        string prefix = "##[section]";
        Console.WriteLine($"{prefix}✅ {message}");
    }

}