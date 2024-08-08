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

public class UITestContentPage : ContentPage
{
    public UITestContentPage()
    {
    }

    public void DoBleed()
    {
        DeviceDisplay.MainDisplayInfoChanged += (_, _) => {};
    }
}