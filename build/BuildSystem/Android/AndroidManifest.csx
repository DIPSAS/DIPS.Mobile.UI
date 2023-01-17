#load "../Helpers/FileHelper.csx"

using System.Xml;

public static class AndroidManifest{

    public static async Task<string> GetPackageName(string androidProjectPath)
    {
        var androidManifest = GetAndroidManifest(androidProjectPath);

        XmlDocument doc = await GetAndroidManifestAsXml(androidManifest);
        XmlNode node = doc.SelectSingleNode("manifest");
        return node.Attributes.GetNamedItem("package").Value;
    }

    // Update '@android:versionCode' and '@android:versionName' in AndroidManifest.xml
    // Inspiration taken from Azure Pipeline task: https://github.com/jamesmontemagno/vsts-mobile-tasks/blob/master/tasks/AndroidBumpVersion/task.ts
    public static async Task UpdateVersionNumbers(string versionCode, string versionName, string androidProjectPath){
        var androidManifest = GetAndroidManifest(androidProjectPath);

        var xmlString = await File.ReadAllTextAsync(androidManifest);
        var start = 0;
        var end = 0;

        while ((start = xmlString.IndexOf("-->", start)) != -1){
            end = xmlString.IndexOf("-->", start);
            if (end == -1)
                break; 
            xmlString = xmlString.Substring(0, start) + xmlString.Substring(end + 3);
        }

        XmlDocument doc = new XmlDocument();
        doc.LoadXml(xmlString);
        XmlNode node = doc.SelectSingleNode("manifest");
        node.Attributes.GetNamedItem("android:versionCode").Value = versionCode;
        node.Attributes.GetNamedItem("android:versionName").Value = versionName;
        doc.Save(androidManifest);
    }

    //TODO: Consider refactoring to reuse logic from AndroidUpdateVersionNumbers
    public static async Task<Tuple<string, string>> UpdatePackageName(string newPackageName, string androidProjectPath){
        var androidManifest = GetAndroidManifest(androidProjectPath);

        XmlDocument doc = await GetAndroidManifestAsXml(androidManifest);
        XmlNode node = doc.SelectSingleNode("manifest");
        var oldPackageName = node.Attributes.GetNamedItem("package").Value;
        node.Attributes.GetNamedItem("package").Value = newPackageName;
        doc.Save(androidManifest);
        return new Tuple<string, string>(oldPackageName, newPackageName);
    }

    public static async Task<Tuple<string, string>> AddPostfixToPackageName(string postfix, string androidProjectPath)
    {
        var androidManifest = GetAndroidManifest(androidProjectPath);

        XmlDocument doc = await GetAndroidManifestAsXml(androidManifest);
        XmlNode node = doc.SelectSingleNode("manifest");
        var oldPackageName = node.Attributes.GetNamedItem("package").Value;
        var newPackageName = $"{oldPackageName}.{postfix}";
        node.Attributes.GetNamedItem("package").Value = newPackageName;
        doc.Save(androidManifest);
        return new Tuple<string, string>(oldPackageName, newPackageName);
    }

    public static async Task<XmlDocument> GetAndroidManifestAsXml(string androidManifest)
    {
        var xmlString = await File.ReadAllTextAsync(androidManifest);
        var start = 0;
        var end = 0;

        while ((start = xmlString.IndexOf("-->", start)) != -1)
        {
            end = xmlString.IndexOf("-->", start);
            if (end == -1)
                break;
            xmlString = xmlString.Substring(0, start) + xmlString.Substring(end + 3);
        }

        XmlDocument doc = new XmlDocument();
        doc.LoadXml(xmlString);
        return doc;
    }

    private static string GetAndroidManifest(string projectPath){
        var manifestPath = $"{projectPath}/Properties";
        return FileHelper.FindSingleFileByName(manifestPath, "AndroidManifest.xml").FullName;
    }

}