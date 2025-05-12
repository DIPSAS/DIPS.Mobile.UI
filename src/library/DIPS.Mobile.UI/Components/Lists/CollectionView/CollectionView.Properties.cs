namespace DIPS.Mobile.UI.Components.Lists;

public partial class CollectionView
{
    /// <summary>
    /// Adds spacing between the items in the collection view.
    /// </summary>
    /// <remarks>When the <see cref="ItemsLayout"/> of the <see cref="CollectionView"/> is set to <see cref="GridItemsLayout"/> it will set both the horizontal and vertical item spacing.</remarks>
    public double ItemSpacing
    {
        get => (double)GetValue(ItemSpacingProperty);
        set => SetValue(ItemSpacingProperty, value);
    }

    /// <summary>
    /// Will automatically set the corner radius of the first and last item in the <see cref="CollectionView"/>.
    /// </summary>
    internal bool AutoCornerRadius { get; set; } = true;
    
    /// <summary>
    /// Adds additional space at the end of the list so people can more easily see and tap the last items.
    /// </summary>
    /// <remarks>Default value is true</remarks>
    public bool HasAdditionalSpaceAtTheEnd
    {
        get => (bool)GetValue(HasAdditionalSizeAtTheEndProperty);
        set => SetValue(HasAdditionalSizeAtTheEndProperty, value);
    }
    
    /// <summary>
    /// Determines if input fields should be unfocused when the user scrolls the <see cref="CollectionView"/>. (ScrollBar, Editor etc..) 
    /// </summary>
    public bool RemoveFocusOnScroll { get; init; }

    public static readonly BindableProperty ShouldBounceProperty = BindableProperty.Create(
        nameof(ShouldBounce),
        typeof(bool),
        typeof(CollectionView),
        defaultValue: true);

    /// <summary>
    /// Determines if the collection view should bounce.
    /// </summary>
    public bool ShouldBounce
    {
        get => (bool)GetValue(ShouldBounceProperty);
        set => SetValue(ShouldBounceProperty, value);
    }
    
    public CollapsibleElement? CollapsibleElement
    {
        get => (CollapsibleElement?)GetValue(CollapsibleElementProperty);
        set => SetValue(CollapsibleElementProperty, value);
    }

    /// <summary>
    /// Margin for the content inside the CollectionView, using this will not affect for example scroll bar, like default margin does
    /// </summary>
    /// <remarks>Default value is HorizontalThickness: size_3<br/> Left and right must be uniform. <br/><b>NB:</b> Top padding not implemented yet</remarks>
    public Thickness Padding { get; set; } = new(Sizes.GetSize(SizeName.content_margin_medium), 0);
    
    public CornerRadius FirstItemCornerRadius { get; init; }
    public CornerRadius LastItemCornerRadius { get; init; }
    
    public static readonly BindableProperty HasAdditionalSizeAtTheEndProperty = BindableProperty.Create(
        nameof(HasAdditionalSpaceAtTheEnd),
        typeof(bool),
        typeof(CollectionView), defaultValue:true);
    
    public static readonly BindableProperty ItemSpacingProperty = BindableProperty.Create(
        nameof(ItemSpacing),
        typeof(double),
        typeof(CollectionView), propertyChanged: (bindable, _, _) => ((CollectionView)bindable).TrySetItemSpacing(), defaultValue:(double)Sizes.GetSize(SizeName.size_1));

    public static readonly BindableProperty CollapsibleElementProperty = BindableProperty.Create(
        nameof(CollapsibleElement),
        typeof(CollapsibleElement),
        typeof(CollectionView),
        defaultBindingMode: BindingMode.OneTime);
    
    /// <summary>
    /// Reloads all the data in the <see cref="CollectionView"/>
    /// </summary>
    /// <remarks>Use this if you need to re draw the items.</remarks>
    public void ReloadData()
    {
        if (Handler is CollectionViewHandler handler)
        {
            handler.ReloadData(handler);
        }

#if __IOS__
        if(Handler is CollectionView2Handler handler2)
            handler2.ReloadData(handler2);
#endif
    }
}

public class CollapsibleElement : BindableObject
{
    public static readonly BindableProperty ElementProperty = BindableProperty.Create(
        nameof(Element),
        typeof(VisualElement),
        typeof(CollapsibleElement), 
        defaultBindingMode: BindingMode.OneTime);

    public static readonly BindableProperty InputTransparentOnCollapseProperty = BindableProperty.Create(
        nameof(InputTransparentOnCollapse),
        typeof(bool),
        typeof(CollapsibleElement),
        defaultBindingMode: BindingMode.OneTime);

