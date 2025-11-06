using System.Diagnostics;

namespace Components.ComponentsSamples.Loading.DelayedView;

public partial class VeryComplexTestPage
{
    private readonly Stopwatch m_stopwatch = new();

    public VeryComplexTestPage(float renderingDelay)
    {
        m_stopwatch.Start();
        InitializeComponent();
        DelayedContent.SecondsUntilRender = renderingDelay;
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        
        m_stopwatch.Stop();
        
        if (BindingContext is CollectionViewTestPageViewModel viewModel)
        {
            viewModel.NavigationTimeMessage = $"Navigation took {m_stopwatch.ElapsedMilliseconds}ms";
        }
    }
}
