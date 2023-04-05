using DIPS.Mobile.UI.Sizes.Sizes;

namespace DIPS.Mobile.UI.Resources.Sizes;

public partial class Sizes
{
    public Sizes()
    {
        InitializeComponent();
    }
    /// <summary>
    /// Get the size by <see cref="SizeName"/>
    /// </summary>
    /// <param name="sizeName">The size name to get</param>
    public static int GetSize(SizeName sizeName)
    {
        return SizesExtension.GetSize(sizeName);
    }
}