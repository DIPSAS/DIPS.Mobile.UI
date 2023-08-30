public static class WriteToFileHelper
{
    public static async void WriteToEnumFile(string enumFilePath, string[] enumsToAdd, Func<string,string> enumCommentToAdd, string[] enumsToRemove)
    {
        var enumContent = await File.ReadAllTextAsync(enumFilePath);
        var enums = enumContent.Split(",");
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

        foreach (var enumToRemove in enumsToRemove)
        {
            var lineThatContains = enums.FirstOrDefault(s => s.Contains(enumToRemove));
            if(lineThatContains != null)
            {
                enumContent = enumContent.Replace(lineThatContains+",", "");
            }
        }

        Console.WriteLine(enumContent);
        // await File.WriteAllTextAsync(enumFilePath, enumContent);
    }
}