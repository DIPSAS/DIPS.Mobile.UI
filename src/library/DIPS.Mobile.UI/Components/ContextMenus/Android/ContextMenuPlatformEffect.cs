using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using DIPS.Mobile.UI.Components.ContextMenus.Android;
using DIPS.Mobile.UI.Effects.Touch;
using Application = Android.App.Application;
using Object = Java.Lang.Object;
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

        m_contextMenu.BindingContext = Element.BindingContext;
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

    public class ContextMenuHandler : Object, PopupMenu.IOnMenuItemClickListener, Application.IActivityLifecycleCallbacks, PopupMenu.IOnDismissListener
    {
        private readonly ContextMenu m_contextMenu;
        private readonly View m_control;
        
        private Dictionary<IContextMenuItem, IMenuItem> m_menuItems;
        private PopupMenu m_popupMenu;
        private bool m_isShowing;

        public ContextMenuHandler(ContextMenu contextMenu, View view)
        {
            m_contextMenu = contextMenu;
            m_control = view;
            Platform.CurrentActivity!.RegisterActivityLifecycleCallbacks(this);
        }
        
        public void OpenContextMenu(object? sender, EventArgs e)
        {
            m_popupMenu = new PopupMenu(Platform.CurrentActivity, m_control);
            
            m_menuItems = ContextMenuHelper.CreateMenuItems(m_contextMenu.ItemsSource!,
                m_contextMenu, m_popupMenu);
            
            m_popupMenu.Gravity = (m_contextMenu!.ContextMenuHorizontalOptions == ContextMenuHorizontalOptions.Right)
                ? GravityFlags.Right
                : GravityFlags.Left; ;
            
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
        }

        private void SetListeners()
        {
            m_popupMenu.SetOnDismissListener(this);
            m_popupMenu.SetOnMenuItemClickListener(this);
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

        public void OnActivityCreated(Activity activity, Bundle? savedInstanceState)
        {
        }

        public void OnActivityDestroyed(Activity activity)
        {
        }

        public void OnActivityPaused(Activity activity)
        {
            if (m_isShowing)
            {
                m_popupMenu.Dismiss();    
            }
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
        
        Platform.CurrentActivity!.UnregisterActivityLifecycleCallbacks(m_contextMenuBehaviour);
    }
}