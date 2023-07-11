using DIPS.Mobile.UI.Effects.Touch;
using ActivityIndicator = DIPS.Mobile.UI.Components.Loading.ActivityIndicator;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;

namespace DIPS.Mobile.UI.Components.ListItems;

public partial class LoadableListItem : ListItem
{
    private Grid m_busyContent;

    private Grid m_errorContent;

    public LoadableListItem()
    {
        CreateBusyContent();
        CreateErrorContent();
    }

    private void CreateErrorContent()
    {
        m_errorContent = new Grid
        {
            ColumnDefinitions = new ColumnDefinitionCollection
            {
                new(GridLength.Star), new(GridLength.Auto)
            }
        };
        
        var errorText = new Labels.Label();
        errorText.SetBinding(Label.TextProperty, new Binding(nameof(ErrorText), source: this));
        m_errorContent.Add(errorText);

        var errorImage = new Image
        {
            TintColor = Colors.GetColor(ColorName.color_error_dark), Source = Icons.GetIcon(IconName.failure_fill)
        };
        m_errorContent.Add(errorImage, 1);
    }

    private void CreateBusyContent()
    {
        m_busyContent = new Grid
        {
            ColumnDefinitions = new ColumnDefinitionCollection
            {
                new(GridLength.Star), new(GridLength.Auto)
            }
        };
        
        var busyText = new Labels.Label();
        busyText.SetBinding(Label.TextProperty, new Binding(nameof(BusyText), source: this));
        m_busyContent.Add(busyText);
        
        var busyActivityIndicator = new ActivityIndicator { IsRunning = true, VerticalOptions = LayoutOptions.Center };
        m_busyContent.Add(busyActivityIndicator, 1);
    }
    
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