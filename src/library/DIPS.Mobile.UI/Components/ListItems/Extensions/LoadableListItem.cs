using System.Windows.Input;
using ActivityIndicator = DIPS.Mobile.UI.Components.Loading.ActivityIndicator;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;

namespace DIPS.Mobile.UI.Components.ListItems.Extensions;

public partial class LoadableListItem : ListItem
{
    private Grid m_busyContent;

    private Grid m_errorContent;

    private Grid ContentGrid { get; } = new()
    {
        ColumnDefinitions = new ColumnDefinitionCollection
        {
            new(GridLength.Star)
        },
        RowDefinitions = new RowDefinitionCollection
        {
            new (GridLength.Auto)
        }
    };

    public LoadableListItem()
    {
        CreateBusyContent();
        CreateErrorContent();
    }

    private void CreateErrorContent()
    {
        m_errorContent = new Grid
        {
            ColumnSpacing = Sizes.GetSize(SizeName.size_2),
            ColumnDefinitions = new ColumnDefinitionCollection
            {
                new(GridLength.Star), new(GridLength.Auto)
            },
            VerticalOptions = LayoutOptions.Center
        };

        var errorText = new Labels.Label { VerticalTextAlignment = TextAlignment.Center };
        errorText.SetBinding(Label.TextColorProperty, new Binding(nameof(ErrorTextColor), source: this));
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
            ColumnSpacing = Sizes.GetSize(SizeName.size_2),
            ColumnDefinitions = new ColumnDefinitionCollection
            {
                new(GridLength.Star), new(GridLength.Auto)
            }
        };

        var busyText = new Labels.Label { VerticalTextAlignment = TextAlignment.Center, TextColor = Colors.GetColor(ColorName.color_neutral_90)};
        busyText.SetBinding(Label.TextProperty, new Binding(nameof(BusyText), source: this));
        m_busyContent.Add(busyText);
        
        var busyActivityIndicator = new ActivityIndicator { IsRunning = true };
        m_busyContent.Add(busyActivityIndicator, 1);
    }
    
    private ICommand? m_cachedCommand;
    private object? m_cachedCommandParameter;

    
    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();

        m_cachedCommand = Command;
        m_cachedCommandParameter = CommandParameter;

        HorizontalContentItem = ContentGrid;

        if (StaticContentItem is not null)
        {
            ContentGrid.Add(StaticContentItem, 1);
            ContentGrid.AddColumnDefinition(new ColumnDefinition(GridLength.Auto));
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

        _ = SwitchContentTo(LoadedContentItem);
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

        if (LoadedContentItem != null)
        {
            LoadedContentItem.InputTransparent = view != LoadedContentItem;
        }
    }
}