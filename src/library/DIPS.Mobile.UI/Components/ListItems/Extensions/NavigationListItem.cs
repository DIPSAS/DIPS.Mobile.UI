using DIPS.Mobile.UI.Components.ListItems.Options.Icon;
using DIPS.Mobile.UI.Components.ListItems.Options.InLineContent;
using DIPS.Mobile.UI.Components.ListItems.Options.Title;
using DIPS.Mobile.UI.Internal;
using Colors = Microsoft.Maui.Graphics.Colors;

namespace DIPS.Mobile.UI.Components.ListItems.Extensions;

[ContentProperty(nameof(InLineContent))]
public partial class NavigationListItem : ListItem
{
    private readonly Grid m_contentGrid = new()
    {
        ColumnDefinitions = new ColumnDefinitionCollection {new(GridLength.Star), new(GridLength.Auto)},
        RowDefinitions = new RowDefinitionCollection {new(GridLength.Auto)},
        VerticalOptions = LayoutOptions.Center
    };

    public NavigationListItem()
    {
        m_contentGrid.Add(
            new Image
            {
                AutomationId = "ArrowImage".ToDUIAutomationId<NavigationListItem>(),
                Source = Icons.GetIcon(IconName.arrow_right_s_line),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.End
            }, 1);

        TitleOptions = new TitleOptions()
        {
            Width = GridLength.Star, LineBreakMode = LineBreakMode.TailTruncation, MaxLines = 1
        };
        InLineContentOptions = new InLineContentOptions() {Width = GridLength.Auto};
        IconOptions = new IconOptions()
        {
            Color = DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(ColorName.color_icon_subtle)
        };
    }

    protected override void OnHandlerChanging(HandlerChangingEventArgs args)
    {
        base.OnHandlerChanging(args);
        
        if(args.NewHandler is null)
            return;
        
        if (InLineContent is null)
        {
            AddInLineContent();
        }
    }

    protected override void AddInLineContent()
    {
        var newInLineContent = InLineContent;

        m_contentGrid.Insert(0, newInLineContent);

        SetInLineContent(m_contentGrid);
    }
}