using System.ComponentModel;
using DIPS.Mobile.UI.Components.Images.Image;
using DIPS.Mobile.UI.Components.ListItems.Options.Icon;
using DIPS.Mobile.UI.Components.ListItems.Options.InLineContent;
using DIPS.Mobile.UI.Components.ListItems.Options.Title;
using DIPS.Mobile.UI.Internal;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;

namespace DIPS.Mobile.UI.Components.ListItems.Extensions;

[ContentProperty(nameof(InLineContent))]
public partial class NavigationListItem : ListItem
{
    private string? m_defaultAccessibilityDescription;
    private bool m_hasCustomAccessibilityDescription;
    private bool m_isSettingDefaultAccessibilityDescription;

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
                HorizontalOptions = LayoutOptions.End,
                TintColor = Colors.GetColor(ColorName.color_icon_subtle),
            }, 1);

        TitleOptions = new TitleOptions()
        {
            Width = GridLength.Star, LineBreakMode = LineBreakMode.TailTruncation, MaxLines = 1
        };
        InLineContentOptions = new InLineContentOptions() {Width = GridLength.Auto};

        PropertyChanged += OnNavigationListItemPropertyChanged;
    }

    private void OnNavigationListItemPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == TitleProperty.PropertyName)
        {
            UpdateDefaultAccessibilityDescription();
            return;
        }

        if (e.PropertyName == SemanticProperties.DescriptionProperty.PropertyName && !m_isSettingDefaultAccessibilityDescription)
        {
            m_hasCustomAccessibilityDescription = SemanticProperties.GetDescription(this) != m_defaultAccessibilityDescription;
        }
    }

    private void UpdateDefaultAccessibilityDescription()
    {
        if (m_hasCustomAccessibilityDescription)
            return;

        var currentAccessibilityDescription = SemanticProperties.GetDescription(this);
        if (!string.IsNullOrEmpty(currentAccessibilityDescription) && currentAccessibilityDescription != m_defaultAccessibilityDescription)
        {
            m_hasCustomAccessibilityDescription = true;
            return;
        }

        m_defaultAccessibilityDescription = Title;
        m_isSettingDefaultAccessibilityDescription = true;

        try
        {
            SemanticProperties.SetDescription(this, m_defaultAccessibilityDescription);
        }
        finally
        {
            m_isSettingDefaultAccessibilityDescription = false;
        }
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