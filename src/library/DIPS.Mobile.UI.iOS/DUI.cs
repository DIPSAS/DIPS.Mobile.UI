using System;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.iOS.Components.BottomSheets;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.iOS;

namespace DIPS.Mobile.UI.iOS
{
    public static class DUI
    {

        public static void Init()
        {
            BottomSheetService.Instance = new iOSBottomSheetService();
        }

        /// <summary>
        /// Gets the <see cref="Context"/>.
        /// </summary>
        internal static UIViewController CurrentViewController
        {
            get
            {
                var window = UIApplication.SharedApplication.KeyWindow;

                var currentViewController = window?.RootViewController;

                while (currentViewController?.PresentedViewController != null)
                {
                    currentViewController = currentViewController.PresentedViewController;
                }

                return currentViewController;
            }
        }
    }
}