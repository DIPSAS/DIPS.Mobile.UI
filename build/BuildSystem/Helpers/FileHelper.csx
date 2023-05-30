
/// <summary>
/// A file helper class
/// </summary>
public static class FileHelper{

    /// <summary>
    /// Finds a single file that ends with a file extension
    /// </summary>
    /// <param name="path">The path to look for the file</param>
    /// <param name="fileExtension">The file extension that the file ends with</param>
    /// <returns>A fileinfo object of the file</returns>
    /// <exception cref="Exception">When theres 0 or more than 1 file ending with the file extensions in the path</exception>
    public static FileInfo FindSingleFileByExtension(string path, string fileExtension){
        var allFilesAfterBuild = new DirectoryInfo(path).GetFiles();
        var allFilesWithFileExtension = allFilesAfterBuild.Where(f => f.Extension == fileExtension);
        if(allFilesWithFileExtension.Count() == 0){
            throw new Exception($"No {fileExtension} found");
        }
        if(allFilesWithFileExtension.Count() > 1){
            throw new Exception($"Found more than one {fileExtension} : {String.Join(", ", allFilesWithFileExtension.Select(f => f.Name))}");
        }

        //Copy the .apk from bin folder to output folder
        var file = allFilesWithFileExtension.First();
        return file;
    }

    /// <summary>
    /// Finds a single file that ends with a file extension
    /// </summary>
    /// <param name="path">The path to look for the file</param>
    /// <param name="fileName">The file extension that the file ends with</param>
    /// <returns>A fileinfo object of the file</returns>
    /// <exception cref="Exception">When theres 0 or more than 1 file ending with the file extensions in the path</exception>
    public static FileInfo FindSingleFileByName(string path, string fileName){
        var allFilesAfterBuild = new DirectoryInfo(path).GetFiles();
        var allFilesWithName = allFilesAfterBuild.Where(f => f.Name.ToLower() == fileName.ToLower());
        if(allFilesWithName.Count() == 0){
            throw new Exception($"No {fileName} found");
        }
        if(allFilesWithName.Count() > 1){
            throw new Exception($"Found more than one {fileName} : {String.Join(", ", allFilesWithName.Select(f => f.Name))}");
        }

        var file = allFilesWithName.First();
        return file;
    }

    public static Task PrependToFile(string filePath, string newContent){
        string currentContent = String.Empty;
        if (File.Exists(filePath))
        {
            currentContent = File.ReadAllText(filePath);
        }
        return File.WriteAllTextAsync(filePath, newContent + currentContent );
    }
    
}