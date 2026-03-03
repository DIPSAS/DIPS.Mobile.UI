using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.Widget;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.ContextMenus.Android;
using Microsoft.Maui.Platform;
using Object = Java.Lang.Object;
using PopupMenu = Android.Widget.PopupMenu;
using View = Android.Views.View;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.ContextMenus;

public partial class ContextMenuPlatformEffect
{
    private ContextMenu? m_contextMenu;
    
#nullable disable
    private ContextMenuHandler m_contextMenuBehaviour;
    private ContextMenuEffect.ContextMenuMode m_mode;
#nullable restore

    protected override partial void OnAttached()
    {
        m_contextMenu = ContextMenuEffect.GetMenu(Element);

        m_mode = ContextMenuEffect.GetMode(Element);

        if (m_contextMenu == null)
        {
            return;
        }

        var bindingContext = ContextMenuEffect.GetMenuBindingContext(Element);
        m_contextMenu.BindingContext = bindingContext ?? Element.BindingContext;
        m_contextMenu.Mode = m_mode;
        m_contextMenuBehaviour = new ContextMenuHandler(m_contextMenu, Control);

        if (m_mode == ContextMenuEffect.ContextMenuMode.Pressed)
        {
            Control.Clickable = true;
            Control.Click += m_contextMenuBehaviour.OpenContextMenu;
        }
        else
        {
            Control.LongClickable = true;
            Control.LongClick += m_contextMenuBehaviour.OpenContextMenu;
        }
    }

    public class ContextMenuHandler : Object, PopupMenu.IOnMenuItemClickListener, PopupMenu.IOnDismissListener
    {
        private readonly ContextMenu m_contextMenu;
        private readonly View m_control;
        
        private Dictionary<IContextMenuItem, IMenuItem> m_menuItems;
        private PopupMenu m_popupMenu;
        private PopupWindow? m_previewPopupWindow;

        public ContextMenuHandler(ContextMenu contextMenu, View view)
        {
            m_contextMenu = contextMenu;
            m_control = view;
        }
        
        public void OpenContextMenu(object? sender, EventArgs e)
        {
            m_popupMenu = new PopupMenu(Platform.CurrentActivity, m_control);
            
            m_menuItems = ContextMenuHelper.CreateMenuItems(m_contextMenu.ItemsSource!,
                m_contextMenu, m_popupMenu);
            
            m_popupMenu.Gravity = (m_contextMenu!.ContextMenuHorizontalOptions == ContextMenuHorizontalOptions.Right)
                ? GravityFlags.Right
                : GravityFlags.Left;
            
             m_popupMenu.SetForceShowIcon(m_menuItems.Keys.Any(contextMenuItem =>
             {
                 if (contextMenuItem is not ContextMenuItem menuItem)
                     return false;
                 
                 return menuItem.Icon != null ||
                        !string.IsNullOrEmpty(menuItem.AndroidOptions
                            .IconResourceName);
             }));
            
            SetListeners();

            if (m_contextMenu.PreviewView != null)
            {
                ShowPreview();
            }

            m_popupMenu.Show();
            
        }

        private void ShowPreview()
        {
            var activity = Platform.CurrentActivity;
            var mauiContext = DUI.GetCurrentMauiContext;
            if (activity is null || mauiContext is null || m_contextMenu.PreviewView is null)
                return;

            var previewNativeView = m_contextMenu.PreviewView.ToPlatform(mauiContext);

            previewNativeView.Measure(
                View.MeasureSpec.MakeMeasureSpec(m_control.Width, MeasureSpecMode.AtMost),
                View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified));

            m_previewPopupWindow = new PopupWindow(
                previewNativeView,
                m_control.Width,
                ViewGroup.LayoutParams.WrapContent,
                false);

            m_previewPopupWindow.SetBackgroundDrawable(new ColorDrawable(global::Android.Graphics.Color.Transparent));
            m_previewPopupWindow.Elevation = (float)Sizes.GetSize(SizeName.size_1);

            var previewHeight = previewNativeView.MeasuredHeight;
            m_previewPopupWindow.ShowAsDropDown(m_control, 0, -(m_control.Height + previewHeight));
        }

        private void DismissPreview()
        {
            m_previewPopupWindow?.Dismiss();
            m_previewPopupWindow = null;
        }

        public void OnDismiss(PopupMenu? menu)
        {
            DismissPreview();
        }

        private void SetListeners()
        {
            m_popupMenu.SetOnMenuItemClickListener(this);
            m_popupMenu.SetOnDismissListener(this);
        }
        
        public bool OnMenuItemClick(IMenuItem? theTappedNativeItem)
        {
            if (m_menuItems.FirstOrDefault(m => m.Value == theTappedNativeItem).Key is ContextMenuItem tappedContextMenuItem)
            {
                if (theTappedNativeItem!.IsCheckable) //check the item
                {
                    var singleCheckMode = tappedContextMenuItem.Parent is ContextMenuGroup {IsCheckable: true};

                    switch (singleCheckMode)
                    {
                        //You are unchecking an checked item that when single check mode is active, do not uncheck
                        case true when tappedContextMenuItem.IsChecked:
                            return true;
                        //You are checking an item that is not checked, reset the others
                        case true:
                            {
                                foreach (var pair in m_menuItems)
                                {
                                    var nativeMenuItem = pair.Value;
                                    if (nativeMenuItem.GroupId == theTappedNativeItem.GroupId) //Uncheck previous items in the same group
                                    {
                                        if (nativeMenuItem.IsChecked)
                                        {
                                            nativeMenuItem.SetChecked(false);    
                                        }
                                    }
                                }

                                break;
                            }
                    }

                    m_contextMenu.ResetIsCheckedForTheRest(tappedContextMenuItem);
                    tappedContextMenuItem.IsChecked = !tappedContextMenuItem.IsChecked;
                    theTappedNativeItem.SetChecked(tappedContextMenuItem.IsChecked);
                }

                tappedContextMenuItem.SendClicked(m_contextMenu);
                return true;
            }

            return false;
        }
    }

    protected override partial void OnDetached()
    {
        if (m_mode == ContextMenuEffect.ContextMenuMode.Pressed)
        {
            Control.Click -= m_contextMenuBehaviour.OpenContextMenu;
            if (!Control.HasOnClickListeners)
                Control.Clickable = false;
        }
        else
        {
            Control.LongClick -= m_contextMenuBehaviour.OpenContextMenu;
            if (!Control.HasOnLongClickListeners)
                Control.Clickable = false;
        }
    }
}