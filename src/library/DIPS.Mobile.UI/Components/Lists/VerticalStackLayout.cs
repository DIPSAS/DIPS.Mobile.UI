using DIPS.Mobile.UI.Resources.Sizes;

namespace DIPS.Mobile.UI.Components.Lists;

public class VerticalStackLayout : Microsoft.Maui.Controls.VerticalStackLayout
{
    public VerticalStackLayout()
    {
       Spacing = Sizes.GetSize(SizeName.size_1);
    }
  
}