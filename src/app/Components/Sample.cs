namespace Components;

public class Sample
{
    public SampleType Type { get; }
    public string Name { get; }
    public Func<ContentPage> PageCreator { get; }
    public bool IsModal { get; }

    public Sample(SampleType type, string name, Func<DIPS.Mobile.UI.Components.Pages.ContentPage> pageCreator, bool isModal = false)
    {
        Type = type;
        Name = name;
        PageCreator = pageCreator;
        IsModal = isModal;
    }
}