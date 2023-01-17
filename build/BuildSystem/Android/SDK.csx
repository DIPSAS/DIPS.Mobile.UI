public static class SDK
{
    //Insipired by: https://github.com/microsoft/azure-pipelines-tasks/blob/014f77fb92e26512ea8aecef69535f0ba76fa005/Tasks/AndroidSigningV3/androidsigning.ts#L5
    public static string FindAndroidBuildTool(string toolName){

        var androidSdkFolder = Environment.GetEnvironmentVariable("ANDROID_HOME");
        if(string.IsNullOrEmpty(androidSdkFolder))
        {
            var home = Environment.GetEnvironmentVariable("HOME");
            androidSdkFolder = $"{home}/Library/Developer/Xamarin/android-sdk-macosx/";
        }

        if(string.IsNullOrEmpty(androidSdkFolder))
        {
            throw new Exception("Not able to detect Android SDK location in either $Android_HOME environment path or the Xamarin developer path.");
        }

        if(Directory.Exists(androidSdkFolder+"/build-tools"))
        {
            //Get the highest build-tools version folder to search for the android build-tool
            var directoryPaths = Directory.GetDirectories(androidSdkFolder+"/build-tools");
            var directories = new List<DirectoryInfo>();
            for (int i = 0; i < directoryPaths.Length; i++)
            {
                var directory = Directory.CreateDirectory(directoryPaths[i]);
                directories.Add(directory);
            }

            var maxVersion = directories.Select<DirectoryInfo, Version>(d => {
                if(Version.TryParse(d.Name, out var version)){
                    return version;
                }
                return null;
            }).Max();

            var latestBuildToolsVersionDirectory = directories.FirstOrDefault(d => d.Name == maxVersion.ToString());
            
            var toolPath = latestBuildToolsVersionDirectory.GetFiles().FirstOrDefault(f => f.Name == toolName);

            if(toolPath == null){
                throw new Exception($"Unable to find tool: {toolName} in {latestBuildToolsVersionDirectory.FullName}");
            }
            return toolPath.FullName;
        }

        throw new Exception($"Not able to locate build-tools folder in :{androidSdkFolder}");    
    }
}