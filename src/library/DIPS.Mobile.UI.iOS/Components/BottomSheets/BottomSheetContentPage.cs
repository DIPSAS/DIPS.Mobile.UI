using System;
using System.ComponentModel;
using System.Linq;
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

        private void TrySetBottomSheetToFitToContent()
        {
            
            if (!m_bottomSheet.ShouldFitToContent || UIDevice.CurrentDevice.CheckSystemVersion(16, 0)) //iOS version < 16.0 and bottom sheet should not fit to content
            {
                return;
            }


            if (UIDevice.CurrentDevice.CheckSystemVersion(15, 0))
            {
                if (m_sheetPresentationController is {SelectedDetentIdentifier: UISheetPresentationControllerDetentIdentifier.Medium})
                {
                    if (Content.Height > Height / 2) //if the content is larger than half the screen when the detent is medium, it means that something is outside of bounds
                    {
                       
                        m_sheetPresentationController.SelectedDetentIdentifier =
                            UISheetPresentationControllerDetentIdentifier.Large; //Go full screen
                    }
                }
            }
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
                TrySetBottomSheetToFitToContent();
                m_viewController = control.ViewController;

                m_viewController.RestorationIdentifier = iOSBottomSheetService.BottomSheetRestorationIdentifier;
                m_viewController.ModalPresentationStyle = UIModalPresentationStyle.PageSheet;
                if (UIDevice.CurrentDevice.CheckSystemVersion(15, 0))
                {
                    if (m_viewController.SheetPresentationController != null)
                    {
                        m_sheetPresentationController = m_viewController.SheetPresentationController;
                        
                        
                        var prefferedDetent = UISheetPresentationControllerDetent.CreateMediumDetent();
                        var prefferedDetentIdentifier = UISheetPresentationControllerDetentIdentifier.Medium;

                        if (m_bottomSheet.ShouldFitToContent)
                        {
                            if (UIDevice.CurrentDevice.CheckSystemVersion(16, 0))
                            {
                                prefferedDetent = UISheetPresentationControllerDetent.Create("prefferedDetent",
                                    _ =>
                                    {
                                        return (nfloat)m_bottomSheet.Bounds.Height;
                                    });
                                prefferedDetentIdentifier = UISheetPresentationControllerDetentIdentifier.Unknown;    
                            }
                            else if (UIDevice.CurrentDevice.CheckSystemVersion(15, 0))
                            {
                                if (Content.Height > Height / 2) //if the content is larger than half the screen when the detent is medium, it means that something is outside of bounds
                                {
                       
                                    prefferedDetent = UISheetPresentationControllerDetent.CreateLargeDetent();
                                    prefferedDetentIdentifier = UISheetPresentationControllerDetentIdentifier.Large;
                                }
                            }
                        }
                        
                        m_sheetPresentationController.Detents = new[]
                        {
                            prefferedDetent,
                        };
                        
                        if (m_bottomSheet.IsDraggable)
                        {
                            m_sheetPresentationController.PrefersGrabberVisible = true;
                            Padding = new Thickness(0, 20, 0, 0); //Move top down 10 pixels to make space for the grabber
                        }
                            
                        m_sheetPresentationController.SelectedDetentIdentifier = prefferedDetentIdentifier;
                        
                        m_sheetPresentationController.PrefersScrollingExpandsWhenScrolledToEdge = true;
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