    public static readonly BindableProperty ShouldFadeOutProperty = BindableProperty.Create(
        nameof(ShouldFadeOut),
        typeof(bool),
        typeof(CollapsibleElement), 
        defaultValue: true);

    public static readonly BindableProperty FadeOutThresholdProperty = BindableProperty.Create(
        nameof(FadeOutThreshold),
        typeof(float),
        typeof(CollapsibleElement),
        defaultValue: 0.5f);

    public static readonly BindableProperty OffsetThresholdProperty = BindableProperty.Create(
        nameof(OffsetThreshold),
        typeof(float),
        typeof(CollapsibleElement),
        defaultValue: 50f);

    /// <summary>
    /// When the element should begin to be collapsed, 50 is default
    /// <remarks>A value of 50 seems to resolve some issues if CollectionView is wrapped with RefreshView</remarks>
    /// </summary>
    public float OffsetThreshold
    {
        get => (float)GetValue(OffsetThresholdProperty);
        set => SetValue(OffsetThresholdProperty, value);
    }
    
    /// <summary>
    /// The threshold for when the element should be completely faded out
    /// A threshold of 0.5 means that the element will be completely faded out when it is at 50% of its original height
    /// </summary>
    public float FadeOutThreshold
    {
        get => (float)GetValue(FadeOutThresholdProperty);
        set => SetValue(FadeOutThresholdProperty, value);
    }

    /// <summary>
    /// When collapsing, the element will fade out if this is set to true.
    /// </summary>
    public bool ShouldFadeOut
    {
        get => (bool)GetValue(ShouldFadeOutProperty);
        set => SetValue(ShouldFadeOutProperty, value);
    }
    
    /// <summary>
    /// When collapsing, the element will be set to InputTransparent if this is set to true.
    /// <remarks>InputTransparency will be set at the very start of the collapse</remarks>
    /// </summary>
    public bool InputTransparentOnCollapse
    {
        get => (bool)GetValue(InputTransparentOnCollapseProperty);
        set => SetValue(InputTransparentOnCollapseProperty, value);
    }
    
    /// <summary>
    /// The element that will be collapsed
    /// </summary>
    public VisualElement? Element
    {
        get => (VisualElement?)GetValue(ElementProperty);
        set => SetValue(ElementProperty, value);
    }

    /// <summary>
    /// For some reason, the ScaleY property is not set on Android when scrolling fast, so we set it manually in the platform
    /// </summary>
    internal void SetScaleY(double scaleY)
    {
#if __ANDROID__

        if (Element?.Handler?.PlatformView is global::Android.Views.View view)
        {
            view.ScaleY = (float)scaleY;
        }
#else
        Element!.ScaleY = scaleY;
#endif
    }
    
    internal double? OriginalHeight { get; private set; }

    internal void TryFade()
    {
        if (!ShouldFadeOut || OriginalHeight is null)
        {
            return;
        }

        var opacity = (Element!.HeightRequest / OriginalHeight.Value - FadeOutThreshold) / FadeOutThreshold;
        Element.Opacity = Math.Clamp(opacity, 0, 1);
    }

    /// <summary>
    /// Try adjust the scale of the element based on its height relative to its original height
    /// </summary>
    internal void TryScale()
    {
        if (OriginalHeight is not null)
        {
            var scaleY = Element!.HeightRequest / OriginalHeight.Value;
            if (double.IsNaN(scaleY))
            {
                scaleY = 0;
            }
            SetScaleY(scaleY);
        }
    }

    internal void TrySetInputTransparent()
    {
        if (!InputTransparentOnCollapse)
            return;

        // Set InputTransparent to true if the height is less than the original height
        if (Math.Abs(Element!.Height - OriginalHeight!.Value) < 0.05f)
        {
            Element.InputTransparent = false;
        }
        else
        {
            Element.InputTransparent = true;
            if (Element is SearchBar searchBar)
            {
                searchBar.Unfocus();    
            }
            else
            {
                Element.Unfocus();
            }
        }
    }

    internal void Reset()
    {
        if(!OriginalHeight.HasValue)
            return;
        
        SetScaleY(1);
        Element!.HeightRequest = OriginalHeight.Value;
        Element.Opacity = 1;
        Element.InputTransparent = false;
    }

    internal void TryInitialize()
    {
        if (OriginalHeight is not null)
            return;

        if(Element is null)
            throw new NullReferenceException("Element is null, cannot initialize CollapsableElement. If you are binding using Source and compiled bindings on Source is activated, remember to set x:DataType to the correct type");
        
        OriginalHeight = Element.Height;
        Element.HeightRequest = OriginalHeight.Value;
    }
}