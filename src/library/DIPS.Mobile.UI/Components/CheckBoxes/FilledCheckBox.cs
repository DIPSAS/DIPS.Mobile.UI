using DIPS.Mobile.UI.Components.Images.Image;
using DIPS.Mobile.UI.Effects.Touch;
using Microsoft.Maui.Controls.Shapes;
using ActivityIndicator = DIPS.Mobile.UI.Components.Loading.ActivityIndicator;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;

namespace DIPS.Mobile.UI.Components.CheckBoxes;

public partial class FilledCheckBox : ContentView
{

    private Border m_innerBorder;
    private readonly Image m_innerImage;
    private readonly ActivityIndicator m_activityIndicator;

    public FilledCheckBox()
    {
        Container = new Border
        {
            BackgroundColor = Colors.GetColor(ColorName.color_system_white),
            Padding = Sizes.GetSize(SizeName.size_0)
        };

        m_innerBorder = new Border
        {
            BackgroundColor = CheckedBackgroundColor,
            Scale = Sizes.GetSize(SizeName.size_0)
        };

        m_innerImage = new Image
        {
            TintColor = Colors.GetColor(ColorName.color_secondary_30),
            Source = Icons.GetIcon(IconName.check_line),
            Aspect = Aspect.AspectFill,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center
        };

        m_activityIndicator = new ActivityIndicator { Opacity = Sizes.GetSize(SizeName.size_0) };
        
        InnerGrid = new Grid
        {
            Children =
            {
                m_innerBorder,
                m_activityIndicator,
                m_innerImage
            }
        };

        Container.Content = InnerGrid;

        _ = SetContainerContent();

        Content = Container;
    }

    private Grid InnerGrid { get; }
    private Border Container { get; }
    
    
    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        
        Touch.SetCommand(Container, Command);
        Container.StrokeShape = new RoundRectangle{ CornerRadius = CornerRadius };
        m_innerBorder.StrokeShape = new RoundRectangle { CornerRadius = CornerRadius };
        
        m_innerImage.WidthRequest = WidthRequest / 1.5;
        m_innerImage.HeightRequest = HeightRequest / 1.5;
    }

    private async Task Animate()
    {
        if (IsChecked)
        {
            _ = m_innerBorder.FadeTo(1, 500, easing: Easing.CubicIn);
            _ = m_innerImage.FadeTo(1, easing: Easing.CubicInOut);
            _ = m_innerImage.ScaleTo(1.25, length: 500, Easing.CubicInOut);
            await m_innerBorder.ScaleTo(1, 500, easing: Easing.CubicInOut);
            await Task.Delay(100);
            await m_innerImage.ScaleTo(1, easing: Easing.CubicInOut);
            
            CompletedCommand?.Execute(null);
        }
        else
        {
            _ = m_innerBorder.ScaleTo(0, 500, easing: Easing.CubicInOut);
            _ = m_innerBorder.FadeTo(0, 500, easing: Easing.CubicOut);
        }
    }
    
    private async Task SetContainerContent()
    {
        if (IsProgressing)
        {
            _ = m_innerImage.FadeTo(0);
            await m_activityIndicator.FadeTo(1, easing: Easing.CubicInOut);
        }
        else
        {
            await m_activityIndicator.FadeTo(0, easing: Easing.CubicInOut);
        }
        m_activityIndicator.IsRunning = IsProgressing;
    }
    
    private static void OnIsCheckedChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if(bindable is not FilledCheckBox filledCheckBox)
            return;
        
        _ = filledCheckBox.Animate();
    }

    private static void OnIsProgressingChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if(bindable is not FilledCheckBox filledCheckBox)
            return;

        _ = filledCheckBox.SetContainerContent();
    }
}