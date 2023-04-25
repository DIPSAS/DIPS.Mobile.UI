using DIPS.Mobile.UI.Resources.Colors;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Extensions
{
    public static class BindableObjectExtensions
    {
        /// <summary>
        /// Sets and changes the color of the <see cref="BindableProperty"/> based on the <see cref="ColorName"/> based on the current <see cref="AppTheme"/>
        /// </summary>
        /// <param name="self"></param>
        /// <param name="targetProperty"></param>
        /// <param name="colorName"></param>
        public static void SetAppThemeColor(this BindableObject self, BindableProperty targetProperty,
            ColorName colorName)
        {
            var light = Colors.GetColor(colorName, AppTheme.Light);
            var dark = Colors.GetColor(colorName, AppTheme.Dark);
            self.SetAppThemeColor(targetProperty, light, dark);
        }
    }
}