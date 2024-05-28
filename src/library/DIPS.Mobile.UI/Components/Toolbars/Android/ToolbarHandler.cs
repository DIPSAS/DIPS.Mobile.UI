using System.ComponentModel;
using Android.OS;
using Android.Views;
using Android.Widget;
using DIPS.Mobile.UI.Components.ContextMenus;
using DIPS.Mobile.UI.Components.ContextMenus.Android;
using DIPS.Mobile.UI.Components.Pages;
using Google.Android.Material.AppBar;
using Activity = Android.App.Activity;
using Application = Android.App.Application;
using Object = Java.Lang.Object;
using Toolbar = Microsoft.Maui.Controls.Toolbar;

namespace DIPS.Mobile.UI.Components.Toolbars.Android;

public class ToolbarHandler : Microsoft.Maui.Handlers.ToolbarHandler
{
    protected override async void ConnectHandler(MaterialToolbar platformView)
    {
        base.ConnectHandler(platformView);
        
        if (VirtualView is not Toolbar toolbar)
            return;
        
        toolbar.PropertyChanged += ToolbarOnPropertyChanged;
        
        await Task.Delay(1);
        FindContextMenuToolbarItemsAndSetContextMenusOnThem();
    }

    private void ToolbarOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Toolbar.ToolbarItems))
        {
            FindContextMenuToolbarItemsAndSetContextMenusOnThem();
        }
    }

    private void FindContextMenuToolbarItemsAndSetContextMenusOnThem()
    {
        if (VirtualView is not Toolbar toolbar)
            return;
        
        if(toolbar.ToolbarItems is not List<ToolbarItem> toolbarItems)
            return;
        
        for (var i = 0; i < PlatformView.Menu?.Size(); i++)
        {
            var toolbarItem = toolbarItems[i];
            
            if(toolbarItem is not ContextMenuToolbarItem contextMenuToolbarItem)
                continue;
            
            var menuItem = PlatformView.Menu?.GetItem(i);
            menuItem?.SetShowAsAction(ShowAsAction.Always);
            menuItem?.SetOnMenuItemClickListener(new ToolbarMenuItemClickListener(contextMenuToolbarItem.ContextMenu, PlatformView));
        }
        
    }

    protected override void DisconnectHandler(MaterialToolbar platformView)
    {
        base.DisconnectHandler(platformView);
        
        if (VirtualView is not Toolbar toolbar)
            return;
        
        toolbar.PropertyChanged -= ToolbarOnPropertyChanged;
    }

    private class ToolbarMenuItemClickListener : Object, IMenuItemOnMenuItemClickListener, Application.IActivityLifecycleCallbacks, PopupMenu.IOnDismissListener, PopupMenu.IOnMenuItemClickListener
    {
        private readonly ContextMenu m_contextMenu;
        private readonly MaterialToolbar m_materialToolbar;
        private Dictionary<IContextMenuItem, IMenuItem> m_menuItems;
        private PopupMenu? m_popupMenu;
        private bool m_isShowing;

        public ToolbarMenuItemClickListener(ContextMenu contextMenu, MaterialToolbar materialToolbar)
        {
            m_contextMenu = contextMenu;
            m_materialToolbar = materialToolbar;
        }
        
        /// <summary>
        /// <see cref="IMenuItemOnMenuItemClickListener"/> and <see cref="PopupMenu.IOnMenuItemClickListener"/> interfaces uses the exact same function
        /// So we need to check if popupmenu is not null, if it is not null, we know that we have pressed the item inside popupmenu
        /// </summary>
        public bool OnMenuItemClick(IMenuItem? item)
        {
            return m_popupMenu is not null ? OnTappedPopupMenuItem(item) : OnTappedToolbarItem(item);
        }

        private bool OnTappedPopupMenuItem(IMenuItem? item)
        {
            if (item is null)
                return false;
            
            if (m_menuItems.FirstOrDefault(m => m.Value == item).Key is not ContextMenuItem tappedContextMenuItem)
                return false;

            if (item.IsCheckable) //check the item
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
                                if (nativeMenuItem.GroupId != item.GroupId) //Uncheck previous items in the same group
                                {
                                    continue;
                                }

                                if (nativeMenuItem.IsChecked)
                                {
                                    nativeMenuItem.SetChecked(false);    
                                }
                            }

                            break;
                        }
                }

                m_contextMenu.ResetIsCheckedForTheRest(tappedContextMenuItem);
                tappedContextMenuItem.IsChecked = !tappedContextMenuItem.IsChecked;
                item.SetChecked(tappedContextMenuItem.IsChecked);
            }

            tappedContextMenuItem.SendClicked(m_contextMenu);
            return true;

        }
        
        private bool OnTappedToolbarItem(IMenuItem? item)
        {
            var anchoredItem = m_materialToolbar.FindViewById(item!.ItemId);
            m_popupMenu = new PopupMenu(Platform.CurrentActivity, anchoredItem);
            
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
            
            m_popupMenu.Show();
            
            m_isShowing = true;

            return true;
        }
        
        private void SetListeners()
        {
            m_popupMenu?.SetOnDismissListener(this);
            m_popupMenu?.SetOnMenuItemClickListener(this);
        }

        public void OnActivityCreated(Activity activity, Bundle? savedInstanceState)
        {
            
        }

        public void OnActivityDestroyed(Activity activity)
        {
       
        }

        public void OnActivityPaused(Activity activity)
        {
            if (!m_isShowing)
                return;

            m_popupMenu?.Dismiss();
            m_popupMenu = null;
        }

        public void OnActivityResumed(Activity activity)
        {
           
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
          
        }

        public void OnActivityStarted(Activity activity)
        {
       
        }

        public void OnActivityStopped(Activity activity)
        {
         
        }

        public void OnDismiss(PopupMenu? menu)
        {
            m_isShowing = false;
            m_popupMenu = null;
        }

    }
}