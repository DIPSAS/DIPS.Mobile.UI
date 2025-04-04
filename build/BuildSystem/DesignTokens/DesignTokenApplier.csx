#load "../Helpers/WriteToFileHelper.csx"
#r "nuget:Newtonsoft.Json, 13.0.2"
using Newtonsoft.Json;


/// <summary>
/// The purpose of this class is to use the generated design tokens and apply them to the DIPS.Mobile.UI library so it best suits the needs of the consuming develppers.
/// </summary>
public static class DesignTokenApplier
{
    public static async Task TryAddIcons(DirectoryInfo libraryIconsDir, DirectoryInfo generatedIconsDir)
    {
        var oldIcons = Directory.GetFiles(libraryIconsDir.FullName, "*.svg").Select(f => new FileInfo(f));
        var generatedIcons = Directory.GetFiles(generatedIconsDir.FullName, "*.svg").Select(f => new FileInfo(f));
        //Get the newly added icons
        var newIcons = new List<FileInfo>();
        foreach (var generatedIcon in generatedIcons)
        {
            if(!oldIcons.Any(f => f.Name.Equals(generatedIcon.Name))){
                newIcons.Add(generatedIcon);
            }
        }

        //Get the deleted icons
        var deletedIcons = new List<FileInfo>();
        foreach (var oldIcon in oldIcons)
        {
            if (!generatedIcons.Any(f => f.Name.Equals(oldIcon.Name)))
            {
                deletedIcons.Add(oldIcon);
            }
        }


        await WriteToFileHelper.WriteToEnumFile(libraryIconsDir.GetFiles().FirstOrDefault(f => f.Name.Equals("IconName.cs")).FullName
                                        , newIcons.Select(f => f.Name.Replace(".svg","")).ToArray(), 
                                        deletedIcons.Select(f => f.Name.Replace(".svg","")).ToArray(), (enumName =>
                                        {
                                            return $"///<summary><a href=\"https://raw.githubusercontent.com/DIPSAS/DIPS.Mobile.UI/main/src/library/DIPS.Mobile.UI/Resources/Icons/{enumName}.svg\">View the icon in the browser</a></summary>";
                                        }));

        await WriteToFileHelper.WriteToResourcesDictionary(libraryIconsDir.GetFiles().FirstOrDefault(f => f.Name.Equals("IconResources.cs")).FullName
                                                    , newIcons.Select(f => f.Name.Replace(".svg","")).ToArray(), (key => {
                                                        return $"\"{key}.png\"";
                                                    })
                                                    ,deletedIcons.Select(f => f.Name.Replace(".svg","")).ToArray());


        //Delete all svgs in the library and replace with the generated ones, this will make sure we get changes (added, edited or removed)
        var generatedSvgFilesToAdd = generatedIconsDir.GetFiles().Where(f => f.Extension == ".svg");
        var librarySvgFilesToRemove = libraryIconsDir.GetFiles().Where(f => f.Extension == ".svg");
        foreach (var svgToRemove in librarySvgFilesToRemove)
        {
            File.Delete(svgToRemove.FullName);
        }
        foreach (var fileToAdd in generatedSvgFilesToAdd)
        {
            var destination = libraryIconsDir.FullName +"/"+ fileToAdd.Name;
            File.Copy(fileToAdd.FullName, destination);
        }

    }

