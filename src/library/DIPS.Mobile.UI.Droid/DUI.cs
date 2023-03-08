using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using AndroidX.AppCompat.App;
using AndroidX.Core.SplashScreen;
using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Droid.Components.BottomSheets;
using DIPS.Mobile.UI.Resources.Colors;
using Xamarin.Forms.Platform.Android;
using Application = Xamarin.Forms.Application;

namespace DIPS.Mobile.UI.Droid
{
    public class DUI
    {
        private static Context s_context;

        /// <summary>
        /// Gets the <see cref="Context"/>.
        /// </summary>
        internal static Context Context
        {
            get
            {
                var page = Application.Current.MainPage ??
                           throw new NullReferenceException($"{nameof(Application.MainPage)} cannot be null");
                var renderer = page.GetRenderer();

                if (renderer?.View.Context is not null)
                    s_context = renderer.View.Context;

                return renderer?.View.Context ??
                       s_context ?? throw new NullReferenceException($"{nameof(Context)} cannot be null");
            }
        }

        /// <summary>
        /// Return a resource identifier for the given resource name. A fully qualified resource name is of the form "package:type/entry". The first two components (package and type) are optional if defType and defPackage, respectively, are specified here.
        /// </summary>
        /// <param name="name">The name of the desired resource.</param>
        /// <param name="defType">Optional default resource type to find, if "type/" is not included in the name. Can be null to require an explicit type.</param>
        /// <param name="defPackage">Optional default package to find, if "package:" is not included in the name. Can be null to require an explicit package.</param>
        /// <returns></returns>
        /// <remarks>Taken from here https://developer.android.com/reference/android/content/res/Resources#getIdentifier(java.lang.String,%20java.lang.String,%20java.lang.String)</remarks>
        internal static int? GetResourceId(string name, string? defType = null, string? defPackage = null)
        {
            var id = Context.Resources?.GetIdentifier(name, defType, Context.PackageName);
            return id > 0 ? id : null;
        }

        public static void Init(Activity activity, bool supportDarkMode = true)
        {
            SplashScreen.InstallSplashScreen(activity);
            UI.DUI.Library = new AndroidLibraryService();
        }

    }
}