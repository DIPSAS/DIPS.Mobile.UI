using System;
using DIPS.Mobile.UI.Components.BottomSheet;
using Foundation;
using UIKit;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.iOS.Components.BottomSheet
{
    public class SheetContentPage : ContentPage
    {
        private readonly Action m_closeAction;
        private NSObject m_didEnterBackgroundNotificationObserver;

        public SheetContentPage(BottomSheetView bottomSheetView, Action closeAction)
        {
            m_closeAction = closeAction;
            Content = bottomSheetView;
            Parent = Application.Current;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            m_didEnterBackgroundNotificationObserver = NSNotificationCenter.DefaultCenter.AddObserver(UIApplication.DidEnterBackgroundNotification, notification =>
            {
                m_closeAction.Invoke();
            });
        }

        protected override void OnDisappearing()
        {
            NSNotificationCenter.DefaultCenter.RemoveObserver(m_didEnterBackgroundNotificationObserver);
            base.OnDisappearing();
        }
    }
}