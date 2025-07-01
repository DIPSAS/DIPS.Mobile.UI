using DIPS.Mobile.UI.API.TabBadge;
using DIPS.Mobile.UI.Components.ContextMenus.iOS;
using DIPS.Mobile.UI.Components.Pages;
using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
using Microsoft.Maui.Controls.Platform.Compatibility;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Microsoft.Maui.Platform;
using UIKit;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using ContentPage = DIPS.Mobile.UI.Components.Pages.ContentPage;
using Page = Microsoft.Maui.Controls.Page;

namespace DIPS.Mobile.UI.Components.Shell;

public partial class ShellRenderer : Microsoft.Maui.Controls.Handlers.Compatibility.ShellRenderer
{
    protected override IShellPageRendererTracker CreatePageRendererTracker()
    {
        return new CustomShellPageRendererTracker(this);
    }
    
    protected override IShellTabBarAppearanceTracker CreateTabBarAppearanceTracker()
    {
        return new BadgeShellTabBarAppearanceTracker();
    }

    protected override IShellNavBarAppearanceTracker CreateNavBarAppearanceTracker()
    {
        return new NavBarAppearanceTracker();
    }
}

public class NavBarAppearanceTracker : IShellNavBarAppearanceTracker
{
    private bool m_nextPageHasShadow;

    public void Dispose()
    {
        
    }

    public void ResetAppearance(UINavigationController controller)
    {
        
    }

    public void SetAppearance(UINavigationController controller, ShellAppearance appearance)
    {
        var navBar = controller.NavigationBar;

        var navigationBarAppearance = new UINavigationBarAppearance();

        // since we cannot set the Background Image directly, let's use the alpha in the background color to determine translucence
        if (appearance.BackgroundColor?.Alpha < 1.0f)
        {
            navigationBarAppearance.ConfigureWithTransparentBackground();
            navBar.Translucent = true;
        }
        else
        {
            navigationBarAppearance.ConfigureWithOpaqueBackground();
            navBar.Translucent = false;
        }

        // Set ForegroundColor
        var foreground = appearance.ForegroundColor;

        if (foreground != null)
            navBar.TintColor = foreground.ToPlatform();

        // Set BackgroundColor
        var background = appearance.BackgroundColor;

        if (background != null)
            navigationBarAppearance.BackgroundColor = background.ToPlatform();

        // Clear divider line
        if(!m_nextPageHasShadow)
            navigationBarAppearance.ShadowColor = UIColor.Clear;
        
        // Set TitleColor
        var titleColor = appearance.TitleColor;

        if (titleColor != null)
        {
            navigationBarAppearance.TitleTextAttributes = new UIStringAttributes { ForegroundColor = titleColor.ToPlatform() };
            navigationBarAppearance.LargeTitleTextAttributes = new UIStringAttributes { ForegroundColor = appearance.ForegroundColor.ToPlatform() };
        }
        
        navBar.StandardAppearance = navBar.ScrollEdgeAppearance = navigationBarAppearance;
        navBar.PrefersLargeTitles = true;
    }

    public void UpdateLayout(UINavigationController controller)
    {
        
    }

    public void SetHasShadow(UINavigationController controller, bool hasShadow)
    {
        m_nextPageHasShadow = hasShadow;
    }
}

internal class BadgeShellTabBarAppearanceTracker : ShellTabBarAppearanceTracker
{
    private WeakReference<UITabBarController>? m_tabBarController;

    public override void UpdateLayout(UITabBarController controller)
    {
        base.UpdateLayout(controller);
        
        m_tabBarController = new WeakReference<UITabBarController>(controller);
        
        TabBadgeService.OnBadgeColorChanged -= OnTabBadgeServicePropertyChanged;
        TabBadgeService.OnBadgeCountChanged -= OnTabBadgeServicePropertyChanged;
        
        TabBadgeService.OnBadgeColorChanged += OnTabBadgeServicePropertyChanged;
        TabBadgeService.OnBadgeCountChanged += OnTabBadgeServicePropertyChanged;
        
        SetBadges();
    }

