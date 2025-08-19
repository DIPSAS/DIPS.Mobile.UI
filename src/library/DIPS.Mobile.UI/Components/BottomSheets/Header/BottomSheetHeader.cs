using DIPS.Mobile.UI.Components.Dividers;
using DIPS.Mobile.UI.Converters.ValueConverters;
using DIPS.Mobile.UI.Effects.Touch;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Button;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.BottomSheets.Header;

internal class BottomSheetHeader : Grid
{
    private Grid m_backButtonAndTitleLabelContainer;
    private readonly BottomSheet m_bottomSheet;

    public BottomSheetHeader(BottomSheet bottomSheet)
    {
        m_bottomSheet = bottomSheet;
        
        this.SetBinding(BindingContextProperty, static (BottomSheet bottomSheet) => bottomSheet, source: m_bottomSheet);
        this.SetBinding(IsVisibleProperty, static (BottomSheetHeaderBehavior behavior) => behavior.IsVisible, source: m_bottomSheet.BottomSheetHeaderBehavior);
        
        BackgroundColor = Colors.GetColor(ColorName.color_surface_default);
        ColumnSpacing = Sizes.GetSize(SizeName.content_margin_medium);

        var topPadding = Sizes.GetSize(SizeName.content_margin_medium);

#if __ANDROID__
        topPadding = 0;
#endif
        
        Padding = new Thickness(Sizes.GetSize(SizeName.content_margin_medium), topPadding, Sizes.GetSize(SizeName.content_margin_medium), Sizes.GetSize(SizeName.content_margin_medium));
        
        AddColumnDefinition(new ColumnDefinition(GridLength.Star));
        AddColumnDefinition(new ColumnDefinition(GridLength.Auto));

        AddBackButtonAndTitleLabel();
        AddCloseButton();
        AddDivider();
    }

    private void AddCloseButton()
    {
        var closeButton = new Button
        {
            VerticalOptions = LayoutOptions.Start, 
            Style = Styles.GetButtonStyle(ButtonStyle.CloseIconButtonSmall),
            Command = new Command(() =>
            {
                if (m_bottomSheet.BottomSheetHeaderBehavior?.CloseButtonCommand is not null)
                {
                    m_bottomSheet.BottomSheetHeaderBehavior.CloseButtonCommand.Execute(() =>
                    {
                        m_bottomSheet.Close();
                    });                
                }
                else
                {
                    m_bottomSheet.Close();
                }
            })
        };
        
        SemanticProperties.SetDescription(closeButton, DUILocalizedStrings.Close);
        
        closeButton.SetBinding(IsVisibleProperty, static (BottomSheetHeaderBehavior headerBehavior) => headerBehavior.IsCloseButtonVisible, source: m_bottomSheet.BottomSheetHeaderBehavior);
        
        this.Add(closeButton, 1);
    }

    private void AddBackButtonAndTitleLabel()
    {
        var backButton = new Image
        {
            Source = Icons.GetIcon(IconName.chevron_left_line), 
            WidthRequest = Sizes.GetSize(SizeName.size_4), 
            HeightRequest = Sizes.GetSize(SizeName.size_4),
            Margin = new Thickness(0, 0, Sizes.GetSize(SizeName.content_margin_medium), 0)
        };
        
        var titleLabel = new Label
        {
            Style = Styles.GetLabelStyle(LabelStyle.UI300),
            TextColor = Colors.GetColor(ColorName.color_text_default),
            VerticalTextAlignment = TextAlignment.Center,
            HorizontalTextAlignment = TextAlignment.Start,
            MaxLines = 2,
            LineBreakMode = LineBreakMode.TailTruncation
        };
        
        backButton.SetBinding(IsVisibleProperty, static (BottomSheetHeaderBehavior behavior) => behavior.IsBackButtonVisible, source: m_bottomSheet.BottomSheetHeaderBehavior, fallbackValue: false);
        titleLabel.SetBinding(Label.TextProperty, static (BottomSheet bottomSheet) => bottomSheet.Title);

        m_backButtonAndTitleLabelContainer = new Grid
        {
            ColumnDefinitions = [new ColumnDefinition(GridLength.Auto), new ColumnDefinition(GridLength.Star)],
            HorizontalOptions = LayoutOptions.Start
        };
        
        if (m_bottomSheet.BottomSheetHeaderBehavior?.TitleAndBackButtonContainerCommand is not null)
        {
            Touch.SetCommand(m_backButtonAndTitleLabelContainer, m_bottomSheet.BottomSheetHeaderBehavior.TitleAndBackButtonContainerCommand);
        }
        
        m_backButtonAndTitleLabelContainer.SetBinding(IsEnabledProperty, static (BottomSheetHeaderBehavior behavior) => behavior.IsTitleAndBackButtonContainerEnabled, source: m_bottomSheet.BottomSheetHeaderBehavior);
        m_backButtonAndTitleLabelContainer.Add(backButton);
        m_backButtonAndTitleLabelContainer.Add(titleLabel, 1);
        
        Add(m_backButtonAndTitleLabelContainer);
    }

    private void AddDivider()
    {
        var divider = new Divider
        {
            BackgroundColor = Colors.GetColor(ColorName.color_border_subtle),
            VerticalOptions = LayoutOptions.End,
            Margin = new Thickness(this.Padding.Left * -1, this.Padding.Top * -1, this.Padding.Right * -1, this.Padding.Bottom * -1)
        };
        
        divider.SetBinding(IsVisibleProperty, static (BottomSheet bottomSheet) => bottomSheet.Title, converter: new IsEmptyConverter
        {
            Inverted = true
        });
        
        this.SetColumnSpan(divider, 2);
        Add(divider);
    }
}