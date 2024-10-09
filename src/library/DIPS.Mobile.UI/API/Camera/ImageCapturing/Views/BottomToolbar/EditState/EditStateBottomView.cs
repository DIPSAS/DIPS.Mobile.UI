using DIPS.Mobile.UI.API.Camera.ImageCapturing.Observers;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Button;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using HorizontalStackLayout = DIPS.Mobile.UI.Components.Lists.HorizontalStackLayout;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.BottomToolbar.EditState;

internal class EditStateBottomView : Grid
{
    public EditStateBottomView(IEditStateObserver editStateObserver)
    {
        Add(new Button
        {
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.End,
            Text = DUILocalizedStrings.Cancel,
            Style = Styles.GetButtonStyle(ButtonStyle.GhostSmall),
            Command = new Command(editStateObserver.OnCancelButtonTapped)
        });
        
        Add(new Button
        {
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.End,
            Text = DUILocalizedStrings.Save,
            Style = Styles.GetButtonStyle(ButtonStyle.PrimarySmall),
            Command = new Command(editStateObserver.OnSaveButtonTapped)
        });

        Add(new HorizontalStackLayout
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Start,
            Spacing = Sizes.GetSize(SizeName.size_8),
            Padding = new Thickness(0, Sizes.GetSize(SizeName.size_4), 0, 0),
            Children =
            {
                new Components.Buttons.Button
                {
                    Style = Styles.GetButtonStyle(ButtonStyle.GhostIconButtonLarge),
                    ImageSource = Icons.GetIcon(IconName.clockwise_fill),
                    ImageTintColor = Colors.GetColor(ColorName.color_system_white),
                    Command = new Command(() => _ = editStateObserver.OnRotateButtonTapped(true))
                },
                new Components.Buttons.Button
                {
                    Style = Styles.GetButtonStyle(ButtonStyle.GhostIconButtonLarge),
                    ImageSource = Icons.GetIcon(IconName.counter_clockwise_fill),
                    ImageTintColor = Colors.GetColor(ColorName.color_system_white),
                    Command = new Command(() => _ = editStateObserver.OnRotateButtonTapped(false))
                }
            }
        });
        
    }
}