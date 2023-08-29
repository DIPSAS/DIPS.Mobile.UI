using DIPS.Mobile.UI.Converters.ValueConverters;
using Colors = Microsoft.Maui.Graphics.Colors;
using ImageButton = DIPS.Mobile.UI.Components.Images.ImageButton.ImageButton;

namespace DIPS.Mobile.UI.Components.Selection;

public partial class Checkmark : ImageButton, ISelectable
{
    public Checkmark()
    {
        IsSelected = false;
        Command = new Command(() =>
        {
            IsSelected = !IsSelected;
        });
        HeightRequest = Sizes.GetSize(SizeName.size_6);
        WidthRequest = Sizes.GetSize(SizeName.size_6);
#if __ANDROID__
        TintColor = ISelectable.s_tintColor;
#elif __IOS__
        Source = Icons.GetIcon(IconName.check_line);
#endif
        OnIsSelectedChanged();
    }

    private void OnIsSelectedChanged()
    {
#if __ANDROID__
        Source = Icons.GetIcon(IsSelected ? IconName.checkbox_checked_fill : IconName.checkbox_unchecked_line);
#elif __IOS__
        TintColor = IsSelected ? ISelectable.s_tintColor : Colors.Transparent;
#endif
    }
}