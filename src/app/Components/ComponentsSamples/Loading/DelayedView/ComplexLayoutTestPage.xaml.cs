using System.Diagnostics;
using DIPS.Mobile.UI.Components.Pages;

namespace Components.ComponentsSamples.Loading.DelayedView;

public partial class ComplexLayoutTestPage
{
    public ComplexLayoutTestPage(float renderingDelay)
    {
        InitializeComponent();
        DelayedContent.SecondsUntilRender = renderingDelay;
        DelayedContent.OnRendered += DelayedContentOnOnRendered;
    }

    private void DelayedContentOnOnRendered(object? sender, EventArgs e)
    {
        Console.WriteLine("ComplexLayoutTestPage: Delayed content has been rendered.");
    }
}
