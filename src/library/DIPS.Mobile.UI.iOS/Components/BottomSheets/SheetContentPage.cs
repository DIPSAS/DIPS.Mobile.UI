using System;
using System.Threading.Tasks;
using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Components.Searching;
using Foundation;
using UIKit;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.iOS.Components.BottomSheets
{
    internal class SheetContentPage : ContentPage
    {
        private NSObject m_didEnterBackgroundNotificationObserver;
        private UIViewController m_viewController;
        private readonly BottomSheet m_bottomSheet;
        private UISheetPresentationController m_sheetPresentationController;

        public SheetContentPage(BottomSheet bottomSheet)
        {
            m_bottomSheet = bottomSheet;
            bottomSheet.VerticalOptions = LayoutOptions.Start;
            Content = bottomSheet;
            Parent = Application.Current;

            SetupViewController();
            SubscribeEvents();
            Padding = 0;
        }

        private void SubscribeEvents()
        {
            m_bottomSheet.WillClose += Close;
            m_didEnterBackgroundNotificationObserver = NSNotificationCenter.DefaultCenter.AddObserver(
                UIApplication.DidEnterBackgroundNotification,
                notification => { m_viewController.DismissViewController(true, null); });
        }

        private void UnSubscribeEvents()
        {
            m_bottomSheet.WillClose -= Close;
            NSNotificationCenter.DefaultCenter.RemoveObserver(m_didEnterBackgroundNotificationObserver);
        }

        private void Close(object sender, EventArgs e)
        {
            m_viewController.DismissViewController(true, null);
        }

        private void SetupViewController()
        {
            m_viewController = this.CreateViewController();
            m_viewController.ModalPresentationStyle = UIModalPresentationStyle.PageSheet;
            m_sheetPresentationController = m_viewController.SheetPresentationController;
            if (m_sheetPresentationController != null)
            {
                m_sheetPresentationController.Detents = new[]
                {
                    UISheetPresentationControllerDetent.CreateMediumDetent(),
                    UISheetPresentationControllerDetent.CreateLargeDetent()
                };
                m_sheetPresentationController.SelectedDetentIdentifier =
                    UISheetPresentationControllerDetentIdentifier.Medium;
                m_sheetPresentationController.PrefersGrabberVisible = true;
            }
        }

        internal Task Open()
        {
            var currentViewController = DUI.CurrentViewController;
            return currentViewController.PresentViewControllerAsync(m_viewController, true);
        }

        protected override void OnDisappearing()
        {
            UnSubscribeEvents();
            base.OnDisappearing();
            m_bottomSheet.SendDidClose();
        }
    }
}