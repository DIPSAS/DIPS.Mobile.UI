using Android.OS;
using Android.Text;
using Android.Text.Style;
using Android.Views;
using AndroidX.AppCompat.Graphics.Drawable;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Components.BottomSheets.Android;
using Google.Android.Material.AppBar;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using AView = Android.Views.View;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Toolbars.Android;

internal class DUIToolbarHandler : ViewHandler<DUIToolbar, MaterialToolbar>
{
    public DUIToolbarHandler() : base(ViewMapper)
    {
    }
    
    protected override MaterialToolbar CreatePlatformView() => new (Context);

    protected override void ConnectHandler(MaterialToolbar platformView)
    {
        base.ConnectHandler(platformView);
        
        platformView.SetBackgroundColor(VirtualView.BackgroundColor.ToPlatform());
        platformView.SetTitleTextColor(VirtualView.TitleColor.ToPlatform());    
        ConfigureToolbar();

        var rootPage = Microsoft.Maui.Controls.Shell.Current.Navigation.ModalStack.FirstOrDefault();
        if(rootPage is not NavigationPage navigationPage)
            return;

        if (!navigationPage.RootPage.Equals(VirtualView.PageConnectedTo))
        {
            AddBackButton();
        }
    }

    private void AddBackButton()
    {
        PlatformView.NavigationIcon = new DrawerArrowDrawable(Context)
        {
            Direction = 0,
            Progress = 1,
            Color = VirtualView.ToolbarItemsColor.ToPlatform()
        };
        PlatformView.SetNavigationOnClickListener(new ProcessBackClick(this));
    }
    
    private void ConfigureToolbar()
    {
        if(VirtualView.PageConnectedTo == null)
            return;
        
        PlatformView.Title = VirtualView.PageConnectedTo.Title;

        if (PlatformView.Menu == null) return;
        foreach (var toolbarItem in VirtualView.PageConnectedTo.ToolbarItems)
        {
            toolbarItem.BindingContext = VirtualView.PageConnectedTo.BindingContext;

            var text = toolbarItem.Text ?? string.Empty;
            var titleTinted = new SpannableString(text);
            titleTinted.SetSpan(new ForegroundColorSpan(VirtualView.ToolbarItemsColor.ToPlatform()), 0, titleTinted.Length(), 0);

            var menuItem = PlatformView.Menu.Add(0, AView.GenerateViewId(), (int)toolbarItem.Order, titleTinted);
            menuItem!.SetShowAsAction(ShowAsAction.IfRoom);
            menuItem.SetOnMenuItemClickListener(new BottomSheetHandler.GenericMenuClickListener(((IMenuItemController)toolbarItem).Activate));
            SetMenuItemIcon(menuItem, toolbarItem);
        }
    }
    
    private static void SetMenuItemIcon(IMenuItem menuItem, ToolbarItem toolBarItem)
    {
        toolBarItem.IconImageSource.LoadImage(DUI.GetCurrentMauiContext!, result =>
        {
            var baseDrawable = result?.Value;

            if (baseDrawable == null)
                return;

            using var constant = baseDrawable.GetConstantState();
            using var newDrawable = constant!.NewDrawable();
            using var iconDrawable = newDrawable.Mutate();
            iconDrawable.SetColorFilter(Colors.GetColor(BottomSheet.ToolbarActionButtonsName), FilterMode.SrcAtop);

            menuItem.SetIcon(iconDrawable);
        });
    }

    private void BackClick()
    {
        Microsoft.Maui.Controls.Shell.Current.Navigation.PopAsync();
    }
    
    private class ProcessBackClick : Java.Lang.Object, AView.IOnClickListener
    {
        DUIToolbarHandler m_toolbarHandler;

        public ProcessBackClick(DUIToolbarHandler toolbarHandler)
        {
            m_toolbarHandler = toolbarHandler;
        }

        public void OnClick(AView? v)
        {
            m_toolbarHandler.BackClick();
        }
    }

}