    public static async Task TryAddAnimations(DirectoryInfo libraryAnimationsDir, DirectoryInfo generatedAnimationsDir)
    {
        var oldAnimations = Directory.GetFiles(libraryAnimationsDir.FullName, "*.json").Select(f => new FileInfo(f));
        var generatedAnimations = Directory.GetFiles(generatedAnimationsDir.FullName, "*.json").Select(f => new FileInfo(f));
        var newAnimations = new List<FileInfo>();
        foreach (var generatedAnimation in generatedAnimations)
        {
            if(!oldAnimations.Any(f => f.Name.Equals(generatedAnimation.Name))){
                newAnimations.Add(generatedAnimation);
            }
        }

        var deletedAnimations = new List<FileInfo>();
        foreach (var oldAnimation in oldAnimations)
        {
            if (!generatedAnimations.Any(f => f.Name.Equals(oldAnimation.Name)))
            {
                deletedAnimations.Add(oldAnimation);
            }
        }


        await WriteToFileHelper.WriteToEnumFile(libraryAnimationsDir.GetFiles().FirstOrDefault(f => f.Name.Equals("AnimationName.cs")).FullName
                                        , newAnimations.Select(f => f.Name.Replace(".json","")).ToArray(), 
                                        deletedAnimations.Select(f => f.Name.Replace(".json","")).ToArray());

        await WriteToFileHelper.WriteToResourcesDictionary(libraryAnimationsDir.GetFiles().FirstOrDefault(f => f.Name.Equals("AnimationResources.cs")).FullName
                                                    , newAnimations.Select(f => f.Name.Replace(".json","")).ToArray(), (key => {
                                                        return $"\"{key}.json\"";
                                                    })
                                                    ,deletedAnimations.Select(f => f.Name.Replace(".json","")).ToArray());


        //Delete all old jsons in the library and replace with the generated ones, this will make sure we get changes (added, edited or removed)
        var generatedAnimationFilesToAdd = generatedAnimationsDir.GetFiles().Where(f => f.Extension == ".json");
        var libraryAnimationFilesToRemove = libraryAnimationsDir.GetFiles().Where(f => f.Extension == ".json");
        foreach (var animationToRemove in libraryAnimationFilesToRemove)
        {
            File.Delete(animationToRemove.FullName);
        }

        foreach (var animationToAdd in generatedAnimationFilesToAdd)
        {
            var destination = libraryAnimationsDir.FullName +"/"+ animationToAdd.Name;
            File.Copy(animationToAdd.FullName, destination);
        }
    }

    public static async Task TryAddSizes(DirectoryInfo librarySizesDir, DirectoryInfo generatedSizesDir)
    {
            var generatedSizesJsonFile = generatedSizesDir.GetFiles().FirstOrDefault(f => f.Extension == ".json");
            var generatedSizesJsonContent = await File.ReadAllTextAsync(generatedSizesJsonFile.FullName);
            var generatedSizes = JsonConvert.DeserializeObject<Dictionary<string, string>>(generatedSizesJsonContent);
            
            var librarySizesJsonFile = librarySizesDir.GetFiles().FirstOrDefault(f => f.Extension == ".json");
            var librarySizesJsonContent = await File.ReadAllTextAsync(librarySizesJsonFile.FullName);
            var librarySizes = JsonConvert.DeserializeObject<Dictionary<string, string>>(librarySizesJsonContent);
    
            Dictionary<string, string> newSizes = new Dictionary<string, string>();
            Dictionary<string, string> removedSizes = new Dictionary<string, string>();
            Dictionary<string, string> updatedSizes = new Dictionary<string, string>();
            DictionaryDiff(generatedSizes, librarySizes, newSizes, removedSizes, updatedSizes);

            //Add and remove
            await WriteToFileHelper.WriteToResourcesDictionary(librarySizesDir.GetFiles().FirstOrDefault(f => f.Name.Equals("SizeResources.cs")).FullName
                                                        , newSizes.Select(keyValue => keyValue.Key).ToArray(), (key => {
                                                            return generatedSizes.FirstOrDefault(keyValue => keyValue.Key == key).Value;
                                                        })
                                                        ,removedSizes.Select(keyValue => keyValue.Key).ToArray());
            await WriteToFileHelper.WriteToEnumFile(librarySizesDir.GetFiles().FirstOrDefault(f => f.Name.Equals("SizeName.cs")).FullName
                                , newSizes.Select(keyValue => keyValue.Key).ToArray(), 
                                removedSizes.Select(keyValue => keyValue.Key).ToArray());
            //Update
            foreach (var updatedSize in updatedSizes)
            {
                await WriteToFileHelper.UpdateResourceDictionary(librarySizesDir.GetFiles().FirstOrDefault(f => f.Name.Equals("SizeResources.cs")).FullName, updatedSize.Key, (key =>
                {
                    return generatedSizes.FirstOrDefault(keyValue => keyValue.Key == key).Value;
                }));
            }

        //Update json file

        File.Copy(generatedSizesJsonFile.FullName, librarySizesJsonFile.FullName, true);
    }

