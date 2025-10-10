public static class PackageIdentifier
{
    public static string ReplacePostFix(string postfixToReplace, string newPostFix, string packageName)
    {
        if(!postfixToReplace.StartsWith("."))
        {
            postfixToReplace = $".{postfixToReplace}";
        }
        if(!newPostFix.StartsWith("."))
        {
            newPostFix = $".{newPostFix}";
        }
        var newPackageName = packageName;
        if (packageName.EndsWith(postfixToReplace))
        {
            newPackageName = packageName.Substring(0, packageName.Length - postfixToReplace.Length) + newPostFix;
        }
        return newPackageName;
    }
}