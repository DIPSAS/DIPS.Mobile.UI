using Android.Content;
using Android.Provider;
using Android.Text;
using DIPS.Mobile.UI.API.Library;
using Android.Text.Style;
using Android.Views;
using Android.Widget;
using DIPS.Mobile.UI.Components.BottomSheets.Android;
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

    public AView OnBeforeOpening(IMauiContext mauiContext, Context context,
        BottomSheetBehavior bottomSheetBehavior, AView bottomSheetAndroidView)
    {
        if (VirtualView is not BottomSheet bottomSheet) return new AView(Context);
        m_bottomSheet = bottomSheet;
        
        var linearLayout = new LinearLayout(Context)
            {
                LayoutParameters =
                    new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent,
                        ViewGroup.LayoutParams.WrapContent),
                Orientation = Orientation.Vertical
            };
        
            //Add a handle, with a innerGrid that works as a big hit box for the user to hit
            //Inspired by com.google.android.material.bottomsheet.BottomSheetDragHandleView , which will be added in Xamarin Android Material Design v1.7.0.  https://github.com/material-components/material-components-android/commit/ac7b761294808748df167b50b223b591ca9dac06
            if (bottomSheetBehavior!.Draggable)
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

                linearLayout.AddView(innerGrid.ToPlatform(mauiContext!), 0);
            }

            if (m_bottomSheet.ShouldHaveNavigationBar)
            {
                var toolbar = new MaterialToolbar(Context!);
                ConfigureToolbar(toolbar);
                linearLayout.AddView(toolbar, 1);
            }

            if (m_bottomSheet.HasSearchBar)
            {
                linearLayout.AddView(m_bottomSheet.SearchBar!.ToPlatform(mauiContext!), 2);
            }

            //TODO: This will trigger handler
            linearLayout.AddView(bottomSheetAndroidView, 3);

            if (!m_bottomSheet.ShouldFitToContent)
            {
                // Add an empty view, where its dimensions is set to always match the parent so that the LinearLayout will always take up available space
                linearLayout.AddView(new AView(Application.Context)
                {
                    LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                        ViewGroup.LayoutParams.MatchParent)
                });
            }

            return linearLayout;
    }
    
    private void ConfigureToolbar(MaterialToolbar toolbar)
    {
        //TODO: Move to Mapper in handler
        toolbar.Title = m_bottomSheet.Title;
        toolbar.TitleCentered = true;

        //TODO: Move to mapper in handler
        if (toolbar.Menu == null) return;

        foreach (var toolbarItem in m_bottomSheet.ToolbarItems)
        {
            toolbarItem.BindingContext = m_bottomSheet.BindingContext;
            var color = Colors.GetColor(BottomSheet.ToolbarActionButtonsName).ToPlatform();

            var text = toolbarItem.Text ?? string.Empty;
            var titleTinted = new SpannableString(text);
            titleTinted.SetSpan(new ForegroundColorSpan(color), 0, titleTinted.Length(), 0);

            var menuItem = toolbar.Menu.Add(0, AView.GenerateViewId(), (int)toolbarItem.Order, titleTinted);
            menuItem!.SetShowAsAction(ShowAsAction.IfRoom);
            menuItem.SetOnMenuItemClickListener(
                new GenericMenuClickListener(((IMenuItemController)toolbarItem).Activate));
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
    
    private void ToggleBottomSheetIfPossible()
    {
        var bottomSheetBehavior = m_bottomSheet.BottomSheetDialog.Behavior;
        var collapsed = m_bottomSheet.BottomSheetDialog.Behavior.State == BottomSheetBehavior.StateCollapsed;
        bottomSheetBehavior.State =
            collapsed ? BottomSheetBehavior.StateExpanded : BottomSheetBehavior.StateCollapsed;
    }
    
    public static partial void MapShouldFitToContent(BottomSheetHandler handler, BottomSheet bottomSheet)
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
    }

    public static partial void MapHasSearchBar(BottomSheetHandler handler, BottomSheet bottomSheet){}
    public static partial void MapToolbarItems(BottomSheetHandler handler, BottomSheet bottomSheet){}

    public static partial void MapTitle(BottomSheetHandler handler, BottomSheet bottomSheet){}

    public static partial void MapIsInteractiveCloseable(BottomSheetHandler handler, BottomSheet bottomSheet)
    {
        if (!bottomSheet.IsInteractiveCloseable)
        {
            bottomSheet.BottomSheetDialog.SetCancelable(false);
            bottomSheet.BottomSheetDialog.SetCanceledOnTouchOutside(false);
            bottomSheet.BottomSheetDialog.Behavior.AddBottomSheetCallback(
                new BottomSheetCallback(bottomSheet.BottomSheetDialog.Behavior));
            bottomSheet.BottomSheetDialog.SetOnKeyListener(new KeyListener(bottomSheet));
        }
    }
    
    internal class BottomSheetCallback : BottomSheetBehavior.BottomSheetCallback
    {
        private readonly BottomSheetBehavior m_behavior;

        public BottomSheetCallback(BottomSheetBehavior behavior)
        {
            m_behavior = behavior;
        }

        public override void OnSlide(AView bottomSheet, float slideOffset)
        {
            if (slideOffset < 0)
            {
                m_behavior.State = BottomSheetBehavior.StateHalfExpanded;
            }
        }

        public override void OnStateChanged(AView bottomSheet, int newState)
        {
        }
    }
    
    internal class KeyListener : Java.Lang.Object, IDialogInterfaceOnKeyListener
    {
        private readonly BottomSheet m_bottomSheet;

        public KeyListener(BottomSheet bottomSheet)
        {
            m_bottomSheet = bottomSheet;
        }

        public bool OnKey(IDialogInterface? dialog, Keycode keyCode, KeyEvent? e)
        {
            m_bottomSheet.OnBackButtonPressedCommand?.Execute(null);

            return true;
        }
    }
    
    internal class GenericMenuClickListener : Java.Lang.Object, IMenuItemOnMenuItemClickListener
    {
        readonly Action m_callback;

        public GenericMenuClickListener(Action callback)
        {
            m_callback = callback;
        }

        public bool OnMenuItemClick(IMenuItem item)
        {
            m_callback.Invoke();
            return true;
        }
    }
}