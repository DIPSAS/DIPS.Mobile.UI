namespace Components;

public class Sample
{
    public SampleType Type { get; }
    public string Name { get; }
    public Func<ContentPage> PageCreator { get; }

    public Sample(SampleType type, string name, Func<DIPS.Mobile.UI.Components.Pages.ContentPage> pageCreator)
    {
        Type = type;
        Name = name;
        PageCreator = pageCreator;
    }
}