#load "../Command.csx"


public static class dotnet
{
    public static Task Restore(string path) => Command.ExecuteAsync("dotnet", $"restore {path}");
    
    public static Task Build(string projectPath) => Command.ExecuteAsync("dotnet", $"build {projectPath}");

    public static Task Pack(string projectPath, string version, string outputdir) => Command.ExecuteAsync("dotnet", $"pack {projectPath} -p:PackageVersion={version} -o {outputdir}");

    public static Task Test(string projectPath, string testResultsDir = "")
    {
        var args = "";
        if(!string.IsNullOrEmpty(testResultsDir))
        {
            args += $"-l:trx;LogFileName={testResultsDir}/testresults.xml";
        }
        return Command.ExecuteAsync("dotnet", $"test {projectPath} {args}");
    } 

    public static Task NugetPush(string nupkgPath, string apiKey, string source, bool skipDuplicate=true) 
    {
        var args = $"{nupkgPath} -k {apiKey} -s {source}";
        if(skipDuplicate)
        {
            args += " --skip-duplicate";
        }
        
        return Command.ExecuteAsync("dotnet", $"nuget push {args}");
    }

}