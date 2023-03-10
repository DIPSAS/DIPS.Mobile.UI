using System;
using System.Threading.Tasks;
using DIPS.Mobile.UI.Components.BottomSheets;
using UIKit;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.iOS.Components.BottomSheets
{
    internal class BottomSheetContentPage : ContentPage
    {
        private UIViewController? m_viewController;
        private readonly BottomSheet m_bottomSheet;
        private UISheetPresentationController? m_sheetPresentationController;

        public BottomSheetContentPage(BottomSheet bottomSheet)
        {
            m_bottomSheet = bottomSheet;
            bottomSheet.VerticalOptions = LayoutOptions.Start;
            Content = bottomSheet;
            Parent = Application.Current;

            SetupViewController();
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            m_bottomSheet.WillClose += Close;
        }

        private void UnSubscribeEvents()
        {
            m_bottomSheet.WillClose -= Close;
        }

        private void Close(object sender, EventArgs e)
        {
            m_viewController?.DismissViewController(true, null);
        }

        private void SetupViewController()
        {
            var control = Xamarin.Forms.Platform.iOS.Platform.CreateRenderer(this);
            if (control != null)
            {
                Xamarin.Forms.Platform.iOS.Platform.SetRenderer(this, control);
                var screenWidth = UIScreen.MainScreen.Bounds.Width;
                var screenHeight = UIScreen.MainScreen.Bounds.Height;
                control.SetElementSize(control.GetDesiredSize(screenWidth, screenHeight).Request);
                m_viewController = control.ViewController;

                m_viewController.RestorationIdentifier = iOSBottomSheetService.BottomSheetRestorationIdentifier;
                m_viewController.ModalPresentationStyle = UIModalPresentationStyle.PageSheet;
                if (UIDevice.CurrentDevice.CheckSystemVersion(15, 0))
                {
                    if (m_viewController.SheetPresentationController != null)
                    {
                        m_sheetPresentationController = m_viewController.SheetPresentationController;
                        if (m_sheetPresentationController != null)
                        {
                            if (m_bottomSheet.IsDraggable)
                            {
                                m_sheetPresentationController.Detents = new[]
                                {
                                    UISheetPresentationControllerDetent.CreateMediumDetent(),
                                    UISheetPresentationControllerDetent.CreateLargeDetent()
                                };
                                m_sheetPresentationController.SelectedDetentIdentifier =
                                    UISheetPresentationControllerDetentIdentifier.Medium;
                            }
                            else
                            {
                                m_sheetPresentationController.Detents = new[]
                                {
                                    UISheetPresentationControllerDetent.CreateMediumDetent(),
                                };
                            }

                            m_sheetPresentationController.PrefersGrabberVisible =
                                m_sheetPresentationController.Detents.Length > 1;
                            m_sheetPresentationController.PrefersScrollingExpandsWhenScrolledToEdge = true;

                            if (m_sheetPresentationController.PrefersGrabberVisible)
                            {
                                //Move top down 10 pixels to make space for the grabber
                                Padding = new Thickness(0, 20, 0, 0);
                            }
                        }
                    }
                }
            }
        }

        internal async Task Open()
        {
            var currentViewController = DUI.CurrentViewController;
            if (m_viewController != null)
            {
                await currentViewController.PresentViewControllerAsync(m_viewController, true);
            }
        }

        protected override void OnDisappearing()
        {
            UnSubscribeEvents();
            base.OnDisappearing();
            m_bottomSheet.SendDidClose();
        }
    }
}