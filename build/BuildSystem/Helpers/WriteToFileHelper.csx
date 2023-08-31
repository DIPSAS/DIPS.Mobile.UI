public static class WriteToFileHelper
{
    public static async void WriteToEnumFile(string enumFilePath, string[] enumsToAdd, string[] enumsToRemove, Func<string,string> enumCommentToAdd=null)
    {
        if(enumsToAdd.Length == 0 && enumsToRemove.Length == 0) return; //No need to do anything if there is nothing to add
        
        var enumContent = await File.ReadAllTextAsync(enumFilePath);
        var enums = enumContent.Split(",");

        //Get highest index of enums
        var highestIndex = -1;
        foreach (var theEnum in enums)
        {
            var result = theEnum[(theEnum.LastIndexOf('=') + 1)..];
            if(int.TryParse(result, out var theEnumIndex))
            {
                if (theEnumIndex > highestIndex)
                {
                    highestIndex = theEnumIndex;
                }
            }
        }

        //Add enums
        foreach (var enumToAdd in enumsToAdd)
        {
            var comment = enumCommentToAdd?.Invoke(enumToAdd);
            highestIndex++;
            var value = $"\n{enumToAdd}={highestIndex}";
            if(comment != null)
            {
                value = $"\n{comment}" + value;
            }
            enumContent = AddValueToContentWithCommas(enumContent, value);
        }

        //Remove enums
        foreach (var enumToRemove in enumsToRemove)
        {
            enumContent = RemoveTextFromContentWithCommas(enumContent, () => enums.FirstOrDefault(s => s.Contains(enumToRemove)));
        }

        //Insert generated comment
        enumContent = AddGeneratedComment(enumContent);

        await File.WriteAllTextAsync(enumFilePath, enumContent);
    }

    public static async void WriteToResourcesDictionary(string resourcesClassFile, string[] keysToAdd, Func<string,string> valueToAdd, string[] keysToRemove)
    {
        if(keysToAdd.Length == 0 && keysToRemove.Length == 0) return; //No need to do anything if there is nothing to add
        var resourcesClassContent = await File.ReadAllTextAsync(resourcesClassFile);
        var keys = resourcesClassContent.Split(",");

        foreach (var keyToAdd in keysToAdd)
        {
            var value = valueToAdd.Invoke(keyToAdd);
            var keyAndValue = $"\n[\"{keyToAdd}\"] = {value}";
            resourcesClassContent = AddValueToContentWithCommas(resourcesClassContent, keyAndValue);
        }

        //Remove enums
        foreach (var keyToRemove in keysToRemove)
        {
            resourcesClassContent = RemoveTextFromContentWithCommas(resourcesClassContent, () => keys.FirstOrDefault(s => s.Contains(keyToRemove)));
        }

        resourcesClassContent = AddGeneratedComment(resourcesClassContent);
        await File.WriteAllTextAsync(resourcesClassFile, resourcesClassContent);

    }

    public static async void UpdateResourceDictionary(string resourcesClassFile, string keyToUpdate, Func<string,string> valueToAdd)
    {
        var resourcesClassContent = await File.ReadAllTextAsync(resourcesClassFile);
        var keys = resourcesClassContent.Split(",");
        if(!keys.Contains(keyToUpdate)) return;

        //First remove it
        resourcesClassContent = RemoveTextFromContentWithCommas(resourcesClassContent, () => keys.FirstOrDefault(s => s.Contains(keyToUpdate)));

        //Then add it
        var value = valueToAdd.Invoke(keyToUpdate);
        var keyAndValue = $"\n[\"{keyToUpdate}\"] = {value}";
        WriteLine("Updating {key} with {value}");
        resourcesClassContent = AddValueToContentWithCommas(resourcesClassContent, keyAndValue);
    
        resourcesClassContent = AddGeneratedComment(resourcesClassContent);
        await File.WriteAllTextAsync(resourcesClassFile, resourcesClassContent);

    }

    private static string AddValueToContentWithCommas(string fileContent, string value)
    {
        WriteLine($"Adding {value}");
        var lastComma = fileContent.LastIndexOf(',');
        if (lastComma != -1)
        {
            return fileContent.Remove(lastComma, 1).Insert(lastComma, $",{value},");
        }
        return fileContent;
    }

    private static string RemoveTextFromContentWithCommas(string fileContent, Func<string> whatText)
    {
        var textToRemove = whatText.Invoke();
        if(textToRemove != null)
        {
            WriteLine($"Removing {textToRemove}");
            return fileContent.Replace(textToRemove+",", "");
        }
        return fileContent;
    }

    private static string AddGeneratedComment(string fileContent)
    {
        var generatedCommentSplit = fileContent.Split("*/");
        if(generatedCommentSplit.Length > 0)
        {
            var generatedComment = $"/*\nDo not edit directly,\nthis file is generated\n*/";
            return fileContent.Replace(generatedCommentSplit[0]+"*/", generatedComment);
        }
        return fileContent;
    }
}