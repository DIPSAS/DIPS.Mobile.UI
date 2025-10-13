public class Product
{
    public string ProductName { get; set; }
    public List<Release> Versions { get; set;  }
}

public class Release
{
    public string Version { get; set; }
    public List<Change> ReleaseNotes { get; set; } = new List<Change>();

    public bool HasChanges => ReleaseNotes != null&& ReleaseNotes.Any(r => !r.Skip);
}

public class Change
{
    /// <summary>
    /// The title of the change.
    /// </summary>
    public string Title { get; set; }
    /// <summary>
    /// The description of the change.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// The type of release.
    /// </summary>
    public ReleaseType Type { get; set; }
    /// <summary>
    /// Determines if the change should be skipped from being added to release notes. Can be used for "early documentation of things that are not completed yet".
    /// </summary>
    public bool Skip { get; set; }

    /// <summary>
    /// A list of required actions.
    /// </summary>
    public List<string> RequiredActions { get; set; } = new List<string>();

    /// <summary>
    /// A list of dsak ids, for customer support ticket links.
    /// </summary>
    public List<int> Dsak { get; set; } = new List<int>();
    /// <summary>
    /// A list of azure work item ids, for internal azure work item links.
    /// </summary>
    public List<int> workitem { get; set; } = new List<int>();

    public bool HasAttachements => Dsak != null && Dsak.Count > 0;
}

public enum ReleaseType
{
    Unknown = 0,
    News = 1,
    Change = 2,
    BugFix = 3
}