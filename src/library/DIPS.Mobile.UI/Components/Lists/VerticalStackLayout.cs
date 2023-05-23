using DIPS.Mobile.UI.Sizes.Sizes;

namespace DIPS.Mobile.UI.Components.Lists;

public class VerticalStackLayout : Microsoft.Maui.Controls.VerticalStackLayout
{
    public VerticalStackLayout()
    {
        Spacing = UI.Resources.Sizes.Sizes.GetSize(SizeName.size_1);
    }
}