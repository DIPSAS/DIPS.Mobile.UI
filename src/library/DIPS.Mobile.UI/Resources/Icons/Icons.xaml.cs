
using DIPS.Mobile.UI.Resources.Colors;

namespace DIPS.Mobile.UI.Resources.Icons
{
    public partial class Icons
    {
        public Icons()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Get the icon value from a <see cref="IconName"/>
        /// </summary>
        /// <param name="iconName">The name of the color to get</param>
        /// <returns><see cref="string"/></returns>
        public static ImageSource GetIcon(IconName iconName) => IconLookup.GetIcon(iconName);
        public static string GetIconName(IconName iconName) => iconName.ToString();
    }
}