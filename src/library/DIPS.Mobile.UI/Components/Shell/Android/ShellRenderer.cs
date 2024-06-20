using System.Diagnostics;
using Android.App;
using Android.OS;
using Android.Text.Method;
using Android.Views;
using Android.Widget;
using AndroidX.DrawerLayout.Widget;
using DIPS.Mobile.UI.Components.ContextMenus;
using DIPS.Mobile.UI.Components.ContextMenus.Android;
using DIPS.Mobile.UI.Components.Pages;
using Google.Android.Material.AppBar;
using Microsoft.Maui.Controls.Platform.Compatibility;
using Microsoft.Maui.Platform;
using Activity = Android.App.Activity;
using Application = Android.App.Application;
using Object = Java.Lang.Object;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;
using View = Android.Views.View;

namespace DIPS.Mobile.UI.Components.Shell;

public partial class ShellRenderer : Microsoft.Maui.Controls.Handlers.Compatibility.ShellRenderer
{
    protected override IShellToolbarAppearanceTracker CreateToolbarAppearanceTracker() => ToolbarApperanceTrancer = new CustomToolbarAppearanceTracker(this);

    internal CustomToolbarAppearanceTracker ToolbarApperanceTrancer { get; set; }
}

internal class CustomToolbarAppearanceTracker : ShellToolbarAppearanceTracker
{
    private ShellAppearance? m_appearance;

    public Toolbar Toolbar { get; set; }
    public override void SetAppearance(Toolbar toolbar, IShellToolbarTracker toolbarTracker, ShellAppearance appearance)
    {
        base.SetAppearance(toolbar, toolbarTracker, appearance);
        
        Toolbar = toolbar;
        m_appearance = appearance;
        
        toolbar.LayoutChange -= ToolbarOnLayoutChange;
        toolbar.LayoutChange += ToolbarOnLayoutChange;  
    }
    


    private void ToolbarOnLayoutChange(object? sender,View.LayoutChangeEventArgs layoutChangeEventArgs )
    {
        for (var i = 0; i < Toolbar.Menu?.Size(); i++)
        {
            var page = Microsoft.Maui.Controls.Shell.Current.CurrentPage;
            if (page is null)
                return;
            
            if (page.ToolbarItems.Count == 0) return;
            var toolbarItem = page.ToolbarItems[i];
            
            var menuItem = Toolbar.Menu?.GetItem(i);
            if (menuItem?.IconTintList == null) //Fixes toolbar item icon and text color bug.
            {
                menuItem!.SetIconTintList(m_appearance?.ForegroundColor.ToDefaultColorStateList());    
            }

            if(toolbarItem is not ContextMenuToolbarItem contextMenuToolbarItem)
                continue;
            menuItem?.SetOnMenuItemClickListener(new ToolbarMenuItemClickListener(contextMenuToolbarItem.ContextMenu, Toolbar));
        }

    }

    public CustomToolbarAppearanceTracker(IShellContext shellContext) : base(shellContext) { }
    
    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        
        if(Toolbar is null)
            return;
        
        Toolbar.LayoutChange -= ToolbarOnLayoutChange;
        for (var i = 0; i < Toolbar.Menu?.Size(); i++)
        {
            var menuItem = Toolbar.Menu?.GetItem(i);
            menuItem?.SetOnMenuItemClickListener(null);
        }
    }

    private class ToolbarMenuItemClickListener : Object, IMenuItemOnMenuItemClickListener, Application.IActivityLifecycleCallbacks, PopupMenu.IOnDismissListener, PopupMenu.IOnMenuItemClickListener
    {
        private readonly ContextMenu m_contextMenu;
        private readonly Toolbar m_materialToolbar;
        private Dictionary<IContextMenuItem, IMenuItem> m_menuItems;
        private PopupMenu? m_popupMenu;
        private bool m_isShowing;

        public ToolbarMenuItemClickListener(ContextMenu contextMenu, Toolbar materialToolbar)
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