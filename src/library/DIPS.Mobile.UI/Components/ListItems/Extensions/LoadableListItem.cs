using System.Windows.Input;
using DIPS.Mobile.UI.Internal;
using ActivityIndicator = DIPS.Mobile.UI.Components.Loading.ActivityIndicator;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;

namespace DIPS.Mobile.UI.Components.ListItems.Extensions;

public partial class LoadableListItem : ListItem
{
    private readonly Grid m_busyContent = new()
    {
        AutomationId = "BusyContentGrid".ToDUIAutomationId<LoadableListItem>(),
        ColumnSpacing = Sizes.GetSize(SizeName.size_2),
        ColumnDefinitions = new ColumnDefinitionCollection
        {
            new(GridLength.Star), new(GridLength.Auto)
        },
        RowDefinitions = new RowDefinitionCollection
        {
            new(GridLength.Auto)
        },
        VerticalOptions = LayoutOptions.Center
    };

    private readonly Grid m_errorContent = new()
    {
        AutomationId = "ErrorContentGrid".ToDUIAutomationId<LoadableListItem>(),
        ColumnSpacing = Sizes.GetSize(SizeName.size_2),
        ColumnDefinitions = new ColumnDefinitionCollection
        {
            new(GridLength.Star), 
            new(GridLength.Auto)
        },
        RowDefinitions = new RowDefinitionCollection
        {
            new(GridLength.Auto)
        },
        VerticalOptions = LayoutOptions.Center
    };

    private Grid ContentGrid { get; } = new()
    {
        AutomationId = "ContentGrid".ToDUIAutomationId<LoadableListItem>(),
        ColumnDefinitions = new ColumnDefinitionCollection
        {
            new(GridLength.Star),
            new(GridLength.Auto)
        },
        RowDefinitions = new RowDefinitionCollection
        {
            new (GridLength.Auto)
        },
        VerticalOptions = LayoutOptions.Center
    };
    
    private ICommand? m_cachedCommand;
    private object? m_cachedCommandParameter;

    public LoadableListItem()
    {
        CreateBusyContent();
        CreateErrorContent();
    }

    protected override void OnHandlerChanging(HandlerChangingEventArgs args)
    {
        base.OnHandlerChanging(args);
        
        if(args.NewHandler is null)
            return;
        
        m_cachedCommand = Command;
        m_cachedCommandParameter = CommandParameter;

        if (StaticContentItem is not null)
        {
            ContentGrid.Add(StaticContentItem, 1);
        }
        
        if (IsBusy)
        {
            SetBusyContent();
        }
        else if(IsError)
        {
            SetErrorContent();
        }
        else
        {
            SetContent();
        }
    }

    protected override void AddInLineContent()
    {
        SetInLineContent(ContentGrid);
    }

    private void CreateErrorContent()
    {
        var errorText = new Labels.Label
        {
            AutomationId = "ErrorTextLabel".ToDUIAutomationId<LoadableListItem>(),
            VerticalTextAlignment = TextAlignment.Center,
            HorizontalTextAlignment = TextAlignment.End
        };
        
        errorText.SetBinding(Label.TextColorProperty, static (LoadableListItem loadableListItem) => loadableListItem.ErrorTextColor, source: this);
        errorText.SetBinding(Label.TextProperty, static (LoadableListItem loadableListItem) => loadableListItem.ErrorText, source: this);
        m_errorContent.Add(errorText);

        var errorImage = new Image
        {
            AutomationId = "ErrorImage".ToDUIAutomationId<LoadableListItem>(),
            TintColor = Colors.GetColor(ColorName.color_icon_danger), 
            Source = Icons.GetIcon(IconName.failure_fill)
        };
        m_errorContent.Add(errorImage, 1);
    }

    private void CreateBusyContent()
    {
        var busyText = new Labels.Label
        {
            AutomationId = "BusyTextLabel".ToDUIAutomationId<LoadableListItem>(),
            VerticalTextAlignment = TextAlignment.Center,
            HorizontalTextAlignment = TextAlignment.End,
            TextColor = Colors.GetColor(ColorName.color_text_subtle)
        };
        busyText.SetBinding(Label.TextProperty, static (LoadableListItem loadableListItem) => loadableListItem.BusyText, source: this);
        m_busyContent.Add(busyText);
        
        var busyActivityIndicator = new ActivityIndicator
        {
            AutomationId = "BusyActivityIndicator".ToDUIAutomationId<LoadableListItem>(),
            IsRunning = true, 
            Color = Colors.GetColor(ColorName.color_text_subtle)
        };
        
        m_busyContent.Add(busyActivityIndicator, 1);
    }

    private void SetBusyContent()
    {
        if(IsError)
            return;

        _ = SwitchContentTo(m_busyContent);
        IsEnabled = false;
    }
    
    private void SetContent()
    {
        if(IsError)
            return;

        IsEnabled = true;
        
        Command = m_cachedCommand;
        CommandParameter = m_cachedCommandParameter;

        _ = SwitchContentTo((InLineContent as View)!);
    }
    
    private void SetErrorContent()
    {
        IsEnabled = true;

        _ = SwitchContentTo(m_errorContent);

        if (OnErrorTappedCommand is null)
        {
            Command = m_cachedCommand;
            CommandParameter = m_cachedCommandParameter;
            return;
        }
        
        Command = OnErrorTappedCommand;
        CommandParameter = OnErrorTappedCommandParameter;
    }

    private static void OnIsBusyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if(bindable is not LoadableListItem loadableListItem)
            return;

        if (loadableListItem.IsError)
        {
            return;
        }

        if (newValue is true)
        {
            loadableListItem.SetBusyContent();
        }
        else
        {
            loadableListItem.SetContent();
        }
    }

    private static void OnIsErrorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if(bindable is not LoadableListItem loadableListItem)
            return;
        
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
            loadableListItem.SetContent();
        }
    }


    private async Task SwitchContentTo(View view)
    {
        var child = ContentGrid.Children.FirstOrDefault();

        var replace = child is not null && child != StaticContentItem;
        
        if (FadeContentIn && replace && child is View childView)
        {
            view.Opacity = 0;
            await childView.FadeTo(0, easing: Easing.CubicInOut);
        }

        if(replace)
            ContentGrid.RemoveAt(0);
        ContentGrid.Insert(0, view);
        
        if(FadeContentIn)
        {
            await view.FadeTo(1, easing: Easing.CubicInOut);
        }
    }
}