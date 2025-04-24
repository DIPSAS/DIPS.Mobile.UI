using Microsoft.Maui.Controls.Internals;
using Colors = Microsoft.Maui.Graphics.Colors;

namespace DIPS.Mobile.UI.Components.Lists;

public partial class CollectionView
{
    public static new readonly BindableProperty FooterTemplateProperty = BindableProperty.Create(
        nameof(ItemTemplate),
        typeof(DataTemplate),
        typeof(CollectionView),
        propertyChanged: (bindable, _, _) =>
        {
            if (bindable is not CollectionView collectionView)
                return;

            collectionView.FooterTemplateChanged();
        });
    
    public new DataTemplate? FooterTemplate
    {
        get => (DataTemplate?)GetValue(FooterTemplateProperty);
        set => SetValue(FooterTemplateProperty, value);
    }

    public static readonly new BindableProperty FooterProperty = BindableProperty.Create(
        nameof(Footer),
        typeof(object),
        typeof(CollectionView),
        propertyChanged: (bindable, _, _) =>
        {
            if (bindable is not CollectionView collectionView)
                return;
            
            collectionView.FooterChanged();
        });

    public new object? Footer
    {
        get => GetValue(FooterProperty);
        set => SetValue(FooterProperty, value);
    }

    private void FooterTemplateChanged()
    {
        if (!ShouldHaveAdditionalSpaceAtTheEnd)
        {
            base.FooterTemplate = FooterTemplate;
            return;
        }
        
        base.FooterTemplate = new DataTemplateSelectorWrapper(FooterTemplate!, this);
    }
    
    private void FooterChanged()
    {
        if (!ShouldHaveAdditionalSpaceAtTheEnd || Footer is not View)
        {
            base.Footer = Footer;
            return;
        }

        base.Footer = CreateFooter(Footer as View);
    }

    private class DataTemplateSelectorWrapper(DataTemplate dataTemplate, CollectionView collectionView) : DataTemplateSelector
    {
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            // Run this through the extension method in case consumers' FooterTemplate is really a DataTemplateSelector
            var itemTemplate = dataTemplate.SelectDataTemplate(item, container);

            var consumerContent = itemTemplate.CreateContent() as View;

            // Here we wrap the consumer content in a grid to add extra spacing at bottom
            return new DataTemplate(() => collectionView.CreateFooter(consumerContent));
        }
    }

    private Grid CreateFooter(View? consumerContent = null)
    {
        var rowDefinitions = consumerContent is not null ? 
            new RowDefinitionCollection
            {
                new RowDefinition { Height = GridLength.Star },
                new RowDefinition { Height = GridLength.Auto }
            } :
            new RowDefinitionCollection
            {
                new RowDefinition { Height = GridLength.Auto }
            };
        
        var footer = new Grid
        {
            RowDefinitions = rowDefinitions,
            Children = { consumerContent, CreateSpacingBox() }
        };
        
        footer.SetBinding(BindingContextProperty, static (CollectionView collectionView) => collectionView.Footer, source: this);

        return footer;
    }
    
    private BoxView CreateSpacingBox()
    {
        SpacingBox ??= new BoxView { BackgroundColor = Colors.Transparent, InputTransparent = true };
        Grid.SetRow(SpacingBox, 1);
        return SpacingBox;
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        
        if (SpacingBox is not null)
        {
            SpacingBox.HeightRequest = height / 2;
        }
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        
        // FooterTemplate wont show if Footer is not set, because we use DataTemplateSelector to wrap the consumer content to add additional space at the end
        // https://github.com/dotnet/maui/blob/main/src/Controls/src/Core/Handlers/Items/iOS/TemplateHelpers.cs#L28
        if(!ShouldHaveAdditionalSpaceAtTheEnd)
            return;
        
        base.Footer ??= BindingContext;
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        
        if(Handler is null || !ShouldHaveAdditionalSpaceAtTheEnd)
            return;

        if (base.FooterTemplate is null)
        {
            var footer = CreateFooter();
            base.FooterTemplate = new DataTemplate(() => footer);
        }
    }

    private bool ShouldHaveAdditionalSpaceAtTheEnd => ItemsLayout is LinearItemsLayout { Orientation: ItemsLayoutOrientation.Vertical } && HasAdditionalSpaceAtTheEnd;
    
    public BoxView? SpacingBox { get; set; }
}