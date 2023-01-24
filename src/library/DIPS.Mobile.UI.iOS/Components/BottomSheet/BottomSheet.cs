using System;
using System.Threading.Tasks;
using DIPS.Mobile.UI.Components.BottomSheet;
using UIKit;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.iOS.Components.BottomSheet
{
    public class BottomSheet : IBottomSheet
    {
        private readonly BottomSheetView m_bottomSheetView;
        private UIViewController m_sheetViewController;
        private UISheetPresentationController m_sheetPresentationController;

        public BottomSheet(BottomSheetView bottomSheetView)
        {
            m_bottomSheetView = bottomSheetView;
        }

        public Task Open()
        {
            
            var currentViewController = DUI.CurrentViewController;
            m_sheetViewController = new ContentPage() {Content = m_bottomSheetView}.CreateViewController();

            m_sheetViewController.ModalPresentationStyle = UIModalPresentationStyle.PageSheet;
            m_sheetPresentationController = m_sheetViewController.SheetPresentationController;
            if (m_sheetPresentationController != null)
            {
                m_sheetPresentationController.Detents = new[]
                {
                    UISheetPresentationControllerDetent.CreateMediumDetent(),
                    UISheetPresentationControllerDetent.CreateLargeDetent()
                };
                m_sheetPresentationController.SelectedDetentIdentifier = UISheetPresentationControllerDetentIdentifier.Medium;
                m_sheetPresentationController.PrefersGrabberVisible = true;
            }
            
            return currentViewController.PresentViewControllerAsync(m_sheetViewController, true);
            return Task.CompletedTask;
        }

        public async Task Close()
        {
            await m_sheetViewController.DismissViewControllerAsync(true);
        }
    }
}