    private void SetBadges()
    {
        if(m_tabBarController is null || !m_tabBarController.TryGetTarget(out var tabBarController))
            return;

        if (tabBarController is ShellItemRenderer { ShellItem: null })
        {
            // Dispose is never called, thus, we need to do it manually
            this.Dispose(true);
            return;
        }
        
        foreach (var (tabIndex, count) in TabBadgeService.s_badgeCounts)
        {
            var tabBarItems = tabBarController.TabBar?.Items;
            if(tabBarItems is null || tabBarItems.Length < tabIndex)
                return;

            var item = tabBarItems[tabIndex];
            item.BadgeValue = count;
            
            if (TabBadgeService.s_badgeColors.TryGetValue(tabIndex, out var color))
                item.BadgeColor = color.ToPlatform();
        }
    }

    private void OnTabBadgeServicePropertyChanged()
    {
        SetBadges();
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        
        TabBadgeService.OnBadgeColorChanged -= OnTabBadgeServicePropertyChanged;
        TabBadgeService.OnBadgeCountChanged -= OnTabBadgeServicePropertyChanged;
    }
}

internal class CustomShellPageRendererTracker : ShellPageRendererTracker
{
    public CustomShellPageRendererTracker(IShellContext context) : base(context)
    {
    }

    protected override void OnPageSet(Page oldPage, Page newPage)
    {
        base.OnPageSet(oldPage, newPage);

        if (newPage is not ContentPage contentPage)
            return;

        ViewController.NavigationItem.LargeTitleDisplayMode = contentPage.LargeTitleDisplay switch
        {
            LargeTitleDisplayMode.Automatic => UINavigationItemLargeTitleDisplayMode.Automatic,
            LargeTitleDisplayMode.Never => UINavigationItemLargeTitleDisplayMode.Never,
            LargeTitleDisplayMode.Always => UINavigationItemLargeTitleDisplayMode.Always,
            _ => UINavigationItemLargeTitleDisplayMode.Automatic
        };
    }

    protected override void UpdateToolbarItems()
    {
        base.UpdateToolbarItems();
        
        if (ViewController?.NavigationItem == null)
        {
            return;
        }

        if (ViewController.NavigationItem.RightBarButtonItems != null)
        {
            foreach (var t in ViewController.NavigationItem.RightBarButtonItems)
                t.Dispose();
        }

        List<UIBarButtonItem>? primaries = null;
        if (Page.ToolbarItems.Count > 0)
        {
            foreach (var item in Page.ToolbarItems.OrderBy(x => x.Priority))
            {
                if (item is not ContextMenuToolbarItem contextMenuToolbarItem)
                {
                    (primaries ??= []).Add(item.ToUIBarButtonItem(false, true));
                }
                else
                {
                    (primaries ??= []).Add(ToContextMenuBarButtonItem(contextMenuToolbarItem));
                }
            }

            if (primaries != null)
                primaries.Reverse();
        }

        ViewController.NavigationItem.SetRightBarButtonItems(primaries == null ? [] : primaries.ToArray(), false);
    }
    
    private UIBarButtonItem ToContextMenuBarButtonItem(ContextMenuToolbarItem toolbarItem)
    {
        var dict = ContextMenuHelper.CreateMenuItems(toolbarItem.ContextMenu.ItemsSource!, toolbarItem.ContextMenu, UpdateToolbarItems);
        if (toolbarItem.IconImageSource is FileImageSource fileImageSource)
        {
            return new UIBarButtonItem(UIImage.FromBundle(fileImageSource), UIMenu.Create(toolbarItem.ContextMenu.Title, dict.Select(k => k.Value).ToArray()));
        }

        return new UIBarButtonItem(toolbarItem.Text, UIMenu.Create(toolbarItem.ContextMenu.Title, dict.Select(k => k.Value).ToArray()));
    }

    protected override void Dispose(bool disposing)
    {
        if (ViewController.NavigationItem.RightBarButtonItems is not null)
        {
            foreach (var test in Page.ToolbarItems)
            {
                if (test is not ContextMenuToolbarItem { ContextMenu.ItemsSource: not null } contextMenuToolbarItem)
                    continue;

                foreach (var item in contextMenuToolbarItem.ContextMenu.ItemsSource)
                {
                    if (item is IDisposable contextMenuItem)
                    {
                        contextMenuItem.Dispose();
                    }
                }
            }
        }
        
        base.Dispose(disposing);
    }
}