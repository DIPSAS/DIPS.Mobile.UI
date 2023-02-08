using System;
using System.Runtime.InteropServices;
using DIPS.Mobile.UI.Extensions;
using DIPS.Mobile.UI.Resources.Colors;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace DIPS.Mobile.UI.Components.Pages
{
    public partial class ContentPage : Xamarin.Forms.ContentPage
    {
        public static readonly ColorName BackgroundColorName = ColorName.color_neutral_30;

        public ContentPage()
        {
            this.SetAppThemeColor(BackgroundProperty, BackgroundColorName);
        }

        internal void SendOnContentAppearing()
        {
            OnContentAppearing();
            ContentAppearing?.Invoke(this, EventArgs.Empty);
        }
        
        internal bool SendViewSafeAreaInsetsDidChange(Thickness thickness)
        {
            SafeAreaInsetsDidChange(thickness);
            return false;
        }
    }
}