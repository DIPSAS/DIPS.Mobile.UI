using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Loading.Skeleton;

public partial class SkeletonView
{
    /// <summary>
    /// Time used to fade in and out content
    /// </summary>
    public uint FadeTime
    {
        get => (uint)GetValue(FadeTimeProperty);
        set => SetValue(FadeTimeProperty, value);
        
    }

    public static readonly BindableProperty FadeTimeProperty = BindableProperty.Create(
        nameof(FadeTime),
        typeof(uint),
        typeof(SkeletonView), defaultValue:(uint)400);


    public static readonly BindableProperty SkeletonColorProperty = BindableProperty.Create(
        nameof(SkeletonColor),
        typeof(Color),
        typeof(SkeletonView), defaultValueCreator:(_ => Colors.GetColor(ColorName.color_background_subtle)));
    
    /// <summary>
    /// Color used on skeletons. Defaults to LightGray
    /// </summary>
    public Color SkeletonColor
    {
        get => (Color)GetValue(SkeletonColorProperty);
        set => SetValue(SkeletonColorProperty, value);
    }

    /// <summary>
    /// Content shown when loading is done
    /// </summary>
    public View? MainContent
    {
        get => m_mainContent;
        set
        {
            m_mainContent = value;
            OnChanged();
        }
    }

    /// <summary>
    /// Shapes used when content is loading
    /// </summary>
    public List<SkeletonShape> Shapes { get; set; } = new();

    /// <summary>
    ///  <see cref="IsLoading" />
    /// </summary>
    public static readonly BindableProperty IsLoadingProperty = BindableProperty.Create(
        nameof(IsLoading),
        typeof(bool),
        typeof(SkeletonView),
        false,
        propertyChanged: (s, _, _) => ((SkeletonView)s).OnLoadingChanged());

    /// <summary>
    /// Used to select the template in the selector
    /// </summary>
    public bool IsLoading
    {
        get => (bool)GetValue(IsLoadingProperty);
        set => SetValue(IsLoadingProperty, value);
    }
}
