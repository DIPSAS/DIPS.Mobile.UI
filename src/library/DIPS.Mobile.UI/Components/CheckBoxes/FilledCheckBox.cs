using System.ComponentModel;
using DIPS.Mobile.UI.Components.Images.Image;
using DIPS.Mobile.UI.Effects.Touch;
using DIPS.Mobile.UI.Resources.Animations;
using Microsoft.Maui.Controls.Shapes;
using SkiaSharp.Extended.UI.Controls;
using SkiaSharp.Extended.UI.Controls.Themes;
using ActivityIndicator = DIPS.Mobile.UI.Components.Loading.ActivityIndicator;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;

namespace DIPS.Mobile.UI.Components.CheckBoxes;

public partial class FilledCheckBox : ContentView
{
    private readonly ActivityIndicator m_activityIndicator;
    private readonly SKLottieView m_animation;
    private readonly Image m_nonCheckedImage;
    internal static double IsNotCheckedOpacity => 0.25;

    public FilledCheckBox()
    {
        Container = new Border {Padding = Sizes.GetSize(SizeName.size_0)};
        Container.SetBinding(BackgroundColorProperty, new Binding(nameof(UnCheckedBackgroundColor), source: this));
        WidthRequest = Sizes.GetSize(SizeName.size_25);
        HeightRequest = Sizes.GetSize(SizeName.size_25);
        CornerRadius = HeightRequest / 2;

        m_animation = new SKLottieView()
        {
            Source = Animations.GetAnimation(AnimationName.saved),
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            IsAnimationEnabled = true,
            Opacity = IsNotCheckedOpacity
        };

        m_animation.SetBinding(HeightRequestProperty, new Binding(source: this, path: nameof(HeightRequest)));
        m_animation.SetBinding(WidthRequestProperty, new Binding(source: this, path: nameof(WidthRequest)));

        m_activityIndicator = new ActivityIndicator {Opacity = 0};

        InnerGrid = new Grid {m_activityIndicator, m_animation};

        Container.Content = InnerGrid;

        _ = SetContainerContent();

        Content = Container;
    }

    private Grid InnerGrid { get; }
    private Border Container { get; }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();

        Container.StrokeShape = new RoundRectangle {CornerRadius = CornerRadius};
    }

    private async Task Animate()
    {
        if (IsChecked)
        {
            await m_animation.FadeTo(0);
            _ = m_animation.FadeTo(1, easing: Easing.CubicInOut);

            //Reset the animation and listen to when its completed
            m_animation.PropertyChanged += OnAnimationPropertyChanged;
            m_animation.IsAnimationEnabled = true;
            m_animation.Progress = new TimeSpan(0);
        }
        else
        {
            m_animation.IsAnimationEnabled = false;
            _ = m_animation.FadeTo(IsNotCheckedOpacity, 500, easing: Easing.CubicOut);
            m_animation.PropertyChanged -= OnAnimationPropertyChanged;
        }
    }

    private void OnAnimationPropertyChanged(object? sender, PropertyChangedEventArgs args)
    {
        if (args.PropertyName != null && args.PropertyName.Equals(SKLottieView.IsCompleteProperty.PropertyName) &&
            m_animation.IsComplete)
        {
            CompletedCommand?.Execute(null);
            Completed?.Invoke(this, EventArgs.Empty);
        }
    }

    private async Task SetContainerContent()
    {
        if (IsProgressing)
        {
            _ = m_animation.FadeTo(0);
            await m_activityIndicator.FadeTo(1, easing: Easing.CubicInOut);
        }
        else
        {
            if (IsChecked)
            {
                _ = m_animation.FadeTo(1);
            }
            else
            {
                _ = m_animation.FadeTo(IsNotCheckedOpacity);
            }

            await m_activityIndicator.FadeTo(0, easing: Easing.CubicInOut);
        }

        m_activityIndicator.IsRunning = IsProgressing;
    }

    private static void OnIsCheckedChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not FilledCheckBox filledCheckBox)
            return;

        _ = filledCheckBox.Animate();
    }

    private static void OnIsProgressingChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not FilledCheckBox filledCheckBox)
            return;

        _ = filledCheckBox.SetContainerContent();
    }

    private void OnCommandChanged()
    {
        Touch.SetCommand(this, Command);
    }
}