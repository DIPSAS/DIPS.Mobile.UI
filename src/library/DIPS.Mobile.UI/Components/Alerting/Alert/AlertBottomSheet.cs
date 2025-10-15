using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Internal;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using DIPS.Mobile.UI.Resources.Styles.Span;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;

namespace DIPS.Mobile.UI.Components.Alerting.Alert;

internal class AlertBottomSheet : BottomSheet
{
    public AlertBottomSheet(AlertView alertView)
    {
        Title = alertView.BottomSheetTitle ?? string.Empty;

        var alertContainer = new Grid
        {
            BackgroundColor = alertView.BackgroundColor, 
            ColumnDefinitions = [new ColumnDefinition(GridLength.Auto), new ColumnDefinition(GridLength.Star)],
            VerticalOptions = LayoutOptions.Start,
            ColumnSpacing = alertView.ColumnSpacing,
            Padding = alertView.Padding
        };

        var isLargeAlert = !string.IsNullOrEmpty(alertView.Description);

        var formattedString = new FormattedString
        {
            Spans =
            {
                new Span
                {
                    Text = alertView.Title, 
                    Style = isLargeAlert ? Styles.GetSpanStyle(SpanStyle.UI200) : Styles.GetSpanStyle(SpanStyle.Body200),
                }
            }
        };

        if (isLargeAlert)
        {
            formattedString.Spans.Add(new Span
            {
                Text = Environment.NewLine
            });
            
            formattedString.Spans.Add(new Span
            {
                Text = alertView.Description,
                Style = Styles.GetSpanStyle(SpanStyle.Body100)
            });
        }
        
        var titleAndDescriptionLabel = new Label
        {
            AutomationId = "TitleAndDescriptionLabel".ToDUIAutomationId<AlertView>(),
            Style = Styles.GetLabelStyle(LabelStyle.Body200),
            FormattedText = formattedString,
            VerticalTextAlignment = TextAlignment.Start
        };
        
        var icon = new Image
        {
            Source = alertView.Icon,
            TintColor = alertView.IconColor,
            HeightRequest = Sizes.GetSize(SizeName.size_6),
            WidthRequest = Sizes.GetSize(SizeName.size_6),
            VerticalOptions = LayoutOptions.Start
        };
        
        alertContainer.Add(icon);
        alertContainer.Add(titleAndDescriptionLabel, 1);
        
        UI.Effects.Layout.Layout.SetCornerRadius(alertContainer, Sizes.GetSize(SizeName.radius_small));
        UI.Effects.Layout.Layout.SetStroke(alertContainer, alertView.Stroke);

        Content = new ScrollView
        {
            Content = alertContainer,
            Padding = Sizes.GetSize(SizeName.size_3)
        };
    }
}

