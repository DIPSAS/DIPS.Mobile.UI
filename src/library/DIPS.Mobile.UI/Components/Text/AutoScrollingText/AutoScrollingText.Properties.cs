using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.Components.Text.AutoScrollingText;

public partial class AutoScrollingText
{
    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public Color? FadeColor
    {
        get => (Color?)GetValue(FadeColorProperty);
        set => SetValue(FadeColorProperty, value);
    }

    public new Style Style
    {
        get => (Style)GetValue(StyleProperty);
        set => SetValue(StyleProperty, value);
    }

    public bool ShouldFadeOut
    {
        get => (bool)GetValue(ShouldFadeOutProperty);
        set => SetValue(ShouldFadeOutProperty, value);
    }

    public static readonly BindableProperty ShouldFadeOutProperty = BindableProperty.Create(
        nameof(ShouldFadeOut),
        typeof(bool),
        typeof(AutoScrollingText),
        true,
        defaultBindingMode: BindingMode.OneTime);

    public static readonly new BindableProperty StyleProperty = BindableProperty.Create(
        nameof(Style),
        typeof(Style),
        typeof(AutoScrollingText),
        defaultValue: Label.DefaultLabelStyle,
        defaultBindingMode: BindingMode.OneTime);

    public static readonly BindableProperty FadeColorProperty = BindableProperty.Create(
        nameof(FadeColor),
        typeof(Color),
        typeof(AutoScrollingText),
        defaultBindingMode: BindingMode.OneTime,
        propertyChanged: (bindable, _, _) =>
        {
            if (bindable is AutoScrollingText autoScrollingText)
            {
                autoScrollingText.SetFadingBoxFade();
            }
        });

    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        nameof(Text),
        typeof(string),
        typeof(AutoScrollingText),
        propertyChanged: (bindable, _, _) =>
        {
            if (bindable is not AutoScrollingText autoScrollingText)
                return;

            autoScrollingText.m_label.Text = autoScrollingText.Text;
        });

    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
        nameof(TextColor),
        typeof(Color),
        typeof(AutoScrollingText),
        Colors.GetColor(ColorName.color_text_default),
        defaultBindingMode: BindingMode.OneTime);
}