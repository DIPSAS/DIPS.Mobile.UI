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
                if (UIDevice.CurrentDevice.CheckSystemVersion(15, 0)) //Can use bottom sheet
                {
                    if (m_viewController.SheetPresentationController != null)
                    {
                        m_sheetPresentationController = m_viewController.SheetPresentationController;
                        
                        
                        var prefferedDetent = UISheetPresentationControllerDetent.CreateMediumDetent();
                        var prefferedDetentIdentifier = UISheetPresentationControllerDetentIdentifier.Medium;

                        if (m_bottomSheet.ShouldFitToContent)
                        {
                            if (UIDevice.CurrentDevice.CheckSystemVersion(16, 0)) //Can fit to content by setting a custom detent
                            {
                                prefferedDetent = UISheetPresentationControllerDetent.Create("prefferedDetent",
                                    _ =>
                                    {
                                        return (nfloat)(m_bottomSheet.Bounds.Height + Padding.Top+Padding.Bottom);
                                    });
                                prefferedDetentIdentifier = UISheetPresentationControllerDetentIdentifier.Unknown;    
                            }
                            else if (UIDevice.CurrentDevice.CheckSystemVersion(15, 0)) //Select large detent if the content is bigger than medium detent
                            {
                                if (Content.Height > Height / 2) //if the content is larger than half the screen when the detent is medium, it means that something is outside of bounds
                                {
                       
                                    prefferedDetent = UISheetPresentationControllerDetent.CreateLargeDetent();
                                    prefferedDetentIdentifier = UISheetPresentationControllerDetentIdentifier.Large;
                                }
                            }
                        }


                        m_sheetPresentationController.Detents = (m_bottomSheet.ShouldFitToContent)
                            ? new[] {prefferedDetent}
                            : new[] {prefferedDetent, UISheetPresentationControllerDetent.CreateLargeDetent(),};

                        //Add grabber
                        m_sheetPresentationController.PrefersGrabberVisible = true;

                        m_sheetPresentationController.SelectedDetentIdentifier = prefferedDetentIdentifier;
                        
                        m_sheetPresentationController.PrefersScrollingExpandsWhenScrolledToEdge = true;

                        var view = m_viewController.View;
                        
                        if (view != null)
                        {
                            var bottom = UIApplication.SharedApplication.KeyWindow?.SafeAreaInsets.Bottom != 0 ? 4 : 16; //TODO: Use DesignSystem
                            Padding = new Thickness(0, 12, 0, bottom); //Respect grabber and make sure we add some padding to the bottom, depending on if Safe Area (non physical home button) is visible.
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