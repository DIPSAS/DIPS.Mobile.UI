public static class WriteToFileHelper
{
    public static async void WriteToEnumFile(string enumFilePath, string[] enumsToAdd, Func<string,string> enumCommentToAdd, string[] enumsToRemove)
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
            var lastComma = enumContent.LastIndexOf(',');
            if (lastComma != -1)
            {
                string comment = enumCommentToAdd.Invoke(enumToAdd);
                highestIndex++;
                enumContent = enumContent.Remove(lastComma, 1).Insert(lastComma, $", \n{comment}\n{enumToAdd}={highestIndex},");
            }
        }

        //Remove enums
        foreach (var enumToRemove in enumsToRemove)
        {
            var lineThatContains = enums.FirstOrDefault(s => s.Contains(enumToRemove));
            if(lineThatContains != null)
            {
                enumContent = enumContent.Replace(lineThatContains+",", "");
            }
        }

        //Insert generated comment
        var generatedCommentSplit = enumContent.Split("*/");
        if(generatedCommentSplit.Length > 0)
        {
            var generatedComment = $"/*\nDo not edit directly,\nthis file is generated\n*/";
            enumContent = enumContent.Replace(generatedCommentSplit[0]+"*/", generatedComment);
        }

        Console.WriteLine(enumContent);
        // await File.WriteAllTextAsync(enumFilePath, enumContent);
    }
}