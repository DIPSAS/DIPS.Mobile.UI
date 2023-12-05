using Android.Content;
using Android.Text;
using DIPS.Mobile.UI.API.Library;
using Android.Text.Style;
using Android.Views;
using Android.Widget;
using Google.Android.Material.AppBar;
using Google.Android.Material.BottomSheet;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Application = Android.App.Application;
using Grid = Microsoft.Maui.Controls.Grid;
using AView = Android.Views.View;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Object = Java.Lang.Object;

namespace DIPS.Mobile.UI.Components.BottomSheets;

public partial class BottomSheetHandler : ContentViewHandler
{
#nullable disable
    internal BottomSheet m_bottomSheet;
#nullable enable
    
    internal RelativeLayout m_linearLayout;
    private static AView? m_emptyNonFitToContentView;
    private AView? m_searchBarView;
    private MaterialToolbar? m_toolbar;
    
    internal ContentViewGroup? m_bottomBar;

    public AView OnBeforeOpening(IMauiContext mauiContext, Context context, AView bottomSheetAndroidView,
        RelativeLayout rootLayout, LinearLayout bottomSheetLayout)
    {
        if (VirtualView is not BottomSheet bottomSheet) return new AView(Context);
        
        m_bottomSheet = bottomSheet;
        
        bottomSheet.BottomSheetDialog.Behavior.AddBottomSheetCallback(
            new BottomSheetCallback(this));
        bottomSheet.BottomSheetDialog.SetOnShowListener(new DialogInterfaceOnShowListener(this));
        bottomSheet.BottomSheetDialog.SetOnKeyListener(new KeyListener(this));

        //Add a handle, with a innerGrid that works as a big hit box for the user to hit
        //Inspired by com.google.android.material.bottomsheet.BottomSheetDragHandleView , which will be added in Xamarin Android Material Design v1.7.0.  https://github.com/material-components/material-components-android/commit/ac7b761294808748df167b50b223b591ca9dac06
        if (m_bottomSheet.BottomSheetBehavior.Draggable)
        {
            var innerGrid = new Grid {Padding = new Thickness(0, Sizes.GetSize(SizeName.size_2))};
            innerGrid.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(ToggleBottomSheetIfPossible)
            });
            var handle = new BoxView()
            {
                HeightRequest = 4,
                WidthRequest = 32,
                CornerRadius = 10,
                BackgroundColor = Colors.GetColor(ColorName.color_neutral_40),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            innerGrid.Add(handle);

            bottomSheetLayout.AddView(innerGrid.ToPlatform(mauiContext!));
        }


        m_toolbar = new MaterialToolbar(context);
        bottomSheetLayout.AddView(m_toolbar);
        ToggleToolbarVisibility(bottomSheet);

        m_searchBarView = m_bottomSheet.SearchBar.ToPlatform(mauiContext!);
        bottomSheetLayout.AddView(m_searchBarView);
        ToggleSearchBar();
        MapToolbarItems(this, bottomSheet);

        bottomSheetLayout.AddView(bottomSheetAndroidView);

        if (m_bottomSheet.Positioning is not Positioning.Fit)
        {
            var width = Platform.AppContext.Resources?.DisplayMetrics?.WidthPixels;
            if (width.HasValue)
            {
                // Add an empty view, where its dimensions is set to always match the parent so that the LinearLayout will always take up available space
                m_emptyNonFitToContentView = new AView(Application.Context)
                {
                    LayoutParameters = new ViewGroup.LayoutParams(width.Value,
                        ViewGroup.LayoutParams.MatchParent)
                };
                rootLayout.AddView(m_emptyNonFitToContentView);
            }
        }

        if (m_bottomSheet.HasBottomBarButtons)
        {
            m_bottomBar = (ContentViewGroup)m_bottomSheet.CreateBottomBar().ToPlatform(MauiContext!);
            m_bottomBar.LayoutParameters =
                new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
            
            rootLayout.AddView(m_bottomBar);
        }
        
