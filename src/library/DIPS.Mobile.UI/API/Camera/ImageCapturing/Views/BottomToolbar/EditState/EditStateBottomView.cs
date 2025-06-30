using DIPS.Mobile.UI.API.Camera.ImageCapturing.Observers;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Button;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using HorizontalStackLayout = DIPS.Mobile.UI.Components.Lists.HorizontalStackLayout;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.BottomToolbar.EditState;

internal class EditStateBottomView : Grid
{
    public EditStateBottomView(IImageEditStateObserver imageEditStateObserver)
    {
        Padding = new Thickness(0, Sizes.GetSize(SizeName.content_margin_xsmall), 0, Sizes.GetSize(SizeName.content_margin_xsmall));
        
        this.Add(new HorizontalStackLayout
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Start,
            Spacing = Sizes.GetSize(SizeName.size_8),
            Children =
            {
                new Components.Buttons.Button
                {
                    Style = Styles.GetButtonStyle(ButtonStyle.GhostIconButtonLarge),
                    ImageSource = Icons.GetIcon(IconName.clockwise_fill),
                    ImageTintColor = Microsoft.Maui.Graphics.Colors.White,
                    Command = new Command(() => _ = imageEditStateObserver.OnRotateButtonTapped(true))
                },
                new Components.Buttons.Button
                {
                    Style = Styles.GetButtonStyle(ButtonStyle.GhostIconButtonLarge),
                    ImageSource = Icons.GetIcon(IconName.counter_clockwise_fill),
                    ImageTintColor = Microsoft.Maui.Graphics.Colors.White,
                    Command = new Command(() => _ = imageEditStateObserver.OnRotateButtonTapped(false))
                }
            }
        });
        
        this.Add(new Components.Buttons.Button
        {
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.End,
            TextColor = Microsoft.Maui.Graphics.Colors.White,
            Text = DUILocalizedStrings.Cancel,
            Style = Styles.GetButtonStyle(ButtonStyle.GhostSmall),
            Command = new Command(imageEditStateObserver.OnCancelButtonTapped)
        });
        
        this.Add(new Components.Buttons.Button
        {
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.End,
            Text = DUILocalizedStrings.Save,
            Style = Styles.GetButtonStyle(ButtonStyle.PrimarySmall),
            Command = new Command(imageEditStateObserver.OnSaveButtonTapped)
        });
        
    }
}