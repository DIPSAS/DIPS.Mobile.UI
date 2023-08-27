using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Components.ResourcesSamples.Icons;
using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Resources.Animations;
using SkiaSharp.Extended.UI.Controls;
using SkiaSharp.Extended.UI.Controls.Converters;
using SkiaSharp.Views.Maui.Controls;
using Enum = System.Enum;

namespace Components.ResourcesSamples.Animations;

public partial class AnimationsSamples
{
    private List<AnimationName> m_animations;
    private List<AnimationName> m_allAnimations;

    public AnimationsSamples()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var animations = AnimationResources.Animations;
        m_allAnimations = new List<AnimationName>();
        foreach (var animation in animations)
        {
            if (Enum.TryParse<AnimationName>(animation.Key, out var theEnum))
            {
                m_allAnimations.Add(theEnum);
            }
        }

        Animations = m_allAnimations;
    }

    public List<AnimationName> Animations
    {
        get => m_animations;
        private set
        {
            m_animations = value;
            OnPropertyChanged();
        }
    }

    private void InputView_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(e.NewTextValue))
        {
            Animations = m_allAnimations;
        }
        else
        {
            var matchingAnimations =
                m_allAnimations.Where(c => c.ToString().ToLower().Contains(e.NewTextValue.ToLower()));
            Animations = matchingAnimations.ToList();
        }
    }

    public ICommand OpenAnimationCommand => new Command<SKLottieView>(skLottieView =>
    {
        skLottieView.Progress = new TimeSpan(0);
    });


    private void VisualElement_OnLoaded(object? sender, EventArgs e)
    {
        if (sender is SKLottieView skLottieView)
        {
            if (skLottieView.BindingContext is AnimationName animationName)
            {
                skLottieView.Source = DIPS.Mobile.UI.Resources.Animations.Animations.GetAnimation(animationName);
                skLottieView.IsAnimationEnabled = true;
                skLottieView.Progress = new TimeSpan(0);
            }
        }
    }
}