        return m_linearLayout = rootLayout;
    }

    private void ToggleToolbarVisibility(BottomSheet bottomSheet)
    {
        if (m_toolbar is not { } toolbar) return;
        toolbar.Visibility = bottomSheet.ShouldHaveNavigationBar ? ViewStates.Visible : ViewStates.Gone;
    }

    private void ToggleSearchBar()
    {
        if (m_searchBarView == null) return;
        if (m_bottomSheet.HasSearchBar)
        {
            m_searchBarView.Visibility = ViewStates.Visible;
        }
        else
        {
            m_searchBarView.Visibility = ViewStates.Gone;
        }
    }

    private void ToggleFitToContent(BottomSheet bottomSheet)
    {
        if (m_emptyNonFitToContentView != null) //Only add there is something in the bottom sheet
        {
            if (bottomSheet.Positioning is not Positioning.Fit)
            {
                m_emptyNonFitToContentView.Visibility = ViewStates.Visible;
            }
            else
            {
                m_emptyNonFitToContentView.Visibility = ViewStates.Gone;
            }
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

    private static partial void MapPositioning(BottomSheetHandler handler, BottomSheet bottomSheet)
    {
        //Reset fit to content stuff in case it changes
        bottomSheet.BottomSheetDialog.Behavior.FitToContents =
            (bottomSheet.Positioning) == Positioning.Fit; 
        handler.ToggleFitToContent(bottomSheet);

        if (bottomSheet.Positioning == Positioning.Large)
        {
            bottomSheet.BottomSheetDialog.Behavior.State = BottomSheetBehavior.StateExpanded;
        }
        else if (bottomSheet.Positioning == Positioning.Medium)
        {
            bottomSheet.BottomSheetDialog.Behavior.State = BottomSheetBehavior.StateHalfExpanded;
        }
        else if (bottomSheet.Positioning == Positioning.Collapsed)
        {
            bottomSheet.BottomSheetDialog.Behavior.State = BottomSheetBehavior.StateCollapsed;
        }
    }

    private void ToggleBottomSheetIfPossible()
    {
        var bottomSheetBehavior = m_bottomSheet.BottomSheetDialog.Behavior;
        var collapsed = m_bottomSheet.BottomSheetDialog.Behavior.State == BottomSheetBehavior.StateCollapsed;
        bottomSheetBehavior.State =
            collapsed ? BottomSheetBehavior.StateExpanded : BottomSheetBehavior.StateCollapsed;
    }

    public static partial void MapHasSearchBar(BottomSheetHandler handler, BottomSheet bottomSheet)
    {
        handler.ToggleSearchBar();
    }

    public static partial void MapToolbarItems(BottomSheetHandler handler, BottomSheet bottomSheet)
    {
        handler.ToggleToolbarVisibility(bottomSheet);

        if (handler.m_toolbar is not { } toolbar)
        {
            return;
        }

        if (toolbar.Menu == null) return;

        foreach (var toolbarItem in bottomSheet.ToolbarItems)
        {
            toolbarItem.BindingContext = bottomSheet.BindingContext;
            var color = Colors.GetColor(BottomSheet.ToolbarActionButtonsName).ToPlatform();

            var text = toolbarItem.Text;
            var titleTinted = new SpannableString(text);
            titleTinted.SetSpan(new ForegroundColorSpan(color), 0, titleTinted.Length(), 0);

            var menuItem = toolbar.Menu.Add(0, AView.GenerateViewId(), (int)toolbarItem.Order, titleTinted);
            menuItem!.SetShowAsAction(ShowAsAction.IfRoom);
            menuItem.SetOnMenuItemClickListener(
                new GenericMenuClickListener(((IMenuItemController)toolbarItem).Activate));
            SetMenuItemIcon(menuItem, toolbarItem);
        }
    }

    public static partial void MapTitle(BottomSheetHandler handler, BottomSheet bottomSheet)
    {
        handler.ToggleToolbarVisibility(bottomSheet);
        if (handler.m_toolbar is not { } toolbar)
        {
            return;
        }

        toolbar.Title = bottomSheet.Title;
        toolbar.TitleCentered = true;
    }

    public static partial void MapIsInteractiveCloseable(BottomSheetHandler handler, BottomSheet bottomSheet)
    {
        bottomSheet.BottomSheetDialog.SetCancelable(bottomSheet.IsInteractiveCloseable);
        bottomSheet.BottomSheetDialog.SetCanceledOnTouchOutside(bottomSheet.IsInteractiveCloseable);
    }

    private static async partial  void MapBottomBar(BottomSheetHandler handler, BottomSheet bottomSheet)
    {
        
    }

    /// <summary>
    /// 1.0 = full screen
    /// 0.0 = medium screen
    /// -1.0 = closed
    /// </summary>
    private void OnSlide(float slideOffset, AView bottomSheet)
    {
        if (m_bottomSheet.HasBottomBarButtons)
        {
            if(slideOffset < -.35)
                return;
            
            SetBottomBarTranslation(bottomSheet);
        }
    }

    internal void SetBottomBarTranslation(AView view)
    {
        var bottomSheetVisibleHeight = view.Height - view.Top;
                
        if(m_bottomBar is not null)
            m_bottomBar.TranslationY = bottomSheetVisibleHeight - m_bottomBar.Height;
    }
    
    internal bool OnKey(IDialogInterface? dialog, Keycode keyCode, KeyEvent? keyEvent)
    {
        m_bottomSheet.OnBackButtonPressedCommand?.Execute(null);
        return !m_bottomSheet.IsInteractiveCloseable; //Returning true will block back key action
    }

    internal class BottomSheetCallback : BottomSheetBehavior.BottomSheetCallback
    {
        private readonly BottomSheetHandler m_bottomSheetHandler;

        public BottomSheetCallback(BottomSheetHandler bottomSheetHandler)
        {
            m_bottomSheetHandler = bottomSheetHandler;
        }

        public override void OnSlide(AView bottomSheet, float slideOffset)
        {
            m_bottomSheetHandler.OnSlide(slideOffset, bottomSheet);
        }

        public override void OnStateChanged(AView bottomSheet, int state)
        {
            m_bottomSheetHandler.m_bottomSheet.Positioning = state switch
            {
                BottomSheetBehavior.StateExpanded => Positioning.Large,
                BottomSheetBehavior.StateHalfExpanded => Positioning.Medium,
                BottomSheetBehavior.StateCollapsed => Positioning.Collapsed,
                _ => m_bottomSheetHandler.m_bottomSheet.Positioning
            };
        }
    }

    internal class KeyListener : Java.Lang.Object, IDialogInterfaceOnKeyListener
    {   
        private readonly BottomSheetHandler m_bottomSheetHandler;

        public KeyListener(BottomSheetHandler bottomSheetHandler)
        {
            m_bottomSheetHandler = bottomSheetHandler;
        }

        public bool OnKey(IDialogInterface? dialog, Keycode keyCode, KeyEvent? e) =>
            m_bottomSheetHandler.OnKey(dialog, keyCode, e);
    }
}

public class DialogInterfaceOnShowListener : Object, IDialogInterfaceOnShowListener
{
    private readonly BottomSheetHandler m_handler;

    public DialogInterfaceOnShowListener(BottomSheetHandler handler)
    {
        m_handler = handler;
    }
    
    public void OnShow(IDialogInterface? dialog)
    {
        if (m_handler.m_linearLayout.Parent is FrameLayout frameLayout)
        {
            m_handler.SetBottomBarTranslation(frameLayout);
        }
    }
}