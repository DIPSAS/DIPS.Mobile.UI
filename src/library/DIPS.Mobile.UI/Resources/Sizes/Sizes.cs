namespace DIPS.Mobile.UI.Resources.Sizes;

public static class Sizes
{
    /// <summary>
    /// Get the size by <see cref="SizeName"/>
    /// </summary>
    /// <param name="sizeName">The size name to get</param>
    public static double GetSize(SizeName sizeName)
    {
        return SizesExtension.GetSize(sizeName);
    }
}