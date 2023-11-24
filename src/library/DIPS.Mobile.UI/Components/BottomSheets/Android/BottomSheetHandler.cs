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
using Orientation = Android.Widget.Orientation;

namespace DIPS.Mobile.UI.Components.BottomSheets;

public partial class BottomSheetHandler : ContentViewHandler
{
    private BottomSheet m_bottomSheet;
    private LinearLayout m_linearLayout;
    private static AView? m_emptyNonFitToContentView;
    private AView? m_searchBarView;
    private MaterialToolbar? m_toolbar;

    public AView OnBeforeOpening(IMauiContext mauiContext, Context context, AView bottomSheetAndroidView,
        LinearLayout rootLayout)
    {
        if (VirtualView is not BottomSheet bottomSheet) return new AView(Context);
        m_bottomSheet = bottomSheet;
        bottomSheet.BottomSheetDialog.Behavior.AddBottomSheetCallback(
            new BottomSheetCallback(this));
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

            rootLayout.AddView(innerGrid.ToPlatform(mauiContext!));
        }


        m_toolbar = new MaterialToolbar(context);
        rootLayout.AddView(m_toolbar);
        ToggleToolbarVisibility(bottomSheet);

        m_searchBarView = m_bottomSheet.SearchBar.ToPlatform(mauiContext!);
        rootLayout.AddView(m_searchBarView);
        ToggleSearchBar();
        MapToolbarItems(this, bottomSheet);

        rootLayout.AddView(bottomSheetAndroidView);

        if (!m_bottomSheet.ShouldFitToContent)
        {
            // Add an empty view, where its dimensions is set to always match the parent so that the LinearLayout will always take up available space
            m_emptyNonFitToContentView = new AView(Application.Context)
            {
                LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                    ViewGroup.LayoutParams.MatchParent)
            };
            rootLayout.AddView(m_emptyNonFitToContentView);
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
        var context = Platform.AppContext;

        bottomSheet.BottomSheetDialog.Behavior.FitToContents = bottomSheet.ShouldFitToContent;
        if (!bottomSheet.ShouldFitToContent)
        {
            var fullScreenHeight = context.Resources?.DisplayMetrics?.HeightPixels;
            if (fullScreenHeight != null)
            {
                bottomSheet.BottomSheetDialog.Behavior.PeekHeight = fullScreenHeight.Value / 2;
            }
        }

        if (m_emptyNonFitToContentView != null) //Only add there is something in the bottom sheet
        {
            if (!bottomSheet.ShouldFitToContent)
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

    private void ToggleBottomSheetIfPossible()
    {
        var bottomSheetBehavior = m_bottomSheet.BottomSheetDialog.Behavior;
        var collapsed = m_bottomSheet.BottomSheetDialog.Behavior.State == BottomSheetBehavior.StateCollapsed;
        bottomSheetBehavior.State =
            collapsed ? BottomSheetBehavior.StateExpanded : BottomSheetBehavior.StateCollapsed;
    }

    public static partial void MapShouldFitToContent(BottomSheetHandler handler, BottomSheet bottomSheet)
    {
        handler.ToggleFitToContent(bottomSheet);
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

    private void OnSlide(float slideOffset)
    {
        if (slideOffset < 0 && !m_bottomSheet.IsInteractiveCloseable)
        {
            m_bottomSheet.BottomSheetBehavior.State = BottomSheetBehavior.StateDragging;
        }
    }

    private bool OnKey(IDialogInterface? dialog, Keycode keyCode, KeyEvent? keyEvent)
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

        public override void OnSlide(AView bottomSheet, float slideOffset) => m_bottomSheetHandler.OnSlide(slideOffset);

        public override void OnStateChanged(AView p0, int p1)
        {
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