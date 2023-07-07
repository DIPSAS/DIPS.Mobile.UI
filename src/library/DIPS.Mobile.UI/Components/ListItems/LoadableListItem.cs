using DIPS.Mobile.UI.Effects.Touch;
using ActivityIndicator = DIPS.Mobile.UI.Components.Loading.ActivityIndicator;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;

namespace DIPS.Mobile.UI.Components.ListItems;

public partial class LoadableListItem : ListItem
{
    private readonly ContentView m_busyContent = new()
    {
        Content = new ActivityIndicator { IsRunning = true, VerticalOptions = LayoutOptions.Center }
    };
    
    private readonly ContentView m_errorContent = new()
    {
        Content = new Image
        {
            TintColor = Colors.GetColor(ColorName.color_error_dark), 
            Source = Icons.GetIcon(IconName.failure_fill)
        }
    };
    
    private View? m_cachedHorizontalContentItem;

    /// Need this property to ensure we cache the right content
    private bool HandlerInitialized { get; set; }
    
    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();

        m_cachedHorizontalContentItem = (HorizontalContentItem as View)!;
        HandlerInitialized = true;
        
        Command = OnErrorTappedCommand;
        CommandParameter = OnErrorTappedCommandParameter;
        Touch.SetIsEnabled(Border, false);
        
        // Keep ListItem's height the same 
        m_busyContent.HeightRequest = m_cachedHorizontalContentItem.HeightRequest;
        m_errorContent.HeightRequest = m_cachedHorizontalContentItem.HeightRequest;

        if (IsBusy)
        {
            SetBusyContent();
        }
        else if(IsError)
        {
            SetErrorContent();
        }
    }

    private void SetBusyContent()
    {
        if(IsError)
            return;

        HorizontalContentItem = m_busyContent;
    }
    
    private void SetCachedContent()
    {
        if(IsError || m_cachedHorizontalContentItem is null)
            return;
        
        HorizontalContentItem = m_cachedHorizontalContentItem;
    }
    
    private void SetErrorContent()
    {
        HorizontalContentItem = m_errorContent;
        
        Touch.SetIsEnabled(Border, true);
    }

    private static void OnIsBusyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if(bindable is not LoadableListItem loadableListItem)
            return;

        if (!loadableListItem.HandlerInitialized)
        {
            return;
        }

        if (newValue is true)
        {
            loadableListItem.SetBusyContent();
        }
        else
        {
            loadableListItem.SetCachedContent();
        }
    }

    private static void OnIsErrorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if(bindable is not LoadableListItem loadableListItem)
            return;
        
        if (!loadableListItem.HandlerInitialized)
        {
            return;
        }

        if (newValue is true)
        {
            loadableListItem.SetErrorContent();
            return;
        }
        
        if(loadableListItem.IsBusy)
        {
            loadableListItem.SetBusyContent();
        }
        else
        {
            loadableListItem.SetCachedContent();
        }
        
        Touch.SetIsEnabled(loadableListItem.Border, false);
    }

    
}