public static class WriteToFileHelper
{
    public static async Task WriteToEnumFile(string enumFilePath, string[] enumsToAdd, string[] enumsToRemove, Func<string,string> enumCommentToAdd=null)
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

    public static async Task WriteToResourcesDictionary(string resourcesClassFile, string[] keysToAdd, Func<string,string> valueToAdd, string[] keysToRemove)
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

    public static async Task UpdateResourceDictionary(string resourcesClassFile, string keyToUpdate, Func<string,string> valueToAdd)
    {
        var resourcesClassContent = await File.ReadAllTextAsync(resourcesClassFile);
        var keys = resourcesClassContent.Split(",");
        if(keys.FirstOrDefault(s => s.Contains(keyToUpdate)) == null) return;

        //First remove it
        resourcesClassContent = RemoveTextFromContentWithCommas(resourcesClassContent, () => keys.FirstOrDefault(s => s.Contains(keyToUpdate)));

        //Then add it
        var value = valueToAdd.Invoke(keyToUpdate);
        var keyAndValue = $"\n[\"{keyToUpdate}\"] = {value}";
        WriteLine($"Updating {keyToUpdate} with {value} ");
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

    /// <summary>
    /// Writes a complete XAML ResourceDictionary file for color tokens.
    /// Each color is written as a &lt;Color x:Key="..."&gt;#AARRGGBB&lt;/Color&gt; entry.
    /// </summary>
    /// <param name="xamlFilePath">Path to the .xaml file to write.</param>
    /// <param name="fullyQualifiedClassName">Fully qualified class name for x:Class, e.g. "DIPS.Mobile.UI.Resources.Colors.ColorsLight".</param>
    /// <param name="colors">Dictionary mapping color token keys to hex color values in #RRGGBBAA format.</param>
    /// <param name="timestamp">ISO 8601 timestamp string to embed in the file header comment.</param>
    public static async Task WriteXamlColorsDictionary(
        string xamlFilePath,
        string fullyQualifiedClassName,
        Dictionary<string, string> colors,
        string timestamp)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
        sb.AppendLine("<!--");
        sb.AppendLine("Puls Design Tokens");
        sb.AppendLine("Do not edit directly, this file is auto-generated");
        sb.AppendLine($"Generated: {timestamp}");
        sb.AppendLine("-->");
        sb.AppendLine($"<ResourceDictionary xmlns=\"http://schemas.microsoft.com/dotnet/2021/maui\"");
        sb.AppendLine($"                    xmlns:x=\"http://schemas.microsoft.com/winfx/2009/xaml\"");
        sb.AppendLine($"                    x:Class=\"{fullyQualifiedClassName}\">");
        foreach (var kvp in colors)
        {
            var xamlColor = RgbaToArgbHex(kvp.Value);
            sb.AppendLine($"    <Color x:Key=\"{kvp.Key}\">{xamlColor}</Color>");
        }
        sb.AppendLine("</ResourceDictionary>");
        await File.WriteAllTextAsync(xamlFilePath, sb.ToString());
        WriteLine($"Generated XAML color dictionary: {xamlFilePath}");
    }

    /// <summary>
    /// Writes a complete XAML ResourceDictionary file for size tokens.
    /// Each size is written as an &lt;x:Double x:Key="..."&gt;value&lt;/x:Double&gt; entry.
    /// </summary>
    /// <param name="xamlFilePath">Path to the .xaml file to write.</param>
    /// <param name="fullyQualifiedClassName">Fully qualified class name for x:Class.</param>
    /// <param name="sizes">Dictionary mapping size token keys to their numeric values as strings.</param>
    /// <param name="timestamp">ISO 8601 timestamp string to embed in the file header comment.</param>
    public static async Task WriteXamlSizesDictionary(
        string xamlFilePath,
        string fullyQualifiedClassName,
        Dictionary<string, string> sizes,
        string timestamp)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
        sb.AppendLine("<!--");
        sb.AppendLine("Puls Design Tokens");
        sb.AppendLine("Do not edit directly, this file is auto-generated");
        sb.AppendLine($"Generated: {timestamp}");
        sb.AppendLine("-->");
        sb.AppendLine($"<ResourceDictionary xmlns=\"http://schemas.microsoft.com/dotnet/2021/maui\"");
        sb.AppendLine($"                    xmlns:x=\"http://schemas.microsoft.com/winfx/2009/xaml\"");
        sb.AppendLine($"                    x:Class=\"{fullyQualifiedClassName}\">");
        foreach (var kvp in sizes)
        {
            sb.AppendLine($"    <x:Double x:Key=\"{kvp.Key}\">{kvp.Value}</x:Double>");
        }
        sb.AppendLine("</ResourceDictionary>");
        await File.WriteAllTextAsync(xamlFilePath, sb.ToString());
        WriteLine($"Generated XAML sizes dictionary: {xamlFilePath}");
    }

    /// <summary>
    /// Converts a hex color value from #RRGGBBAA format (used by Color.FromRgba) to
    /// #AARRGGBB format (used by MAUI XAML Color parsing).
    /// Returns "Transparent" unchanged.
    /// </summary>
    private static string RgbaToArgbHex(string hexValue)
    {
        if (string.IsNullOrEmpty(hexValue) || hexValue == "Transparent")
            return "Transparent";
        var h = hexValue.TrimStart('#');
        if (h.Length == 8)
        {
            var rr = h.Substring(0, 2);
            var gg = h.Substring(2, 2);
            var bb = h.Substring(4, 2);
            var aa = h.Substring(6, 2);
            return $"#{aa}{rr}{gg}{bb}";
        }
        if (h.Length == 6)
            return $"#ff{h}";
        return hexValue;
    }
}