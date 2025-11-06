using System.Diagnostics;
using DIPS.Mobile.UI.Components.Pages;

namespace Components.ComponentsSamples.Loading.DelayedView;

public partial class SimpleContentTestPage
{
    private readonly Stopwatch m_stopwatch = new();

    public SimpleContentTestPage(float renderingDelay)
    {
        m_stopwatch.Start();
        InitializeComponent();
        DelayedContent.SecondsUntilRender = renderingDelay;
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();

        m_stopwatch.Stop();
        
        if (BindingContext is SimpleContentTestPageViewModel viewModel)
        {
            viewModel.NavigationTimeMessage = $"Navigation took {m_stopwatch.ElapsedMilliseconds}ms";
        }
    }
}
