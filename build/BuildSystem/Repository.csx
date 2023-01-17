
public static class Repository{
    public static string RootDir()
    {
        for (var current = Environment.CurrentDirectory; current != null; current = Path.GetDirectoryName(current))
        {
            var check = Path.Combine(current, ".git");
            if (Directory.Exists(check))
            {
                return current;
            }
        }
        throw new Exception("Unable to find .git folder. Are you in a git repo?");
    }
}
