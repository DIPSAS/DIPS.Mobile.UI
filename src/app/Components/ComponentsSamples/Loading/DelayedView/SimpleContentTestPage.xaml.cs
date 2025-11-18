using System.Diagnostics;
using DIPS.Mobile.UI.Components.Pages;

namespace Components.ComponentsSamples.Loading.DelayedView;

public partial class SimpleContentTestPage
{
    public SimpleContentTestPage(float renderingDelay)
    {
        InitializeComponent();
        DelayedContent.SecondsUntilRender = renderingDelay;
    }
}