    public static async Task<bool> TryAddColors(DirectoryInfo libraryColorsDir, DirectoryInfo generatedColorsDir)
    {
        var generatedColorsJsonFile = generatedColorsDir.GetFiles().FirstOrDefault(f => f.Extension == ".json");
        var generatedColorsJsonContent = await File.ReadAllTextAsync(generatedColorsJsonFile.FullName);
        var generatedColors = JsonConvert.DeserializeObject<Dictionary<string, string>>(generatedColorsJsonContent);
        
        var libraryColorsJsonFile = libraryColorsDir.GetFiles().FirstOrDefault(f => f.Extension == ".json");
        var libraryColorsJsonContent = await File.ReadAllTextAsync(libraryColorsJsonFile.FullName);
        var libraryColors = JsonConvert.DeserializeObject<Dictionary<string, string>>(libraryColorsJsonContent);

        Dictionary<string, string> newColors = new Dictionary<string, string>();
        Dictionary<string, string> removedColors = new Dictionary<string, string>();
        Dictionary<string, string> updatedColors = new Dictionary<string, string>();
        DictionaryDiff(generatedColors, libraryColors, newColors, removedColors, updatedColors);
        if(!newColors.Any() && !removedColors.Any() && !updatedColors.Any())
        {
            return false;
        }

        //Add and remove
        await WriteToFileHelper.WriteToResourcesDictionary(libraryColorsDir.GetFiles().FirstOrDefault(f => f.Name.Equals("ColorResources.cs")).FullName
                                                    , newColors.Select(keyValue => keyValue.Key).ToArray(), (key => {
                                                        var value = generatedColors.FirstOrDefault(keyValue => keyValue.Key == key).Value;
                                                        return $"Color.FromArgb(\"{value}\")";
                                                    })
                                                    ,removedColors.Select(keyValue => keyValue.Key).ToArray());
        await WriteToFileHelper.WriteToEnumFile(libraryColorsDir.GetFiles().FirstOrDefault(f => f.Name.Equals("ColorName.cs")).FullName
                            , newColors.Select(keyValue => keyValue.Key).ToArray(), 
                            removedColors.Select(keyValue => keyValue.Key).ToArray());
        //Update
        foreach (var updatedSize in updatedColors)
        {
            await WriteToFileHelper.UpdateResourceDictionary(libraryColorsDir.GetFiles().FirstOrDefault(f => f.Name.Equals("ColorResources.cs")).FullName, updatedSize.Key, (key =>
            {
                var value = generatedColors.FirstOrDefault(keyValue => keyValue.Key == key).Value;
                return $"Color.FromArgb(\"{value}\")";
            }));
        }

        //Update json file
        File.Copy(generatedColorsJsonFile.FullName, libraryColorsJsonFile.FullName, true);
        return true;
    }
    
    private static void DictionaryDiff(
        Dictionary<string,string> firstDictionary,
        Dictionary<string,string> secondDictionary,
        Dictionary<string,string> newValues, 
        Dictionary<string, string> deletedValues, 
        Dictionary<string,string> updatedValues)
    {
        foreach (var generatedSizeKeyValue in firstDictionary)
            {
                var key = generatedSizeKeyValue.Key;
                var value = generatedSizeKeyValue.Value;
                var theSameSize = secondDictionary.FirstOrDefault(KeyValuePair => KeyValuePair.Key == key);
                if (theSameSize.Key == null) //Its a new size
                {
                    newValues.Add(key, value);
                }
                else //It exists from before
                {
                    if(theSameSize.Value != value) //But the value has changed
                    {
                        updatedValues.Add(key, value);
                    }
                }
            }

            foreach (var librarySizeKeyvalue in secondDictionary)
            {
                var key = librarySizeKeyvalue.Key;
                var value = librarySizeKeyvalue.Key;
                var theSameSize = firstDictionary.FirstOrDefault(KeyValuePair => KeyValuePair.Key == key);
                if(theSameSize.Key == null) //It got removed
                {
                    deletedValues.Add(key, value);
                }
            }
    }
}