namespace MemoryLeakTests.Tests;

public abstract class UITest
{
    public abstract void BeforeTest(ContentPage contentPage);

    public abstract string Name { get; }

    public ContentPage CreatePage()
    {
        var uiTestContentPage = new UITestContentPage();
        uiTestContentPage.AutomationId = Name;
        uiTestContentPage.Title = Name;
        BeforeTest(uiTestContentPage);
        return uiTestContentPage;
    }
}

public class UITestContentPage : DIPS.Mobile.UI.Components.Pages.ContentPage
{
    public UITestContentPage()
    {
       DoBleed();
    }

    public void DoBleed()
    {
        DeviceDisplay.MainDisplayInfoChanged += OnBleed;
    }

    private void OnBleed(object? sender, DisplayInfoChangedEventArgs e)
    {
        _ = this.Title;
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();

        if (Handler is null)
        {
            DeviceDisplay.MainDisplayInfoChanged -= OnBleed;
        }
    }
}