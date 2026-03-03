namespace DIPS.Mobile.UI.Components.Toolbar;

public partial class Toolbar
{
    partial void Init()
    {
        // M3 Bottom App Bar spec: height = 80dp
        HeightRequest = Sizes.GetSize(SizeName.size_20);
    }
}
