namespace DIPS.Mobile.UI.Components.Loading.DelayedView;

/// <summary>
/// A view that delays rendering its content for a set amount of time, showing an activity indicator in the meantime
/// <remarks>Use this to workaround UI freeze when navigating to heavy pages</remarks>
/// </summary>
[ContentProperty(nameof(ItemTemplate))]
public partial class DelayedView : Grid
{
    private readonly Microsoft.Maui.Controls.ActivityIndicator m_activityIndicator;

    public DelayedView()
    {
        this.Add(m_activityIndicator = new ActivityIndicator
        {
            IsRunning = true, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center
        });
    }

    protected override async void OnHandlerChanged()
    {
        base.OnHandlerChanged();

        if (Handler is null)
            return;

        if (ItemTemplate is null)
        {
            throw new ArgumentNullException($"{nameof(ItemTemplate)} should be set here!");
        }

        await Task.Delay(TimeSpan.FromSeconds(SecondsUntilRender));
        RenderContent();
    }

    private void RenderContent()
    {
        if (Handler is null)
            return;

        var content = ItemTemplate?.CreateContent() as View;

        Add(content);
        
        OnRendered?.Invoke(this, EventArgs.Empty);
        
        RemoveAt(0);
        m_activityIndicator.DisconnectHandlers();
    }
}