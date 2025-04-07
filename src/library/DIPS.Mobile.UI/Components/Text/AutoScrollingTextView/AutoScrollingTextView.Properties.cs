using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.Components.Text.AutoScrollingTextView;

public partial class AutoScrollingTextView
{
    /// <summary>
    /// Defines the <see cref="Microsoft.Maui.Controls.Label.TextColor"/>
    /// </summary>
    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    /// <summary>
    /// Text to be displayed
    /// </summary>
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    /// <summary>
    /// Sets the color of the fading box
    /// </summary>
    /// <remarks>Should be same color as the container <see cref="AutoScrollingTextView"/> is in</remarks>
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

    /// <summary>
    /// Whether the text at the top should fade out
    /// </summary>
    public bool ShouldFadeOut
    {
        get => (bool)GetValue(ShouldFadeOutProperty);
        set => SetValue(ShouldFadeOutProperty, value);
    }

    public static readonly BindableProperty ShouldFadeOutProperty = BindableProperty.Create(
        nameof(ShouldFadeOut),
        typeof(bool),
        typeof(AutoScrollingTextView),
        true,
        defaultBindingMode: BindingMode.OneTime);

    public static readonly new BindableProperty StyleProperty = BindableProperty.Create(
        nameof(Style),
        typeof(Style),
        typeof(AutoScrollingTextView),
        defaultValue: Label.DefaultLabelStyle,
        defaultBindingMode: BindingMode.OneTime);

    public static readonly BindableProperty FadeColorProperty = BindableProperty.Create(
        nameof(FadeColor),
        typeof(Color),
        typeof(AutoScrollingTextView),
        defaultBindingMode: BindingMode.OneTime,
        propertyChanged: (bindable, _, _) =>
        {
            if (bindable is AutoScrollingTextView autoScrollingText)
            {
                autoScrollingText.SetFadingBoxFade();
            }
        });

    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        nameof(Text),
        typeof(string),
        typeof(AutoScrollingTextView),
        propertyChanged: (bindable, _, _) =>
        {
            if (bindable is not AutoScrollingTextView autoScrollingText)
                return;

            autoScrollingText.m_label.Text = autoScrollingText.Text;
        });

    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
        nameof(TextColor),
        typeof(Color),
        typeof(AutoScrollingTextView),
        Colors.GetColor(ColorName.color_text_default),
        defaultBindingMode: BindingMode.OneTime);
}