using System;
using Android.Content;
using DIPS.Mobile.UI.Resources.Colors;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

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
                var page = Application.Current.MainPage ?? throw new NullReferenceException($"{nameof(Application.MainPage)} cannot be null");
                var renderer = page.GetRenderer();

                if (renderer?.View.Context is not null)
                    s_context = renderer.View.Context;

                return renderer?.View.Context ?? s_context ?? throw new NullReferenceException($"{nameof(Context)} cannot be null");
            }
        }
        
        public static void Init(){}
    }
}