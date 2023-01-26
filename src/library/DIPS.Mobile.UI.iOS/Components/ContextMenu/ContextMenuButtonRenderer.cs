using System;
using System.Linq;
using System.Threading.Tasks;
using CoreGraphics;
using DIPS.Mobile.UI.Components.Buttons;
using DIPS.Mobile.UI.Components.ContextMenu;
using DIPS.Mobile.UI.iOS.Components.ContextMenu;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContextMenuButton), typeof(ContextMenuButtonRenderer))]

namespace DIPS.Mobile.UI.iOS.Components.ContextMenu
{
    internal class ContextMenuButtonRenderer : Xamarin.Forms.Platform.iOS.ButtonRenderer
    {
        private ContextMenuButton m_contextMenuButton;
        private NSObject m_didEnterBackgroundNotification;

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement is ContextMenuButton contextMenuButton)
            {
                m_contextMenuButton = contextMenuButton;
                if (Control == null)
                {
                    return;
                }

                {
                    base.SetNativeControl(new ContextMenuUiButton(m_contextMenuButton));
                    Control.Menu =
                        CreateMenu(); //Create the menu the first time so it shows up the first time the user taps the button
                    Control.TouchDown += OnTouchDown;
                    Control.ShowsMenuAsPrimaryAction = true;
                    m_didEnterBackgroundNotification = NSNotificationCenter.DefaultCenter.AddObserver(UIApplication.DidEnterBackgroundNotification, notification =>
                    {
                        Control.Menu = null;
                        Control.Menu = CreateMenu(); //Recreate the menu to close it, and to make it possible to re-open it in one tap after it went to the background
                    });
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            Control.TouchDown -= OnTouchDown;
            NSNotificationCenter.DefaultCenter.RemoveObserver(m_didEnterBackgroundNotification);
            base.Dispose(disposing);
        }

        private void OnTouchDown(object sender, EventArgs e)
        {
            Control.Menu =
                CreateMenu(); //Recreate the menu so the visuals of the items of the menu are able to change between each time the user opens the menu
            m_contextMenuButton.SendContextMenuOpened();
        }

        private UIMenu CreateMenu()
        {
            if (m_contextMenuButton.ItemsSource == null) return null;
            
            var dict = ContextMenuHelper.CreateMenuItems(
                m_contextMenuButton.ItemsSource,
                m_contextMenuButton);
            return UIMenu.Create(m_contextMenuButton.Title, dict.Select(k => k.Value).ToArray());
        }
    }

    internal sealed class ContextMenuUiButton : UIButton
    {
        private readonly ContextMenuButton m_contextMenuButton;

        public ContextMenuUiButton(ContextMenuButton contextMenuButton)
        {
            m_contextMenuButton = contextMenuButton;
            SetTitle(contextMenuButton.Text, UIControlState.Normal);
            SetTitleColor(contextMenuButton.TextColor.ToUIColor(), UIControlState.Normal);
        }

        public override CGPoint GetMenuAttachmentPoint(UIContextMenuConfiguration configuration)
        {
            var original = base.GetMenuAttachmentPoint(configuration);
            return m_contextMenuButton.ContextMenuHorizontalOptions switch
            {
                ContextMenuHorizontalOptions.Right => new CGPoint(9999, original.Y),
                ContextMenuHorizontalOptions.Left => new CGPoint(0, original.Y),
                _ => new CGPoint(9999, original.Y)
            };
        }
    }
}