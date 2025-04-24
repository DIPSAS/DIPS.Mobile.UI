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

            if (collectionView.ItemsLayout is LinearItemsLayout linearItemsLayout)
            {
                collectionView.FooterTemplateChanged(linearItemsLayout.Orientation == ItemsLayoutOrientation.Horizontal);
            }
            else
            {
                collectionView.FooterTemplateChanged(true);
            }
        });

    public static readonly new BindableProperty FooterProperty = BindableProperty.Create(
        nameof(Footer),
        typeof(object),
        typeof(CollectionView),
        propertyChanged: (bindable, _, _) =>
        {
            if (bindable is not CollectionView collectionView)
                return;
            
            if (collectionView.ItemsLayout is LinearItemsLayout linearItemsLayout)
            {
                collectionView.FooterChanged(linearItemsLayout.Orientation == ItemsLayoutOrientation.Horizontal);
            }
            else
            {
                collectionView.FooterChanged(true);
            }
        });

    private void FooterChanged(bool useDefault = false)
    {
        if (!HasAdditionalSpaceAtTheEnd || useDefault || Footer is not View)
        {
            base.Footer = Footer;
            return;
        }

        base.Footer = CreateFooter(Footer as View);
    }

    public new object Footer
    {
        get => GetValue(FooterProperty);
        set => SetValue(FooterProperty, value);
    }

    private void FooterTemplateChanged(bool useDefault = false)
    {
        if (!HasAdditionalSpaceAtTheEnd || useDefault)
        {
            base.FooterTemplate = FooterTemplate;
            return;
        }
        
        base.FooterTemplate = new DataTemplateSelectorWrapper(FooterTemplate, this);
    }

    public new DataTemplate FooterTemplate
    {
        get => (DataTemplate)GetValue(FooterTemplateProperty);
        set => SetValue(FooterTemplateProperty, value);
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

    private Grid CreateFooter(View? consumerContent)
    {
        var footer = new Grid
        {
            RowDefinitions =
            [
                new RowDefinition { Height = GridLength.Star },
                new RowDefinition { Height = GridLength.Auto }
            ],
            Children = { consumerContent, CreateSpacingBox() }
        };
        
        footer.SetBinding(BindingContextProperty, static (CollectionView collectionView) => collectionView.BindingContext, source: this);

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
        base.Footer ??= BindingContext;
    }

    public BoxView? SpacingBox { get; set; }
}